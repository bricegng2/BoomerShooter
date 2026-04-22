using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    PlayerController player;

    Vector3 target;
    float speed;
    Vector3 direction;
    float timeToDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        target = player.transform.position;
        
        direction = target - transform.position;

        speed = Constants.c_enemy_projSpeed;

        timeToDestroy = Constants.c_enemy_timeToDestroyProj;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0.0f)
        {
            this.gameObject.SetActive(false); // add this to an object pool
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.DoDamage(Constants.c_enemy_projDamage);
            this.gameObject.SetActive(false); // add this to an object pool 
        }
    }
}
