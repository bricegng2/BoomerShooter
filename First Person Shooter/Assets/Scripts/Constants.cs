using UnityEngine;

public static class Constants
{
    //enemy variables
    public const float c_enemy_projFireRate = 5.0f;
    public const float c_enemy_projSpeed = 5.0f;
    public const float c_enemy_timeToDestroyProj = 5.0f;
    public const float c_enemy_minDistanceToPlayerWhenPlayerDest = 17.5f;
    public const float c_enemy_minDistanceToPlayerWhenRandomDest = 10.0f;
    public const float c_enemy_timeToSwitchDestination = 2.0f;
    public const int c_enemy_projDamage = 5;
    public const int c_enemy_baseHealth = 20;
    public const float c_enemy_timerToResetMaterial = 0.15f;


    //player variables


    // gun variables
    public const int c_gun_defaultDamage = 0;
    public const int c_gun_defaultAmmo = 0;
    public const float c_gun_defaultFireRate = 0f;
    // pistol variables
    public const int c_gun_pistolDamage = 5;
    public const int c_gun_pistolAmmo = 50;
    public const float c_gun_pistolFireRate = 0.25f;
    // machine gun variables
    public const int c_gun_machineGunDamage = 7;
    public const int c_gun_machineGunAmmo = 100;
    public const float c_gun_machineGunFireRate = 0.1f;


    //pick up variables
    public const int c_pickUp_pistolAmmoAmount = 10;
    public const int c_pickUp_armourAmount = 10;
}
