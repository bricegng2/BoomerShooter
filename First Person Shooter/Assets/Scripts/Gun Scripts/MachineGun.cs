using UnityEngine;

public class MachineGun : PlayerGun
{
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.MachineGun;

        damage = Constants.c_gun_machineGunDamage;
        ammo = Constants.c_gun_machineGunAmmo;
        fireRate = Constants.c_gun_machineGunFireRate;

        playerHUD.UpdateAmmo(ammo);
    }

    protected override float ResetFireRate()
    {
        return Constants.c_gun_machineGunFireRate;
    }
}
