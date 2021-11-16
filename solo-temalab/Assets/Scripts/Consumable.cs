using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public float healAmount = 10f;
    private void OnTriggerEnter(Collider other)
    {
        PlayerTarget target = other.GetComponent<PlayerTarget>();

        if(target != null)
        {
            target.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
