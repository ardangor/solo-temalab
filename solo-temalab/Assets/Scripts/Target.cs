using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float maxHealth = 50f;
    float health;

    public event System.Action OnGetHit;
    public event System.Action OnDie;

    public bool isDead = false;

    public event System.Action<float, float> OnHealthChanged;

    void Start()
    {
        health = maxHealth;
    }

    public void setHealth(float amount)
    {
        Debug.Log("Player has been healed by " + amount + " from " + health + " to " + (health + amount));
        health += amount;    

        if (health > maxHealth)
            health = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;

        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, health);
        }

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
