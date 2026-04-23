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

    protected override void AddImpactForce(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(player.playerCamera.transform.forward * 1.0f, ForceMode.Impulse);
    }
}
