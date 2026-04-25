using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : PlayerGun
{
    public ObjectPooling grenadeProjObjectPool;

    public GrenadeLauncherProjectile grenadeProjectilePrefab;

    List<GrenadeLauncherProjectile> activeGrenades = new List<GrenadeLauncherProjectile>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.GrenadeLauncher;

        damage = 0; // not being used for grenade launcher, damage is handled by the projectile
        ammo = Constants.c_grenadeLauncher_ammo;
        fireRate = Constants.c_grenadeLauncher_fireRate;

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

            GameObject potentialProjectile = grenadeProjObjectPool.GetPooledObject();

            if (potentialProjectile == null)
            {
                GrenadeLauncherProjectile proj = Instantiate(grenadeProjectilePrefab, position, Quaternion.LookRotation(player.playerCamera.transform.forward));
                proj.ApplyForce();
                grenadeProjObjectPool.AddObjectToPool(proj.gameObject);
                activeGrenades.Add(proj);
            }
            else if (potentialProjectile != null)
            {
                GrenadeLauncherProjectile proj = potentialProjectile.GetComponent<GrenadeLauncherProjectile>();
                potentialProjectile.SetActive(true);
                proj.Activate(position);
            }
        }
    }

    protected override void RightClick()
    {
        for (int i = 0; i < activeGrenades.Count; i++)
        {
            if (activeGrenades[i].gameObject.activeSelf == true)
            {
                activeGrenades[i].Explode();
            }
        }
    }

    protected override float ResetFireRate()
    {
        return Constants.c_grenadeLauncher_fireRate;
    }
}
