using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZoneBlockers : MonoBehaviour
{
    //los objetos "paredes" que bloquean el paso
    [SerializeField] private GameObject[] bloqueadores;

    private void ManejaCamaraBlock()
    {
        //Activar las paredes que bloquean
        for(int b = 0; b< bloqueadores.Length;b++)
        {
            bloqueadores[b].SetActive(true);
        }
    }
        
    private void ManejaCamaraUnblock()
    {
        //quita las paredes
        for (int contador = 0; contador < bloqueadores.Length; contador++)
            bloqueadores[contador].SetActive(false);
    }

    private void OnEnable()
    {
        //suscribir a los eventos de la camara
        CameraEvents.OnCameraBlock += ManejaCamaraBlock;
        CameraEvents.OnCameraUnblock += ManejaCamaraUnblock;
    }

    private void OnDisable()
    {
        //desuscribir
        CameraEvents.OnCameraBlock -= ManejaCamaraBlock;
        CameraEvents.OnCameraUnblock -= ManejaCamaraUnblock;
    }


}
