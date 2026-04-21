using UnityEngine;

public class ShotgunPickUp : PickUp
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override void ApplyPickUpEffect()
    {
        isPickedUp = true;
        DataManager.Instance.inventoryManager.guns.Find(gun => gun.gunType == EGunType.Shotgun).gameObject.SetActive(true);

        player.SwitchGun(EGunType.Shotgun);
        Debug.Log("Shotgun Picked Up");
    }
}
