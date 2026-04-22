using UnityEngine;

public class Pistol : PlayerGun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.Pistol;

        damage = Constants.c_pistol_damage;
        ammo = Constants.c_pistol_ammo;
        fireRate = Constants.c_pistol_fireRate;

        playerHUD.UpdateAmmo(ammo);
    }

    protected override float ResetFireRate()
    {
        return Constants.c_pistol_fireRate;
    }
}
