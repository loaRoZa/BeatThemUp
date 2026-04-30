using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemigo;

    //puntos desde donde aparecen los enemigos
    [SerializeField] private Transform[] spawnPoints;

    private List<GameObject> activeEnemies;

   void Awake()
    {
        activeEnemies = new List<GameObject>();
    }
     public void SpawnEnemies (int numeroEnemigos, float intervalo)
    {
        StartCoroutine(SpawnEnemyWave(numeroEnemigos, intervalo));
    }

    private IEnumerator SpawnEnemyWave (int n, float i)
    {
        for(int cont = 0; cont < n; cont++)
        {
            //elegimos un spawn point al azar
            int punto = Random.Range(0, spawnPoints.Length);

            Transform puntoElegido = spawnPoints[punto];

            //creamos al objetivo
            GameObject nuevoEenemigo = Instantiate(prefabEnemigo, puntoElegido
                .position, Quaternion.identity);

            //lo agregamos a la lista de enemigos activa
            activeEnemies.Add(nuevoEenemigo);

            //hacemos una pausa antes de crear el sig.
            yield return new WaitForSeconds(i);
        }
    }

    public bool EstanEnemigosDerrotados()
    {
        //limpio la ista de enemigos quitando los que hayan sidon derrotados
        activeEnemies.RemoveAll(enemigo => enemigo == null);

        return activeEnemies.Count == 0;
    }
}
