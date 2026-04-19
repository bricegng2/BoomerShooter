using UnityEngine;

public class Key : MonoBehaviour
{
    public KeyData keyData;

    PlayerController player;

    float rotationSpeed = 75.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        if (meshRenderer != null && keyData != null)
        {
            meshRenderer.material = keyData.keyMaterial;
        }

        player = FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);

        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * 0.2f + 0.7f, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DataManager.Instance.inventoryManager.keys.Add(keyData);

            player.playerHUD.UpdateKeys();

            this.gameObject.SetActive(false); // maybe add this to an object pool
        }
    }
}
