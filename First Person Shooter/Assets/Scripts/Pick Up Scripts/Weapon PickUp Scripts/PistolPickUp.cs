using UnityEngine;

public class PistolPickUp : PickUp
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void ApplyPickUpEffect()
    {
        isPickedUp = true;
        DataManager.Instance.inventoryManager.guns.Find(gun => gun.gunType == EGunType.Pistol).gameObject.SetActive(true);

        player.SwitchGun(EGunType.Pistol);
        Debug.Log("Pistol Picked Up");
    }
}
