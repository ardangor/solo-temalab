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

    public int scoreValue = 10;

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

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, health);
        }
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
            Score.Instance.addScore(scoreValue);
            Die();
        }

        if (OnGetHit != null && !isDead)
            StartCoroutine(delayedAnimation());
    }

    IEnumerator delayedAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        OnGetHit();
    }

    public virtual void Die()
    {
        //Override this in children
        if (OnDie != null)
            OnDie();
    }
}
