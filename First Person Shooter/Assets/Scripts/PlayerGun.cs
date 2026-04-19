using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    int damage = 5;

    int ammo = 50;

    PlayerHUDController playerHUD;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHUD = FindAnyObjectByType<PlayerHUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetDamage()
    {
        if (ammo > 0)
        {
            return damage;
        }

        return 0;
    }

    public void Shoot()
    {
        if (ammo > 0)
        {
            ModAmmo(-1);
        }
    }

    public void ModAmmo(int modAmount)
    {
        ammo += modAmount;
        if (ammo < 0)
        {
            ammo = 0;
        }
        else if (ammo > 999)
        {
            ammo = 999;
        }
        playerHUD.UpdateAmmo(ammo);
    }

    public int GetAmmo()
    {
        return ammo;
    }
}
