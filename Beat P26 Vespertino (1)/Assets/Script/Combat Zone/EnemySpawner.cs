using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject prefabEnemigo;
	
	// Puntos desde donde aparecen los enemigos
	[SerializeField] private Transform[] spawnPoints;
	
	private List<GameObject> activeEnemies;
	
	void Awake()
	{
		activeEnemies = new List<GameObject>();
	}
	
	public void SpawnEnemies(
		int numeroEnemigos, float intervalo)
	{
		StartCoroutine(
			SpawnEnemyWave(numeroEnemigos,intervalo));
	}
	
	private IEnumerator SpawnEnemyWave(int n, float i)
	{
		for(int cont = 0; cont < n; cont++)
		{
			// Elegimos un spawn point al azar
			int punto = Random.Range(0, spawnPoints.Length);
			
			Transform puntoElegido = spawnPoints[punto];
			
			// Creamos al enemigo
			GameObject nuevoEnemigo = 
				Instantiate(
					prefabEnemigo,
					puntoElegido.position,
					Quaternion.identity);
					
			// lo agregamos a la lista de enemigos activos
			activeEnemies.Add(nuevoEnemigo);
			
			// hacemos una pausa antes de crear el siguiente
			yield return new WaitForSeconds(i);
		}
	}
	
	public bool EstanEnemigosDerrotados()
	{
		// limpio la lista de enemigos quitando
		// los que hayan sido destruidos
		activeEnemies.RemoveAll(enemigo => enemigo == null);
		
		return activeEnemies.Count == 0;
	}
}
