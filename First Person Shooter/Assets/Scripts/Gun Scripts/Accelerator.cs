using UnityEngine;

public class Accelerator : PlayerGun
{
    public AcceleratorProjectile acceleratorProjectilePrefab;

    public ObjectPooling acceleratorProjObjectPool;
    
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.Accelerator;

        damage = Constants.c_accelerator_damage;
        ammo = Constants.c_accelerator_ammo;
        fireRate = Constants.c_accelerator_fireRate;

        playerHUD.UpdateAmmo(ammo);
    }

    public override void Shoot()
    {
        if (ammo > 0)
        {
            int projCount = 5;

            ModAmmo(-projCount);

            for (int i = -projCount/2; i <= projCount/2; i++)
            {
                Vector3 position = player.playerCamera.transform.position;
                position.y -= 0.2f;
                position += player.playerCamera.transform.forward * 0.5f;

                GameObject potentialProjectile = acceleratorProjObjectPool.GetPooledObject();

                if (potentialProjectile == null)
                {
                    AcceleratorProjectile proj = Instantiate(acceleratorProjectilePrefab, position, Quaternion.identity);
                    proj.indexForDirection = i;
                    acceleratorProjObjectPool.AddObjectToPool(proj.gameObject);
                }
                else if (potentialProjectile != null)
                {
                    AcceleratorProjectile proj = potentialProjectile.GetComponent<AcceleratorProjectile>();
                    proj.Activate(position, i);
                    potentialProjectile.SetActive(true);
                }
            }
        }
    }

    protected override float ResetFireRate()
    {
        return Constants.c_accelerator_fireRate;
    }
}
