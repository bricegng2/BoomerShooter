using UnityEngine;
using System;

public class Shotgun : PlayerGun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.Shotgun;

        damage = Constants.c_gun_shotgunDamage;
        ammo = Constants.c_gun_shotgunAmmo;
        fireRate = Constants.c_gun_shotgunFireRate;

        playerHUD.UpdateAmmo(ammo);
    }

    public override void Shoot()
    {
        if (ammo > 0)
        {
            ModAmmo(-1);

            RaycastHit[] hits = Physics.SphereCastAll(player.playerCamera.transform.position, 0.55f, player.playerCamera.transform.forward, 100.0f, shootLayerMask, QueryTriggerInteraction.Ignore);
            
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Enemy"))
                {
                    Enemy enemy = hits[i].collider.GetComponent<Enemy>();
                    enemy.DoDamage(damage);
                }
            }
        }
    }

    protected override float ResetFireRate()
    {
        return Constants.c_gun_shotgunFireRate;
    }
}
