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

    public EnemyProjectile projectile;
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

    public ObjectPooling projectileObjectPool;

    bool isPhysicsHappening = false;
    bool hasPhysicsLaunched = false;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        meshRenderer = GetComponent<MeshRenderer>();

        defaultMaterial = meshRenderer.material;

        SphereCollider detectionZone = gameObject.GetComponent<SphereCollider>();
        detectionZone.isTrigger = true;
        detectionZone.radius = Constants.c_enemy_distanceToPlayerWhenRandomDest;

        SetDestination(EEnemyDestination.RandomDestination);

        timerToFireProj = Constants.c_enemy_projFireRate;

        timerToSwitchDestination = Constants.c_enemy_timeToSwitchDestination;

        health = Constants.c_enemy_baseHealth;

        timerToResetMaterial = Constants.c_enemy_timerToResetMaterial;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled == true && isPhysicsHappening == false)
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
                        GameObject potentialProjectile = projectileObjectPool.GetPooledObject();

                        if (potentialProjectile == null)
                        {
                            EnemyProjectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
                            projectileObjectPool.AddObjectToPool(proj.gameObject);
                        }
                        else if (potentialProjectile != null)
                        {
                            EnemyProjectile proj = potentialProjectile.GetComponent<EnemyProjectile>();
                            proj.Activate(this);
                            potentialProjectile.SetActive(true);
                        }
                    }

                    timerToFireProj = Constants.c_enemy_projFireRate;
                }
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


    void SetDestination(EEnemyDestination destinationType)
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
}
