using UnityEngine;

public static class Constants
{
    //enemy variables
    public const float c_enemy_projFireRate = 5.0f;
    public const float c_enemy_projSpeed = 5.0f;
    public const float c_enemy_timeToDestroyProj = 5.0f;
    public const float c_enemy_minDistanceToPlayerWhenPlayerDest = 25.0f;
    public const float c_enemy_minDistanceToPlayerWhenRandomDest = 13.0f;
    public const float c_enemy_timeToSwitchDestination = 2.0f;
    public const int c_enemy_projDamage = 5;
    public const int c_enemy_baseHealth = 500; // 30
    public const float c_enemy_timerToResetMaterial = 0.1f;


    //player variables


    // gun variables
    public const int c_gun_defaultDamage = 0;
    public const int c_gun_defaultAmmo = 0;
    public const float c_gun_defaultFireRate = 0f;
    // pistol variables
    public const int c_pistol_damage = 5;
    public const int c_pistol_ammo = 50;
    public const float c_pistol_fireRate = 0.25f;
    // machine gun variables
    public const int c_machineGun_damage = 7;
    public const int c_machineGun_ammo = 100;
    public const float c_machineGun_fireRate = 0.1f;
    // shotgun variables
    public const int c_shotgun_damage = 10;
    public const int c_shotgun_ammo = 20;
    public const float c_shotgun_fireRate = 1.0f;
    // accelerator variables
    public const int c_accelerator_damage = 6;
    public const int c_accelerator_ammo = 300;
    public const float c_accelerator_fireRate = 0.2f;
    public const float c_accelerator_projSpeed = 25.0f;
    public const float c_accelerator_timeToDestroyProj = 5.0f;
    public const float c_accelerator_projSpread = 7.0f;
    // rocket launcher variables
    public const int c_rocketLauncher_damage = 20;
    public const int c_rocketLauncher_ammo = 500;
    public const float c_rocketLauncher_fireRate = 1.5f;
    public const float c_rocketLauncher_projSpeed = 30.0f;
    public const float c_rocketLauncher_timeToDestroyProj = 5.0f;
    public const float c_rocketLauncher_explosionRadius = 3.0f;


    //pick up variables
    public const int c_pickUp_pistolAmmoAmount = 10;
    public const int c_pickUp_armourAmount = 10;
}
