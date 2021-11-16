using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUIForPlayer : MonoBehaviour
{
    public Transform ui;
    Image healthSlider;
    
    void Start()
    {
        GetComponent<Target>().OnHealthChanged += OnHealthChanged;
        healthSlider = ui.GetChild(0).GetComponent<Image>();
    }

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        float healthPercent = currentHealth / maxHealth;
        healthSlider.fillAmount = healthPercent;       
    }  
}
