using System;
using UnityEngine;

public class EnemyHeadCollider : MonoBehaviour
{
    [SerializeField] EnemyBase parent;

    public void DoDamage(int damage)
    {
        parent.DoDamage(damage);
    }
}
