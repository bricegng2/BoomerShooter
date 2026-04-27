using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    PlayerController player;

    Vector3 target;
    float speed;
    Vector3 direction;
    float timeToDestroy;

    bool isParried = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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

    public void Activate(Enemy enemy)
    {
        transform.position = enemy.transform.position;

        transform.rotation = Quaternion.identity;

        target = player.transform.position;
        
        direction = target - transform.position;

        speed = Constants.c_enemy_projSpeed;

        timeToDestroy = Constants.c_enemy_timeToDestroyProj;

        isParried = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.parry.parryCollider.enabled == true)
            {
                player.parry.ResetParry();

                isParried = true;
                
                direction = player.playerCamera.transform.forward;
                speed *= 3.0f;

                Debug.Log("projectile parried | " + this.name + " | " + this.gameObject.activeSelf);
                return;
            }
            player.DoDamage(Constants.c_enemy_projDamage);
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Enemy") && isParried == true)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            enemy.DoDamage(Constants.c_enemy_projDamage);

            this.gameObject.SetActive(false);
        }
    }
}
