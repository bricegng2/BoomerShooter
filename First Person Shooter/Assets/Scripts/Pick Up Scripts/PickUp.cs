using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    protected PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    protected abstract void ApplyPickUpEffect();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ApplyPickUpEffect();
            this.gameObject.SetActive(false); // add this to an object pool
        }
    }
}
