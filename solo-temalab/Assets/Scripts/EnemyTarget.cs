using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : Target
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject, 2f);
    }
}
