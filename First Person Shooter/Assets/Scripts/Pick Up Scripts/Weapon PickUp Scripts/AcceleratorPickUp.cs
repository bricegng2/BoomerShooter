using UnityEngine;

public class AcceleratorPickUp : PickUp
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void ApplyPickUpEffect()
    {
        isPickedUp = true;
        DataManager.Instance.inventoryManager.guns.Find(gun => gun.gunType == EGunType.Accelerator).gameObject.SetActive(true);

        player.SwitchGun(EGunType.Accelerator);
        Debug.Log("Accelerator Picked Up");
    }
}
