using UnityEngine;
using UnityEngine.AI;

public class FlameThrowerEnemy : EnemyBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        timerToSwitchDestination = Constants.c_enemy_timeToSwitchDestination;

        health = Constants.c_flameThrowerEnemy_baseHealth;

        timerToResetMaterial = Constants.c_enemy_timerToResetMaterial;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
