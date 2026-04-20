using UnityEngine;

public class Pistol : PlayerGun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.Pistol;

        damage = Constants.c_gun_pistolDamage;
        ammo = Constants.c_gun_pistolAmmo;
        fireRate = Constants.c_gun_pistolFireRate;

        playerHUD.UpdateAmmo(ammo);
    }

    protected override float ResetFireRate()
    {
        return Constants.c_gun_pistolFireRate;
    }
}
