using UnityEngine;

public class PistolAmmoPickUp : PickUp
{
    int pistolAmmoAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        pistolAmmoAmount = Constants.c_pickUp_pistolAmmoAmount;
    }

    protected override void ApplyPickUpEffect()
    {
        player.gun.ModAmmo(pistolAmmoAmount);
    }
}
