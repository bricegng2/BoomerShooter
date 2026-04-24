using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RocketLauncherProjectile : MonoBehaviour
{
    PlayerController player;

    float speed;
    Vector3 direction;
    float timeToDestroy;
    int damage = Constants.c_rocketLauncher_damage;

    public LayerMask explosionLayerMask = ~0;

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
        transform.rotation = Quaternion.LookRotation(player.playerCamera.transform.forward);

        direction = player.playerCamera.transform.forward;

        speed = Constants.c_rocketLauncher_projSpeed;

        damage = Constants.c_rocketLauncher_damage;

        timeToDestroy = Constants.c_rocketLauncher_timeToDestroyProj;
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

        GameObject[] objectsForPhysics = Physics.OverlapSphere(explosionPosition, Constants.c_rocketLauncher_explosionRadius, explosionLayerMask, QueryTriggerInteraction.Ignore).Select(other => other.gameObject).ToArray();
        
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

                        enemy.HandlePhysics();

                        rb.AddExplosionForce(200.0f, explosionPosition, Constants.c_rocketLauncher_explosionRadius, 0.8f);

                        enemy.DoDamage(damage);
                    }
                    else if (objectsForPhysics[i].CompareTag("Dumpster"))
                    {
                        rb.AddExplosionForce(800.0f, explosionPosition, Constants.c_rocketLauncher_explosionRadius, 0.8f);
                    }
                    else if (objectsForPhysics[i].CompareTag("Player"))
                    {
                        rb.AddExplosionForce(800.0f, explosionPosition, Constants.c_rocketLauncher_explosionRadius * 0.2f, 1.2f);
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
        
        if (this.gameObject.activeSelf == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
