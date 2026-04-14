using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 position;
    float speed = 10.0f;
    Vector2 input = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    float mouseX = 25.0f;
    float mouseY = 25.0f;
    Vector2 playerRotation = Vector2.zero;
    Vector2 verticalLookBounds = new Vector2(-85, 85);
    public Camera playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = true;
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
            float xMovement = lookInput.x * mouseX * Time.deltaTime * 2.0f;
            float yMovement = lookInput.y * mouseY * Time.deltaTime * 2.0f;

            playerRotation.x -= yMovement;
            playerRotation.x = Mathf.Clamp(playerRotation.x, verticalLookBounds.x, verticalLookBounds.y);

            playerRotation.y += xMovement;

            transform.rotation = Quaternion.Euler(0, playerRotation.y, 0);
            playerCamera.transform.localRotation = Quaternion.Euler(playerRotation.x, 0, 0);
        }
        // look rotation
        // --------------------
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Left Clicked");
        }
    }
}
