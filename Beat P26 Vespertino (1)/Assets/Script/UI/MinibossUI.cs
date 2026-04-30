using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // para trabajar con objetos de UI
using System;

public class MinibossUI : MonoBehaviour
{

    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

	void Awake()
	{
		gameObject.SetActive(false);
		
		// nos suscribimos al evento de que el jugador llega
		// a la zona del miniboss
		MiniBossSensor.OnPlayerGetToMiniboss += MinibossShowUI;
	}
	
	private void MinibossShowUI()
	{
		gameObject.SetActive(true);
	}
	

    void Start()
    {
        if(healthSystem != null)
        {
            // suscribimos una función al evento de recibir daño
            healthSystem.OnDamaged += HS_OnChanged;
			healthSystem.OnDie += HS_OnDead;
        }
    }
	
	private void HS_OnDead(object sender, EventArgs e)
	{
		MinibossDead();
	}
	
	private void MinibossDead()
	{
		gameObject.SetActive(false);
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
