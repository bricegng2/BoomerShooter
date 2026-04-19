using UnityEngine;

public class ArmourPickUp : PickUp
{
    int armourAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        
        armourAmount = Constants.c_pickUp_armourAmount;
    }

    protected override void ApplyPickUpEffect()
    {
        player.ModArmour(armourAmount);
    }
}
