using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GrenadeLauncherProjectile : MonoBehaviour
{
    PlayerController player;

    float speed;
    Vector3 direction;
    float timeToDestroy;
    int directDamage;
    int splashDamage;

    public LayerMask explosionLayerMask = ~0;

    bool hasCollided;

    public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();

        direction = player.playerCamera.transform.forward;

        speed = Constants.c_grenadeLauncher_projSpeed;

        directDamage = Constants.c_grenadeLauncher_directDamage;
        splashDamage = Constants.c_grenadeLauncher_splashDamage;

        timeToDestroy = Constants.c_grenadeLauncher_timeToDestroyProj;

        hasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (hasCollided == false)
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0.0f)
            {
                timeToDestroy = Constants.c_grenadeLauncher_timeToDestroyProj;
                this.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    public void ApplyForce()
    {
        Vector3 launchDirection = (direction + new Vector3(0.0f, 0.3f, 0.0f)).normalized;
        rb.AddForce(launchDirection * speed, ForceMode.Impulse);
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        ApplyForce();
    }

    void OnCollisionEnter(Collision collision)
    {
        //hasCollided = true; // REMEMBER TO SET THIS BACK TO FALSE WHEN REUSING THE PROJECTILE
    }

    // for a right click function:
    /*
    Enemy hitEnemy = null;

        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            hitEnemy = enemy;

            enemy.HandlePhysics();

            enemy.GetComponent<Rigidbody>().AddForce(player.playerCamera.transform.forward * 10.0f, ForceMode.Impulse);

            enemy.DoDamage(directDamage);
        }
        else if (other.gameObject.CompareTag("Dumpster"))
        {
            GameObject dumpster = other.gameObject;

            dumpster.GetComponent<Rigidbody>().AddForce(player.playerCamera.transform.forward * 20.0f, ForceMode.Impulse);

            this.gameObject.SetActive(false);
        }
        
        Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

        GameObject[] objectsForPhysics = Physics.OverlapSphere(explosionPosition, Constants.c_grenadeLauncher_explosionRadius, explosionLayerMask, QueryTriggerInteraction.Ignore).Select(other => other.gameObject).ToArray();
        
        for (int i = 0; i < objectsForPhysics.Length; i++)
        {
            GameObject obj = objectsForPhysics[i];
            for (int j = 0; j < objectsForPhysics.Length; j++)
            {
                if (i != j && obj == objectsForPhysics[j])
                {
                    objectsForPhysics[i] = null;
                }
            }
        }

        for (int i = 0; i < objectsForPhysics.Length; i++)
        {
            if (objectsForPhysics[i] == null)
            {
                continue;
            }

            Rigidbody rb = objectsForPhysics[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (objectsForPhysics[i].CompareTag("Enemy"))
                {
                    Enemy enemy = objectsForPhysics[i].GetComponent<Enemy>();

                    if (enemy == hitEnemy) // if the enemy is the one that was directly hit by the rocket, skip it
                    {
                        continue;
                    }

                    enemy.HandlePhysics();

                    rb.AddExplosionForce(200.0f, explosionPosition, Constants.c_grenadeLauncher_explosionRadius * 1.2f, 0.8f);

                    enemy.DoDamage(splashDamage);
                }
                else if (objectsForPhysics[i].CompareTag("Dumpster"))
                {
                    rb.AddExplosionForce(800.0f, explosionPosition, Constants.c_grenadeLauncher_explosionRadius, 0.8f);
                }
                else if (objectsForPhysics[i].CompareTag("Player"))
                {
                    rb.AddExplosionForce(800.0f, explosionPosition, Constants.c_grenadeLauncher_explosionRadius * 1.2f, 1.2f);
                }
            }
        }
        
        if (this.gameObject.activeSelf == true)
        {
            this.gameObject.SetActive(false);
        }
    */
}
