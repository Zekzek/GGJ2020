using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotController : MonoBehaviour
{
    public enum Personality { DISABLED, MIMIC, BALL }

    [SerializeField]
    private Personality currentPersonality;
    public Personality CurrentPersonality
    {
        get => currentPersonality; set
        {
            currentPersonality = value;
            InitPersonality();
        }
    }

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitPersonality();
    }

    private void Update()
    {
        switch (currentPersonality)
        {
            case (Personality.MIMIC):
                MimicUpdate();
                break;
            default:
                break;
        }
    }

    private void InitPersonality()
    {
        agent.ResetPath();

        if (agent.isStopped && (CurrentPersonality == Personality.BALL || CurrentPersonality == Personality.MIMIC))
            agent.isStopped = false;
        if (!agent.isStopped && CurrentPersonality == Personality.DISABLED)
            agent.isStopped = true;

        switch (currentPersonality)
        {
            case (Personality.BALL):
                agent.destination = new Vector3(-0.5f, 0.5f, 3.5f);
                break;
            default:
                break;
        }
    }

    private void MimicUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        if (direction.magnitude > 1)
            direction.Normalize();

        agent.Move(direction * Time.deltaTime * agent.speed);
    }
}
