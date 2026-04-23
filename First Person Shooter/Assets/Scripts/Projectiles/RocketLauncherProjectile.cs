using System.Linq;
using UnityEngine;

public class RocketLauncherProjectile : MonoBehaviour
{
    PlayerController player;

    float speed;
    Vector3 direction;
    float timeToDestroy;
    int damage = Constants.c_rocketLauncher_damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        direction = player.playerCamera.transform.forward;

        speed = Constants.c_rocketLauncher_projSpeed;

        damage = Constants.c_rocketLauncher_damage;

        timeToDestroy = Constants.c_rocketLauncher_timeToDestroyProj;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0.0f)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        direction = player.playerCamera.transform.forward;

        speed = Constants.c_rocketLauncher_projSpeed;

        damage = Constants.c_rocketLauncher_damage;

        timeToDestroy = Constants.c_rocketLauncher_timeToDestroyProj;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject[] objectsForPhysics = Physics.OverlapSphere(transform.position, Constants.c_rocketLauncher_explosionRadius).Select(other => other.gameObject).ToArray();
        
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
            if (objectsForPhysics[i] != null)
            {
                Rigidbody rb = objectsForPhysics[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (objectsForPhysics[i].CompareTag("Enemy"))
                    {
                        Enemy enemy = objectsForPhysics[i].GetComponent<Enemy>();

                        enemy.DoDamage(damage);

                        rb.AddExplosionForce(20.0f, transform.position, Constants.c_rocketLauncher_explosionRadius * 0.2f);
                    }
                    else if (objectsForPhysics[i].CompareTag("Dumpster"))
                    {
                        rb.AddExplosionForce(400.0f, transform.position, Constants.c_rocketLauncher_explosionRadius, 0.8f);
                    }
                    else if (objectsForPhysics[i].CompareTag("Player"))
                    {
                        rb.AddExplosionForce(800.0f, transform.position, Constants.c_rocketLauncher_explosionRadius * 0.2f, 1.2f);
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("Dumpster"))
        {
            GameObject dumpster = other.gameObject;

            dumpster.GetComponent<Rigidbody>().AddForce(player.playerCamera.transform.forward * 20.0f, ForceMode.Impulse);

            this.gameObject.SetActive(false);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
