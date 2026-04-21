using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 position;
    float speed = 10.0f;
    Vector2 input = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    [SerializeField]
    Rigidbody playerRigidbody;

    public Vector3 jump;
    public float jumpForce = 10.0f;
    public bool isGrounded;

    float mouseXSensitivity = 15.0f;
    float mouseYSensitivity = 15.0f;
    Vector2 playerRotation = Vector2.zero;
    Vector2 verticalLookBounds = new Vector2(-85, 85);
    public Camera playerCamera;

    public PlayerGun gun;

    public int health = 100;
    public int armour = 0;
    public PlayerHUDController playerHUD;

    public GameObject throwingKnifePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        jump = new Vector3(0.0f, 3.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // --------------------
        // movement
        Vector3 moveDirection = (transform.right * input.x) + (transform.forward * input.y);
        moveDirection.y = 0.0f;
        transform.position += moveDirection * speed * Time.deltaTime;
        // movement
        // --------------------

        // --------------------
        // look rotation
        if (lookInput != Vector2.zero)
        {
            float xMovement = lookInput.x * mouseXSensitivity * Time.deltaTime * 2.0f;
            float yMovement = lookInput.y * mouseYSensitivity * Time.deltaTime * 2.0f;

            playerRotation.x -= yMovement;
            playerRotation.x = Mathf.Clamp(playerRotation.x, verticalLookBounds.x, verticalLookBounds.y);

            playerRotation.y += xMovement;

            transform.rotation = Quaternion.Euler(0, playerRotation.y, 0);
            playerCamera.transform.localRotation = Quaternion.Euler(playerRotation.x, 0, 0);
        }
        // look rotation
        // --------------------
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
            if (!DataManager.Instance.inventoryManager.guns.Find(gun => gun.gunType == gunType).gameObject.activeSelf)
            {
                Debug.Log("gun not picked up: " + gunType.ToString());
                return;
            }

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
    }

    public void OnThrowKnife(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (DataManager.Instance.inventoryManager.throwingKnives > 0)
        {
            Instantiate(throwingKnifePrefab, GameObject.FindGameObjectWithTag("ThrowingKnifeThrowPos").transform.position + playerCamera.transform.forward, playerCamera.transform.rotation);
            DataManager.Instance.inventoryManager.throwingKnives--;
            Debug.Log("threw knife, remaining knives: " + DataManager.Instance.inventoryManager.throwingKnives);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
