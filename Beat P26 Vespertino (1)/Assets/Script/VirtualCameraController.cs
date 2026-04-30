using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // <- para cámaras virtuales

/// Este script lo tendrán TODAS las cámaras virtuales
public class VirtualCameraController : MonoBehaviour
{
	private CinemachineVirtualCamera estaCamara;
	
	[SerializeField] private int prioridadDefault = 10;
	
	// Indica si esta cámara es la del jugador
	[SerializeField] private bool esCamaraPrincipal = false;
	
	void Awake()
	{
		estaCamara = GetComponent<CinemachineVirtualCamera>();
	}
	
	private void ManejaCambioCamara(CinemachineVirtualCamera camaraNueva)
	{
		// Verificamos si esta es la cámara que si quiere usar
		if(camaraNueva == estaCamara)
		{
			// Ponemos prioridad alta
			estaCamara.Priority = 20;
		}
		else if(camaraNueva == null && esCamaraPrincipal)
		{
			// no se asigno una cámara y esta es la principal
			estaCamara.Priority = 20;
		}
		else
		{
			// si llega a este punto, es que no fue seleccionada esta cámara
			estaCamara.Priority = prioridadDefault;
		}
	}
	
	private void OnEnable()
	{
		// suscribimos nuestro método de cámara al evento de cambio de camara
		CameraEvents.OnCameraChange += ManejaCambioCamara;
	}
	
	private void OnDisable()
	{
		CameraEvents.OnCameraChange -= ManejaCambioCamara;
	}
}
