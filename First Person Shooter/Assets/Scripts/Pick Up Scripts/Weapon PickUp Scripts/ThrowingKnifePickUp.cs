using UnityEngine;

public class ThrowingKnifePickUp : PickUp
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void ApplyPickUpEffect()
    {
        isPickedUp = true;
        DataManager.Instance.inventoryManager.throwingKnives++;
    }
}
