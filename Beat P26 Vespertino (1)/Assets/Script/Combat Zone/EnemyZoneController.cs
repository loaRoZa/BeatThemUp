using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // <- para cámaras virtuales

public class EnemyZoneController : MonoBehaviour
{
	private EnemySpawner enemySpawner;
	
	// La cámara preconfigurada para esta zona de combate
	private CinemachineVirtualCamera zoneCamera;
	
	// Cuantos enemigos queremos en la zona
	[SerializeField] private int enemiesToSpawn;
	// Cada cuanto tiempo
	[SerializeField] private float spawnInterval;
	
	private bool isBlocked = false;
	
	void Awake()
	{
		enemySpawner = GetComponent<EnemySpawner>();
		zoneCamera = GetComponentInChildren<CinemachineVirtualCamera>();
	}
	
	void OnTriggerEnter(Collider col)
	{
		// Si el jugador pasa por el sensor
		if(col.gameObject.tag == "Player" &&
			! isBlocked)
		{
			// inicia el bloqueo de la zona
			isBlocked = true;
			
			// Bloquear cámara
			CameraEvents.BlockCamera();
			
			// Cambiar a la cámara de esta zona
			CameraEvents.ActivarCambioCamara(zoneCamera);
			
			// Aparecer enemigos
			enemySpawner.SpawnEnemies(enemiesToSpawn, spawnInterval);
			
			// Monitorear si el jugador termina con los enemigos
			StartCoroutine(CheckIfEnemiesAreDefeated());
		}
	}
	
	private IEnumerator CheckIfEnemiesAreDefeated()
	{
		// todo el tiempo preguntamos si quedan enemigos
		while(true)
		{
			if(enemySpawner.EstanEnemigosDerrotados())
			{
				// Desbloqueamos el avance del jugador
				CameraEvents.UnblockCamera();
				
				// regresar a la cámara principal
				CameraEvents.ActivarCambioCamara(null);
				
				// en cuanto el jugador supera esta zona
				// ya no me es útil
				gameObject.SetActive(false);
				
				break; // <- Rompe el ciclo while
			}
			// esperamos 2 segundos antes de volver a checar
			yield return new WaitForSeconds(2f);
		}
	}
}
