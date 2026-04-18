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
    int health = 10;

    PlayerController player;

    public NavMeshAgent agent;

    public Projectile projectile;
    float timerToFireProj;

    EEnemyState currentState = EEnemyState.Idle;
    EEnemyDestination currentDestinationType = EEnemyDestination.None;
    float timerToSwitchDestination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        DecideDestination();

        timerToFireProj = Constants.c_enemyProjectile;

        timerToSwitchDestination = Constants.c_timeToSwitchDestination;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));

        DecideDestination();

        if (currentDestinationType == EEnemyDestination.RandomDestination)
        {
            timerToSwitchDestination -= Time.deltaTime;
            if (timerToSwitchDestination <= 0.0f)
            {
                Debug.Log(currentDestinationType);
                
                timerToSwitchDestination = Constants.c_timeToSwitchDestination;
                GetDestination(EEnemyDestination.RandomDestination);
            }
        }

        timerToFireProj -= 0.1f;
        if (timerToFireProj <= 0.0f)
        {
            if (projectile != null)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
            }

            timerToFireProj = Constants.c_enemyProjectile;
        }
    }

    public void DoDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        // add this object to a pool possibly
        Debug.Log(health);
    }

    void DecideDestination()
    {
        EEnemyDestination destination = EEnemyDestination.None;

        if ((Vector3.Distance(transform.position, player.transform.position) > Constants.c_minDistanceToPlayer) && currentDestinationType == EEnemyDestination.PlayerDestination)
        {
            destination = EEnemyDestination.RandomDestination;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= Constants.c_minDistanceToPlayer)
        {
            destination = EEnemyDestination.PlayerDestination;
        }

        GetDestination(destination);
    }

    void SetState()
    {
        
    }

    void GetDestination(EEnemyDestination destination)
    {
        currentDestinationType = destination;

        if (destination == EEnemyDestination.PlayerDestination)
        {
            agent.SetDestination(player.transform.position);
        }
        else if (destination == EEnemyDestination.RandomDestination)
        {
            agent.SetDestination(DataManager.Instance.destinationManager.PickDestination());
        }
    }
}
