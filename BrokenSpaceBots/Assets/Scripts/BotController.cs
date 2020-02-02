using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotController : MonoBehaviour
{
    private const float GOAL_DISTANCE_TO_STATION = 1f;
    private const float GOAL_SQR_DISTANCE_TO_STATION = GOAL_DISTANCE_TO_STATION * GOAL_DISTANCE_TO_STATION;

    private const float TIME_BETWEEN_FIX_TICKS = 1f;

    public enum Personality { FREEZE, DISABLED, MIMIC, KILL, FIX }

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

    public float hps = 2;


    private float timeSinceLastFix;

    private Station targetStation;

    NavMeshAgent agent;

    public InventoryHolder inventory;

    public BotSoundMaster soundMaster;

    private void Start()
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
        if (targetStation != null && timeSinceLastFix > TIME_BETWEEN_FIX_TICKS)
            CheckStationFixProximity();

        // Random ambient vocal
        if (Random.Range(0,3200) == 13)
        {
            PlayVocal();
        }
    }

    private void InitPersonality()
    {
        agent.ResetPath();

        if (agent.isStopped && (CurrentPersonality == Personality.FIX || CurrentPersonality == Personality.MIMIC))
            agent.isStopped = false;
        if (!agent.isStopped && (CurrentPersonality == Personality.DISABLED || CurrentPersonality == Personality.FREEZE))
            agent.isStopped = true;

        if (currentPersonality == Personality.FIX)
        {
            Station mostUrgentStation = GetMostUrgentStation(focusStationType);
            if (mostUrgentStation != null)
            {
                targetStation = mostUrgentStation;
                agent.destination = targetStation.transform.position;
            }
        }
    }

    private void FixMostUrgentStation()
    {
        Station mostUrgentStation = GetMostUrgentStation(focusStationType);
        if (mostUrgentStation != null)
        {
            targetStation = mostUrgentStation;
            agent.destination = targetStation.transform.position;
        }
    }

    private void CheckStationFixProximity()
    {
        if ((targetStation.transform.position - transform.position).sqrMagnitude < GOAL_SQR_DISTANCE_TO_STATION + 1)
        {

            if (string.Equals(targetStation.Type, focusStationType))
            {
                timeSinceLastFix = 0;
                targetStation.Fix(hps);
                FixMostUrgentStation();
            }
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

    private Station GetMostUrgentStation(string stationType)
    {
        float lowestScore = float.MaxValue;
        Station mostUrgentStation = null;

        Object[] stations = FindObjectsOfType(typeof(Station));
        foreach (Station station in stations)
            if (GetStationPriorityScore(station) < lowestScore)
            {
                lowestScore = GetStationPriorityScore(station);
                mostUrgentStation = station;
            }


        return mostUrgentStation;
    }

    private float GetStationPriorityScore(Station station)
    {
        float percentHealth = station.CurrentHealth / (float)station.MaxHealth;
        float score = (station.transform.position - transform.position).sqrMagnitude + 1000 * percentHealth;
        return score;
    }

    private void PlayVocal()
    {
        if (currentPersonality == Personality.DISABLED || currentPersonality == Personality.FREEZE)
        {
            soundMaster.PlayBrokenSound();
        }
        else
        {
            soundMaster.PlayFixSound();
        }
    }
}
