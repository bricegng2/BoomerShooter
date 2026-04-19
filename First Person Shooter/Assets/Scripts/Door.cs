using UnityEngine;

public enum EDoorState
{
    Closed,
    Opening,
    Opened,
}

public class Door : MonoBehaviour
{
    public DoorData doorData;

    PlayerController player;

    public EDoorState doorState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        if (meshRenderer != null && doorData != null)
        {
            meshRenderer.material = doorData.doorMaterial;
        }

        player = FindAnyObjectByType<PlayerController>();

        doorState = EDoorState.Closed;
    }

    void Update()
    {
        if (doorState != EDoorState.Opening)
        {
            return;
        }

        if (transform.position.y < 3.75f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 2.0f, transform.position.z);
            
            if (transform.position.y > 3.75f)
            {
                transform.position = new Vector3(transform.position.x, 3.75f, transform.position.z);
                doorState = EDoorState.Opened;
            }
        }
    }
}
