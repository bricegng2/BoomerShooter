using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    int health = 10;

    PlayerController player;

    public NavMeshAgent agent;

    public Projectile projectile;
    float timerToFireProj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        DecideDestination();

        timerToFireProj = Constants.c_enemyProjectile;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));

        DecideDestination();

        timerToFireProj -= 0.1f;
        if (timerToFireProj <= 0.0f)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);

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
        if (Vector3.Distance(transform.position, player.transform.position) > Constants.c_minDistanceToPlayer)
        {
            transform.position = DataManager.Instance.destinationManager.PickDestination();
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
