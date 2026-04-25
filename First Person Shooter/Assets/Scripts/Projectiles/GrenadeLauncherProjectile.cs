using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GrenadeLauncherProjectile : ExplosiveProjectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Awake()
    {
        base.Awake();

        speed = Constants.c_grenadeLauncher_projSpeed;

        splashDamage = Constants.c_grenadeLauncher_splashDamage;

        timeToDestroy = GetTimeToDestroy();
    }

    public override void ApplyForce()
    {
        direction = player.playerCamera.transform.forward;
        Vector3 launchDirection = (direction + new Vector3(0.0f, 0.3f, 0.0f)).normalized;
        rb.AddForce(launchDirection * speed, ForceMode.Impulse);
    }

    public override void Activate(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        ApplyForce();
    }

    protected override float GetTimeToDestroy()
    {
        return Constants.c_grenadeLauncher_timeToDestroyProj;
    }
}
