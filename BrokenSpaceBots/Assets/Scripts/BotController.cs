using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotController : MonoBehaviour
{
    public enum Personality { DISABLED, MIMIC, FIX }

    [SerializeField]
    private Personality currentPersonality;
    public Personality CurrentPersonality
    {
        get => currentPersonality;
        set
        {
            currentPersonality = value;
            InitPersonality();
        }
    }

    [SerializeField]
    private string focusStationType;
    public string FocusStationType
    {
        get => focusStationType;
        set
        {
            focusStationType = value;
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

        if (agent.isStopped && (CurrentPersonality == Personality.FIX || CurrentPersonality == Personality.MIMIC))
            agent.isStopped = false;
        if (!agent.isStopped && CurrentPersonality == Personality.DISABLED)
            agent.isStopped = true;

        switch (currentPersonality)
        {
            case (Personality.FIX):
                GoToNearestStation("Ball");
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

    private void GoToNearestStation(string stationType)
    {
        float closestSquareDistance = float.MaxValue;
        Station closestStation = null;

        Object[] stations = FindObjectsOfType(typeof(Station));
        foreach (Station station in stations)
            if ((station.transform.position - transform.position).sqrMagnitude < closestSquareDistance)
            {
                closestSquareDistance = (station.transform.position - transform.position).sqrMagnitude;
                closestStation = station;
            }

        if (closestStation != null)
            agent.destination = closestStation.transform.position;
    }
}
