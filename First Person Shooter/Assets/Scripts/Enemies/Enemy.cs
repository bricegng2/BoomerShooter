using UnityEngine;
using UnityEngine.AI;



public class Enemy : EnemyBase
{
    public EnemyProjectile projectile;
    float timerToFireProj;

    public ObjectPooling projectileObjectPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        timerToFireProj = Constants.c_enemy_projFireRate;

        timerToSwitchDestination = Constants.c_enemy_timeToSwitchDestination;

        health = Constants.c_enemy_baseHealth;

        timerToResetMaterial = Constants.c_enemy_timerToResetMaterial;
    }

    protected override void WhenDestIsPlayer()
    {
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

    void SetState()
    {

    }
}
