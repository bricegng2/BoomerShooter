using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    float rotationSpeed = 2000.0f;
    float speed = 20.0f;
    Vector3 direction;

    bool hasCollided = false;
    float timeToDeactivate = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = transform.forward + (transform.up * 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided == true)
        {
            timeToDeactivate -= Time.deltaTime;
            if (timeToDeactivate <= 0.0f)
            {
                this.gameObject.SetActive(false); // add this to an object pool
            }
            return;
        }

        transform.position += direction * speed * Time.deltaTime;

        transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        hasCollided = true;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.DoDamage(enemy.health); // one hit kill
        }
    }
}
