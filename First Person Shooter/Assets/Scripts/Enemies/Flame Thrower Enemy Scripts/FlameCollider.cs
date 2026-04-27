using UnityEngine;

public class FlameCollider : MonoBehaviour
{
    public BoxCollider damageZone;

    float timerToDamage;
    public bool canDamage = false;

    PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        timerToDamage = Constants.c_flameThrowerEnemy_fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDamage == true && player.currentStatusEffect == EStatusEffect.None)
        {
            timerToDamage -= Time.deltaTime;
            if (timerToDamage <= 0.0f)
            {
                player.DoDamage(Constants.c_flameThrowerEnemy_fireDamage);
                timerToDamage = Constants.c_flameThrowerEnemy_fireRate;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canDamage = true;
            
            player.currentStatusEffect = EStatusEffect.None;
            player.DoDamage(Constants.c_flameThrowerEnemy_fireDamage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canDamage = false;

            player.currentStatusEffect = EStatusEffect.Burning;
            player.DoDamage(Constants.c_burning_damage);
        }
    }
}
