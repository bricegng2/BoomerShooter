using UnityEngine;

public class MachineGunPIckUp : PickUp
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override void ApplyPickUpEffect()
    {
        isPickedUp = true;
        DataManager.Instance.inventoryManager.guns.Find(gun => gun.gunType == EGunType.MachineGun).gameObject.SetActive(true);

        player.SwitchGun(EGunType.MachineGun);
        Debug.Log("Machine Gun Picked Up");
    }
}
