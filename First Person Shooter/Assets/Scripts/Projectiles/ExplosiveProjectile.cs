using UnityEngine;
using System.Linq;

public abstract class ExplosiveProjectile : Projectile
{
    protected int directDamage;
    protected int splashDamage;

    public LayerMask explosionLayerMask = ~0;

    public Rigidbody rb;

    protected void Awake()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    public abstract void ApplyForce();

    public override void Activate(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        ApplyForce();
    }

    public void Explode()
    {
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
    }
}
