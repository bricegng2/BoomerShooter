using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    public InputAction inputAction;

    Vector3 position;
    float speed = 10.0f;
    Vector2 input = Vector2.zero;

    void OnEnable()
    {
        inputAction.Enable();
    }

    void OnDisable()
    {
        inputAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position.x += speed * Time.deltaTime * input.x;
        position.z += speed * Time.deltaTime * input.y;
        transform.position = position;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

}
