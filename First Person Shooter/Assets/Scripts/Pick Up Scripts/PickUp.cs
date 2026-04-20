using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    protected PlayerController player;

    float rotationSpeed = 75.0f;

    protected bool isPickedUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);

        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * 0.2f + 0.7f, transform.position.z);
    }

    protected abstract void ApplyPickUpEffect();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ApplyPickUpEffect();
            
            if (isPickedUp == true)
            {
                this.gameObject.SetActive(false); // add this to an object pool
            }
        }
    }
}
