using UnityEngine;

public class MachineGun : PlayerGun
{
    protected override void Start()
    {
        base.Start();

        gunType = EGunType.MachineGun;

        damage = Constants.c_machineGun_damage;
        ammo = Constants.c_machineGun_ammo;
        fireRate = Constants.c_machineGun_fireRate;

        playerHUD.UpdateAmmo(ammo);
    }

    protected override float ResetFireRate()
    {
        return Constants.c_machineGun_fireRate;
    }
}
