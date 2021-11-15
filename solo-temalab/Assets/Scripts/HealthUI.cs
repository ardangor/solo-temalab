using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Target))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;

    Transform ui;
    Image healthSlider;

    Transform cam;

    public float visibleTime = 3f;
    float lastVisibleTime; 
    void Start()
    {
        cam = Camera.main.transform;
        foreach(Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                break;
            }
        }

        ui.gameObject.SetActive(false);
        GetComponent<Target>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if(ui != null)
        {
            ui.gameObject.SetActive(true);
            lastVisibleTime = Time.time;

            float healthPercent = currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;

            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        } 
    } 

    void LateUpdate()
    {
        if(ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastVisibleTime > visibleTime)
                ui.gameObject.SetActive(false);
        }   
    }
}
