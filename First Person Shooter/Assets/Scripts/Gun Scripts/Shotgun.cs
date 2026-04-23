using UnityEngine;
using System;

public class Shotgun : PlayerGun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.Shotgun;

        damage = Constants.c_shotgun_damage;
        ammo = Constants.c_shotgun_ammo;
        fireRate = Constants.c_shotgun_fireRate;

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

                    float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
                    
                    float damageMod = 1.0f - (distanceToEnemy / 100.0f);

                    damageMod = Mathf.Clamp(damageMod, 0.5f, 1.0f); 

                    int finalDamage = Mathf.RoundToInt(damage * damageMod);

                    enemy.DoDamage(finalDamage);
                }
                else if (hits[i].collider.CompareTag("Dumpster"))
                {
                    GameObject dumpster = hits[i].collider.gameObject;

                    AddImpactForce(dumpster);
                }
            }
        }
    }

    protected override void AddImpactForce(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(player.playerCamera.transform.forward * 7.5f, ForceMode.Impulse);
    }

    protected override float ResetFireRate()
    {
        return Constants.c_shotgun_fireRate;
    }
}
