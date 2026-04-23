using UnityEngine;

public class AcceleratorProjectile : MonoBehaviour
{
    PlayerController player;

    float speed;
    Vector3 direction;
    float timeToDestroy;
    int damage = Constants.c_accelerator_damage;

    public int indexForDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        direction = Quaternion.Euler(0, indexForDirection * Constants.c_accelerator_projSpread, 0) * player.playerCamera.transform.forward;
        
        speed = Constants.c_accelerator_projSpeed;

        damage = Constants.c_accelerator_damage;

        timeToDestroy = Constants.c_accelerator_timeToDestroyProj;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0.0f)
        {
            this.gameObject.SetActive(false); // add this to an object pool
        }
    }

    public void Activate(Vector3 position, int indexForDirection)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        direction = Quaternion.Euler(0, indexForDirection * Constants.c_accelerator_projSpread, 0) * player.playerCamera.transform.forward;
        
        speed = Constants.c_accelerator_projSpeed;

        damage = Constants.c_accelerator_damage;

        timeToDestroy = Constants.c_accelerator_timeToDestroyProj;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            enemy.DoDamage(damage);

            this.gameObject.SetActive(false); // add this to an object pool
        }
        else if (other.gameObject.CompareTag("Dumpster"))
        {
            GameObject dumpster = other.gameObject;

            dumpster.GetComponent<Rigidbody>().AddForce(player.playerCamera.transform.forward * 2.0f, ForceMode.Impulse);

            this.gameObject.SetActive(false); // add this to an object pool
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Default")) // WHY DOES THIS NOT WORK
        {
            this.gameObject.SetActive(false); // add this to an object pool
        }
    }
}
