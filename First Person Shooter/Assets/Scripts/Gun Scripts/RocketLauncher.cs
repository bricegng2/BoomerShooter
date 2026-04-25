using UnityEngine;

public class RocketLauncher : PlayerGun
{
    public ObjectPooling rocketLauncherProjObjectPool;

    public RocketLauncherProjectile rocketLauncherProjectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.RocketLauncher;

        damage = 0; // not being used for rocket launcher, damage is handled by the projectile
        ammo = Constants.c_rocketLauncher_ammo;
        fireRate = Constants.c_rocketLauncher_fireRate;

        playerHUD.UpdateAmmo(ammo);
    }

    protected override void Shoot()
    {
        if (isSelected == false)
        {
            return;
        }

        if (ammo > 0)
        {
            ModAmmo(-1);

            Vector3 position = player.playerCamera.transform.position;
            position.y -= 0.2f;
            position += player.playerCamera.transform.forward * 0.5f;

            GameObject potentialProjectile = rocketLauncherProjObjectPool.GetPooledObject();

            if (potentialProjectile == null)
            {
                RocketLauncherProjectile proj = Instantiate(rocketLauncherProjectilePrefab, position, Quaternion.LookRotation(player.playerCamera.transform.forward));
                proj.ApplyForce();
                rocketLauncherProjObjectPool.AddObjectToPool(proj.gameObject);
            }
            else if (potentialProjectile != null)
            {
                RocketLauncherProjectile proj = potentialProjectile.GetComponent<RocketLauncherProjectile>();
                potentialProjectile.SetActive(true);
                proj.Activate(position);
            }
        }
    }

    protected override float ResetFireRate()
    {
        return Constants.c_rocketLauncher_fireRate;
    }
}
