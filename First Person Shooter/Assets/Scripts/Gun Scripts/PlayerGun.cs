using UnityEngine;
using UnityEngine.InputSystem;

public enum EGunType
{
    Pistol,
    MachineGun,
    Shotgun,
}

public abstract class PlayerGun : MonoBehaviour
{
    protected int damage; 

    protected int ammo; // move the ammo stuff into the inventory manager later

    protected PlayerController player;

    protected PlayerHUDController playerHUD;

    protected bool isShooting = false;

    protected float fireRate;

    [SerializeField] protected LayerMask shootLayerMask = ~0;

    public EGunType gunType;

    public bool isSelected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        playerHUD = player.playerHUD;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            fireRate -= Time.deltaTime;
            if (fireRate <= 0.0f)
            {
                Shoot();
                fireRate = ResetFireRate();
            }
        }
    }

    public void PerformShoot(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            fireRate = 0.0f;
            isShooting = true;
        }
        else if (context.canceled)
        {
            isShooting = false;
        }
    }

    public virtual void Shoot()
    {
        if (ammo > 0)
        {
            ModAmmo(-1);

            if (Physics.Raycast(player.playerCamera.transform.position, player.playerCamera.transform.forward, out RaycastHit hitInfo, 100.0f, shootLayerMask, QueryTriggerInteraction.Ignore))
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    Enemy enemy = hitInfo.collider.GetComponent<Enemy>();

                    enemy.DoDamage(damage);
                }
            }
        }
    }

    protected abstract float ResetFireRate();

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