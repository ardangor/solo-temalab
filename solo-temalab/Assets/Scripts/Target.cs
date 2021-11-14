using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public event System.Action OnGetHit;
    public event System.Action OnDie;

    public bool isDead = false;

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(transform.name + " is taking " + amount + " of damage!");

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
            
        if (OnGetHit != null)
            OnGetHit();
    }

    public virtual void Die()
    {
        //Override this in children
        if (OnDie != null)
            OnDie();
    }
}
