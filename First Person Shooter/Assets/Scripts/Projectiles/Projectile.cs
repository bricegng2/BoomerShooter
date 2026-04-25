using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    protected PlayerController player;
    
    protected float speed;
    protected Vector3 direction;
    protected float timeToDestroy;
    protected int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        direction = player.playerCamera.transform.forward;

        timeToDestroy = GetTimeToDestroy();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0.0f)
        {
            timeToDestroy = GetTimeToDestroy();
            this.gameObject.SetActive(false);
        }
    }

    public abstract void Activate(Vector3 position);

    protected abstract float GetTimeToDestroy();
}
