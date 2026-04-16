using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    int health = 10;

    PlayerController player;

    public NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        
        agent.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
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
}
