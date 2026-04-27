using UnityEngine;

public class Parry : MonoBehaviour
{
    public BoxCollider parryCollider;

    float parryDuration;

    PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        parryCollider.enabled = false;

        parryDuration = Constants.c_parry_duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (parryCollider.enabled == true)
        {
            parryDuration -= Time.deltaTime;
            if (parryDuration <= 0)
            {
                ResetParry();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (parryCollider.enabled == false)
        {
            return;
        }
    }

    public void PerformParry()
    {
        Time.timeScale = 0.5f;
        parryCollider.enabled = true;
        player.canParry = false;
        player.parryCooldown = Constants.c_parry_cooldown;
    }

    public void ResetParry()
    {
        Time.timeScale = 1.0f;
        parryCollider.enabled = false;
        parryDuration = Constants.c_parry_duration;
    }
}
