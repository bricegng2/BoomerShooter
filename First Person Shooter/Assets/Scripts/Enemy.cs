using UnityEngine;
using UnityEngine.AI;

public enum EEnemyState
{
    Idle,
    Moving,
    Attacking,
}

public enum EEnemyDestination
{
    None,
    PlayerDestination,
    RandomDestination,
}

public class Enemy : MonoBehaviour
{
    public int health;

    PlayerController player;

    public NavMeshAgent agent;

    public Projectile projectile;
    float timerToFireProj;

    EEnemyState currentState = EEnemyState.Idle;
    EEnemyDestination currentDestinationType = EEnemyDestination.None;
    float timerToSwitchDestination;

    bool isDamaged = false;
    float timerToResetMaterial;
    MeshRenderer meshRenderer;
    Material defaultMaterial;
    [SerializeField]
    Material damagedMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        meshRenderer = GetComponent<MeshRenderer>();

        defaultMaterial = meshRenderer.material;

        SphereCollider detectionZone = gameObject.GetComponent<SphereCollider>();
        detectionZone.isTrigger = true;
        detectionZone.radius = Constants.c_enemy_minDistanceToPlayerWhenRandomDest;

        SetDestination(EEnemyDestination.RandomDestination);

        timerToFireProj = Constants.c_enemy_projFireRate;

        timerToSwitchDestination = Constants.c_enemy_timeToSwitchDestination;

        health = Constants.c_enemy_baseHealth;

        timerToResetMaterial = Constants.c_enemy_timerToResetMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDestinationType == EEnemyDestination.RandomDestination)
        {
            bool reached = agent.remainingDistance <= agent.stoppingDistance;

            if (reached == true)
            {
                timerToSwitchDestination -= Time.deltaTime;
                if (timerToSwitchDestination <= 0.0f)
                {
                    timerToSwitchDestination = Constants.c_enemy_timeToSwitchDestination;
                    SetDestination(EEnemyDestination.RandomDestination);
                }
            }
        }
        else if (currentDestinationType == EEnemyDestination.PlayerDestination)
        {
            SetDestination(EEnemyDestination.PlayerDestination);

            timerToFireProj -= 0.1f;
            if (timerToFireProj <= 0.0f)
            {
                if (projectile != null)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }

                timerToFireProj = Constants.c_enemy_projFireRate;
            }
        }

        if (isDamaged)
        {
            timerToResetMaterial -= Time.deltaTime;
            if (timerToResetMaterial <= 0.0f)
            {
                isDamaged = false;
                timerToResetMaterial = Constants.c_enemy_timerToResetMaterial;
                meshRenderer.material = defaultMaterial;
            }
        }
    }

    public void DoDamage(int damage)
    {
        health -= damage;

        isDamaged = true;
        meshRenderer.material = damagedMaterial;

        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        // add this object to a pool possibly
        Debug.Log(health);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            SetDestination(EEnemyDestination.PlayerDestination);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            SetDestination(EEnemyDestination.RandomDestination);
        }
    }

    void SetState()
    {

    }

    void SetDestination(EEnemyDestination destinationType)
    {
        currentDestinationType = destinationType;

        if (destinationType == EEnemyDestination.PlayerDestination)
        {
            SphereCollider detectionZone = gameObject.GetComponent<SphereCollider>();
            detectionZone.radius = Constants.c_enemy_minDistanceToPlayerWhenPlayerDest;

            agent.SetDestination(player.transform.position);
        }
        else if (destinationType == EEnemyDestination.RandomDestination)
        {
            timerToSwitchDestination = Constants.c_enemy_timeToSwitchDestination;

            SphereCollider detectionZone = gameObject.GetComponent<SphereCollider>();
            detectionZone.radius = Constants.c_enemy_minDistanceToPlayerWhenRandomDest;

            agent.SetDestination(DataManager.Instance.destinationManager.PickDestination());
        }
    }
}
