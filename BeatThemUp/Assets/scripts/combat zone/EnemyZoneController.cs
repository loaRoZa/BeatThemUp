using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyZoneController : MonoBehaviour
{
    private EnemySpawner enemySpawner;

    //La camara preconfigurada para esta zona de combate
    private CinemachineVirtualCamera zoneCamera;

    //cuantos enemigos queremos en la zona
    [SerializeField] private int enemiesToSpawn;
        //cada cuanto tiempo
    [SerializeField] private float spawnInterval;

    private bool isBlocked = false;

   void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        zoneCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }
    void OnTriggerEnter(Collider col)
    {
        //si el jugador pasa por el sensor
        if (col.gameObject.tag == "Player" && ! isBlocked)
        {
            //Inicia el bloqueo de la zona
            isBlocked = true;

            //bloquear camara
            CameraEvents.BlockCamera();

            //cambiar la cámara de esta zona
            CameraEvents.ActivarCambioCamara(zoneCamera);

            //aparecer enemigos
            enemySpawner.SpawnEnemies(enemiesToSpawn, spawnInterval);

            //monitorear si el jugador termina con los enemigos
            StartCoroutine(CheckIfEnemiesAreDefeated());
        }
    }
    
    private IEnumerator CheckIfEnemiesAreDefeated ()
    {
        //todo el tiempo´preguntamos si quedaron enemigos 
        while(true)
        {
            if(enemySpawner.EstanEnemigosDerrotados())
            {
                //desbloqueamos el avance del jugador
                CameraEvents.UnblockCamera();

                //regresar a la camara principal
                CameraEvents.ActivarCambioCamara(null);

                //en cuanto el jugador supera esta zona ya no me es util
                gameObject.SetActive(false);

                break; //<- Rompe el ciclo while
            }
            yield return new WaitForSeconds(2f);
        }
    }

    
}
