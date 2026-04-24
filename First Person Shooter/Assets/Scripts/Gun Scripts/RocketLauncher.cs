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

        damage = Constants.c_rocketLauncher_damage;
        ammo = Constants.c_rocketLauncher_ammo;
        fireRate = Constants.c_rocketLauncher_fireRate;

        playerHUD.UpdateAmmo(ammo);
    }

    public override void Shoot()
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
                rocketLauncherProjObjectPool.AddObjectToPool(proj.gameObject);
            }
            else if (potentialProjectile != null)
            {
                RocketLauncherProjectile proj = potentialProjectile.GetComponent<RocketLauncherProjectile>();
                proj.Activate(position);
                potentialProjectile.SetActive(true);
            }
        }
    }

    protected override float ResetFireRate()
    {
        return Constants.c_rocketLauncher_fireRate;
    }
}
