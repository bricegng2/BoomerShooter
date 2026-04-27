using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 position;
    Vector3 velocity = Vector3.zero;
    Vector2 input = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    [SerializeField]
    float speed = 0.0f;
    [SerializeField]
    float acceleration = 0.0f;
    [SerializeField]
    float deceleration = 0.0f;
    Vector3 targetVelocity = Vector3.zero;
    Vector3 newTargetVelocity = Vector3.zero;
    

    [SerializeField]
    Rigidbody playerRigidbody;

    public Vector3 jump;
    public float jumpForce = 10.0f;
    public bool isGrounded;

    float mouseXSensitivity = 4.0f;
    float mouseYSensitivity = 4.0f;
    Vector2 playerRotation = Vector2.zero;
    Vector2 verticalLookBounds = new Vector2(-85, 85);
    public Camera playerCamera;

    public PlayerGun gun;

    public int health = 100;
    public int armour = 0;
    public PlayerHUDController playerHUD;

    public ThrowingKnife throwingKnifePrefab;

    float tiltAngle = 4.0f;
    float tiltSpeed = 5.0f;
    float currentTilt = 0f;

    public ObjectPooling throwingKnifeObjectPool;

    Vector3 moveDirection = Vector3.zero;

    Quaternion targetRotation = Quaternion.identity;

    public Parry parry;

    public float parryCooldown;
    public bool canParry;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;

        playerRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        playerRigidbody.freezeRotation = true;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        jump = new Vector3(0.0f, 4.0f, 0.0f);

        parryCooldown = Constants.c_parry_cooldown;

        canParry = true;
    }

    // Update is called once per frame
    void Update()
    {
        // --------------------------------------------------------------------------------
        // look rotation
        float xMovement = lookInput.x * mouseXSensitivity * Time.deltaTime * 2.0f;
        float yMovement = lookInput.y * mouseYSensitivity * Time.deltaTime * 2.0f;

        playerRotation.x -= yMovement;
        playerRotation.x = Mathf.Clamp(playerRotation.x, verticalLookBounds.x, verticalLookBounds.y);

        playerRotation.y += xMovement;
        // look rotation
        // --------------------------------------------------------------------------------


        // --------------------------------------------------------------------------------
        // camera tilt
        float targetZ = -input.x * tiltAngle;

        currentTilt = Mathf.Lerp(currentTilt, targetZ, Time.deltaTime * tiltSpeed);

        targetRotation = Quaternion.Euler(0, playerRotation.y, currentTilt);
        playerCamera.transform.localRotation = Quaternion.Euler(playerRotation.x, 0, 0);

        playerRigidbody.MoveRotation(targetRotation);
        // camera tilt
        // --------------------------------------------------------------------------------

        if (canParry == false)
        {
            parryCooldown -= Time.deltaTime;
            if (parryCooldown <= 0.0f)
            {
                canParry = true;
                parryCooldown = Constants.c_parry_cooldown;
            }
        }
    }

    void FixedUpdate()
    {
        // --------------------------------------------------------------------------------
        // movement
        moveDirection = (transform.right * input.x) + (transform.forward * input.y);
        moveDirection.y = 0.0f;
        if (moveDirection.sqrMagnitude > 1.0f)
        {
            moveDirection.Normalize();
        }

        targetVelocity = moveDirection * speed;

        float accelRate;
        if (moveDirection.sqrMagnitude > 0.0f)
        {
            accelRate = acceleration;
        }
        else
        {
            accelRate = deceleration;
        }

        Vector3 currentVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0.0f, playerRigidbody.linearVelocity.z);
        Vector3 velocityDelta = targetVelocity - currentVelocity;

        Vector3 clampedVelocityDelta = Vector3.ClampMagnitude(velocityDelta, accelRate * Time.fixedDeltaTime);
        playerRigidbody.AddForce(clampedVelocityDelta, ForceMode.VelocityChange);
        // movement
        // --------------------------------------------------------------------------------
    }

    public void DoDamage(int damage)
    {
        if (armour > 0)
        {
            armour -= damage;
            if (armour <= 0)
            {
                health += armour;
                playerHUD.UpdateHealth(health);
                armour = 0;
            }
            playerHUD.UpdateArmour(armour);
            return;
        }

        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
        playerHUD.UpdateHealth(health);
    }

    public void ModArmour(int modAmount)
    {
        armour += modAmount;
        playerHUD.UpdateArmour(armour);
    }

    public void SwitchGun(EGunType gunType)
    {
        for (int i = 0; i < DataManager.Instance.inventoryManager.guns.Count; i++)
        {
            if (DataManager.Instance.inventoryManager.guns[i].gunType == gunType)
            {
                DataManager.Instance.inventoryManager.guns[i].gameObject.SetActive(true);
                gun = DataManager.Instance.inventoryManager.guns[i];
                Debug.Log("switched to " + gunType.ToString());

                gun.isSelected = true;

                for (int j = 0; j < DataManager.Instance.inventoryManager.guns.Count; j++)
                {
                    if (DataManager.Instance.inventoryManager.guns[j].gunType != gunType)
                    {
                        DataManager.Instance.inventoryManager.guns[j].isSelected = false;
                        DataManager.Instance.inventoryManager.guns[j].isShooting = false;
                    }
                }

                playerHUD.UpdateAmmo(gun.GetAmmo());
                playerHUD.UpdateGunIcons();
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // add bunny hopping
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (isGrounded == true)
        {
            playerRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        gun.PerformShoot(context);
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        gun.PerformRightClick(context);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, 10.0f))
        {
            if (hitInfo.collider.CompareTag("Door"))
            {
                Door door = hitInfo.collider.GetComponent<Door>();

                for (int i = 0; i < DataManager.Instance.inventoryManager.keys.Count; i++)
                {
                    if (DataManager.Instance.inventoryManager.keys[i].keyType == (EKeyType)door.doorData.doorType)
                    {
                        door.doorState = EDoorState.Opening;
                    }
                }
                
                if (door.doorData.doorType == EDoorType.End)
                {
                    door.doorState = EDoorState.Opening;

                    // show UI for end of level
                }
            }
        }
    }

    public void OnSwitchGun(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        
        int gunIndex = context.control.name[context.control.name.Length - 1] - '0'; // get the last character of the control name and convert it to an integer

        if (gunIndex == 1)
        {
            SwitchGun(EGunType.Pistol);
        }
        else if (gunIndex == 2)
        {
            SwitchGun(EGunType.MachineGun);
        }
        else if (gunIndex == 3)
        {
            SwitchGun(EGunType.Shotgun);
        }
        else if (gunIndex == 4)
        {
            SwitchGun(EGunType.Accelerator);
        }
        else if (gunIndex == 5)
        {
            SwitchGun(EGunType.RocketLauncher);
        }
        else if (gunIndex == 6)
        {
            SwitchGun(EGunType.GrenadeLauncher);
        }
    }

    public void OnThrowKnife(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (DataManager.Instance.inventoryManager.throwingKnives > 0)
        {
            GameObject potentialKnife = throwingKnifeObjectPool.GetPooledObject();

            if (potentialKnife == null)
            {
                ThrowingKnife knife = Instantiate(throwingKnifePrefab, GameObject.FindGameObjectWithTag("ThrowingKnifeThrowPos").transform.position + playerCamera.transform.forward, playerCamera.transform.rotation);
                throwingKnifeObjectPool.AddObjectToPool(knife.gameObject);
            }
            else if (potentialKnife != null)
            {
                ThrowingKnife knife = potentialKnife.GetComponent<ThrowingKnife>();
                knife.Activate(this);
                potentialKnife.SetActive(true);
            }
            
            DataManager.Instance.inventoryManager.throwingKnives--;
            Debug.Log("threw knife, remaining knives: " + DataManager.Instance.inventoryManager.throwingKnives);
        }
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (canParry == true)
        {
            parry.PerformParry();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
