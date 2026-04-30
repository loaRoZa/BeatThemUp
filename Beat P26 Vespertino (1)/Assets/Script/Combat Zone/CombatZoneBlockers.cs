using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZoneBlockers : MonoBehaviour
{
	// Los objetos "paredes" que bloquean el paso
	[SerializeField] private GameObject[] bloqueadores;
	
	private void ManejaCamaraBlock()
	{
		// Activar las paredes que bloquean
		for(int b = 0; b < bloqueadores.Length; b++)
		{
			bloqueadores[b].SetActive(true);
		}
	}
	
	private void ManejaCamaraUnblock()
	{
		// Quita las paredes
		for(int contador = 0; contador < bloqueadores.Length; contador++)
			bloqueadores[contador].SetActive(false);
	}
	
	private void OnEnable()
	{
		// suscribir a los eventos de la cámara
		CameraEvents.OnCameraBlock += ManejaCamaraBlock;
		CameraEvents.OnCameraUnblock += ManejaCamaraUnblock;
	}
	
	private void OnDisable()
	{
		// dessucribir
		CameraEvents.OnCameraBlock -= ManejaCamaraBlock;
		CameraEvents.OnCameraUnblock -= ManejaCamaraUnblock;
	}
}
