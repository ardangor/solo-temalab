using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : Target
{
    public Rigidbody r1;
    public float despawnRate = 1f;
    public override void Die()
    {
        base.Die();
        //Rigidbody r = transform.GetComponent<Rigidbody>();
        //Rigidbody r1 = transform.GetComponentInChildren<Rigidbody>();

        //if (r != null)
          //  r.isKinematic = false;

        if (r1 != null)
            r1.isKinematic = false;

        Destroy(gameObject, despawnRate);
    }
}
