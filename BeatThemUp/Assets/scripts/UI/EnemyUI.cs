using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // para trabajar con objetos de UI
using UnityEngine;
using System;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    void Start()
    {
        if (healthSystem != null)
        {
            // suscribimos una función al evento de recibir daño
            healthSystem.OnDamaged += HS_OnChanged;
        }
    }

    private void HS_OnChanged(object sender, EventArgs e)
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
}
