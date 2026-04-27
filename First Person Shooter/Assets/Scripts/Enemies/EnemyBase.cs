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

public class EnemyBase : MonoBehaviour
{
    public int health;

    protected PlayerController player;

    public NavMeshAgent agent;

    protected EEnemyState currentState = EEnemyState.Idle;
    protected EEnemyDestination currentDestinationType = EEnemyDestination.None;
    protected float timerToSwitchDestination;

    protected bool isDamaged = false;
    protected float timerToResetMaterial;
    protected MeshRenderer meshRenderer;
    protected Material defaultMaterial;
    [SerializeField] protected Material damagedMaterial;
    
    protected bool isPhysicsHappening = false;
    protected bool hasPhysicsLaunched = false;
    protected Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        meshRenderer = GetComponent<MeshRenderer>();

        defaultMaterial = meshRenderer.material;

        SphereCollider detectionZone = gameObject.GetComponent<SphereCollider>();
        detectionZone.isTrigger = true;
        detectionZone.radius = Constants.c_enemy_distanceToPlayerWhenRandomDest;

        rb = GetComponent<Rigidbody>();

        SetDestination(EEnemyDestination.RandomDestination); // put this into the parent class
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (PathfindingRequirementCheck() == true)
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
                WhenDestIsPlayer();
            }
        }

        ResetMaterialAfterDamage();
    }

    protected virtual void WhenDestIsPlayer()// what an awful name
    {
        // this is empty because the child class with have the code that goes in here
        // and its not abstract because not all children will use it
    }

    protected bool PathfindingRequirementCheck()
    {
        if (agent.enabled == true && isPhysicsHappening == false)
        {
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        if (isPhysicsHappening == true)
        {
            if (!hasPhysicsLaunched && rb.linearVelocity.magnitude > 0.01f)
            {
                hasPhysicsLaunched = true;
            }

            if (hasPhysicsLaunched && rb.linearVelocity.magnitude <= 0.01f)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
                rb.useGravity = false;

                agent.Warp(transform.position);
                agent.enabled = true;

                isPhysicsHappening = false;
                hasPhysicsLaunched = false;
            }
        }
        
        rb.MovePosition(transform.position);
    }

    public void HandlePhysics()
    {
        agent.enabled = false;

        isPhysicsHappening = true;
        hasPhysicsLaunched = false;

        rb.isKinematic = false;
        rb.useGravity = true;
    }

    void ResetMaterialAfterDamage()
    {
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

    protected void SetDestination(EEnemyDestination destinationType)
    {
        if (agent.enabled == true && isPhysicsHappening == false)
        {
            currentDestinationType = destinationType;

            if (destinationType == EEnemyDestination.PlayerDestination)
            {
                SphereCollider detectionZone = gameObject.GetComponent<SphereCollider>();
                detectionZone.radius = Constants.c_enemy_distanceToPlayerWhenPlayerDest;

                agent.SetDestination(player.transform.position);
            }
            else if (destinationType == EEnemyDestination.RandomDestination)
            {
                timerToSwitchDestination = Constants.c_enemy_timeToSwitchDestination;

                SphereCollider detectionZone = gameObject.GetComponent<SphereCollider>();
                detectionZone.radius = Constants.c_enemy_distanceToPlayerWhenRandomDest;

                agent.SetDestination(DataManager.Instance.destinationManager.PickDestination());
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetDestination(EEnemyDestination.PlayerDestination);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetDestination(EEnemyDestination.RandomDestination);
        }
    }
}
