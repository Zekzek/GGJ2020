using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotController : MonoBehaviour
{
    private const float GOAL_DISTANCE_TO_STATION = 1.5f;
    private const float GOAL_SQR_DISTANCE_TO_STATION = GOAL_DISTANCE_TO_STATION * GOAL_DISTANCE_TO_STATION;

    private const float TIME_BETWEEN_FIX_TICKS = 1f;

    public enum Personality { FREEZE, DISABLED, FIX, MIMIC, KILL, SPIN, STARE }

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

    [SerializeField]
    private float hps = 2;

    [SerializeField]
    private PlayerController player;

    private float timeSinceLastFix;

    private float timeSinceLastSound = 0f;

    private Station targetStation;

    private NavMeshAgent agent;

    public InventoryHolder inventory;

    public BotSoundMaster soundMaster;

    public MeshRenderer botHat;
    public Light botLight;

    public Material badHat;
    public Material goodHat;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = GOAL_DISTANCE_TO_STATION;
        InitPersonality();
    }

    private void Update()
    {
        timeSinceLastFix += Time.deltaTime;

        if (currentPersonality == Personality.MIMIC)
            MimicUpdate();
        else if (currentPersonality == Personality.SPIN)
            SpinUpdate();
        else if (currentPersonality == Personality.STARE)
            StareUpdate();
        else if (currentPersonality == Personality.FIX)
        {
            if (targetStation != null && timeSinceLastFix > TIME_BETWEEN_FIX_TICKS)
                CheckStationFixProximity();
        }

        // Random ambient vocal
        if (timeSinceLastSound + 2.2f < Time.time && Random.Range(0,3200) == 13)
        {
            PlayVocal();
        }
    }

    private void InitPersonality()
    {
        soundMaster.StopLoop();
        //agent.isStopped = true;

        if (agent.isStopped && (CurrentPersonality == Personality.FIX || CurrentPersonality == Personality.MIMIC))
            agent.isStopped = false;
        if (!agent.isStopped && (CurrentPersonality == Personality.DISABLED || CurrentPersonality == Personality.FREEZE))
            agent.isStopped = true;

        if (currentPersonality == Personality.FIX)
        {
            targetStation = null;
            FixMostUrgentStation();
        }

        UpdateHat();
    }

    private void FixMostUrgentStation()
    {
        Station mostUrgentStation = GetMostUrgentStation(focusStationType);
        if (mostUrgentStation != null)
        {
            if (mostUrgentStation != targetStation)
            {
                targetStation = mostUrgentStation;
                agent.destination = targetStation.transform.position;
                soundMaster.StartMoving();
            }
        }
        else
        {
            soundMaster.StopLoop();
        }
    }

    private void CheckStationFixProximity()
    {
        if ((targetStation.transform.position - transform.position).sqrMagnitude < GOAL_SQR_DISTANCE_TO_STATION + 1)
        {
            soundMaster.StartFixing();

                timeSinceLastFix = 0;
                targetStation.Fix(hps);
                FixMostUrgentStation();
            //TODO: handle distactable logic
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

    private void SpinUpdate()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 90f);
    }

    private void StareUpdate()
    {
        Vector3 relativePlayerPosition = transform.InverseTransformPoint(player.transform.position).normalized;
        if (relativePlayerPosition.x > 0.1f)
            transform.Rotate(Vector3.up, Time.deltaTime * 45f);
        else if (relativePlayerPosition.x < -0.1f)
            transform.Rotate(Vector3.up, Time.deltaTime * -45f);
    }

    private Station GetMostUrgentStation(string stationType)
    {
        float lowestScore = float.MaxValue;
        Station mostUrgentStation = null;

        Object[] stations = FindObjectsOfType(typeof(Station));
        foreach (Station station in stations)
        {
            if (string.Equals(station.Type, focusStationType))
            {
            if (GetStationPriorityScore(station) < lowestScore)
            {
                lowestScore = GetStationPriorityScore(station);
                mostUrgentStation = station;
            }
            }
        }

        return mostUrgentStation;
    }

    private float GetStationPriorityScore(Station station)
    {
        float percentHealth = station.CurrentHealth / (float)station.MaxHealth;
        float score = (station.transform.position - transform.position).sqrMagnitude + 1000 * percentHealth;
        return score;
    }

    public void PlayVocal()
    {
        if (currentPersonality != Personality.FIX)
        {
            soundMaster.PlayComplainSound();
            botHat.material = badHat;
        }
        else
        {
            soundMaster.PlayHappySound();
            botHat.material = goodHat;
        }
        timeSinceLastSound = Time.time;
    }

    public void UpdateHat()
    {
        if (currentPersonality != Personality.FIX)
        {
            botHat.material = badHat;
            botLight.color = new Color(1,0.5f,0.5f);
        }
        else
        {
            botHat.material = goodHat;
            botLight.color = new Color(0.5f, 1f, 0.5f);
        }
    }
}
