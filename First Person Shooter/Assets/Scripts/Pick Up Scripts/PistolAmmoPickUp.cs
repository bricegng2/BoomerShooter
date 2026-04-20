using UnityEngine;

public class PistolAmmoPickUp : PickUp
{
    int pistolAmmoAmount;

    Pistol pistol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        pistolAmmoAmount = Constants.c_pickUp_pistolAmmoAmount;

        pistol = DataManager.Instance.inventoryManager.guns.Find(gun => gun.gunType == EGunType.Pistol) as Pistol;
    }

    protected override void ApplyPickUpEffect()
    {
        if (pistol.gameObject.activeInHierarchy == true)
        {
            isPickedUp = true;
            pistol.ModAmmo(pistolAmmoAmount);
        }
    }
}
