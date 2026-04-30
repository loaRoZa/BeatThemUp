using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; //camaras virtuales


public class VirtualCameraController : MonoBehaviour
{
    private CinemachineVirtualCamera estaCamara;

    [SerializeField] private int prioridadDefault = 10;

    //indica si esta cámara es la del jugador
    [SerializeField] private bool esCamaraPrincipal = false;

        void Awake()
    {
        estaCamara = GetComponent<CinemachineVirtualCamera>();
    }

    private void ManejaCambioCamara(CinemachineVirtualCamera camaraNueva)
    {
        //preguntamos si esta es la camara que si quiere usar
        if(camaraNueva == estaCamara)
        {
            //ponemos prioridad alta
            estaCamara.Priority = 20;
        }
        else if (camaraNueva == null && esCamaraPrincipal)
        {
            //no se asigno camara y esta es la principal
            estaCamara.Priority = 20;
            
        }
        else
        {
            //si llega a este punto es que no fue seleccionada esta camara
            estaCamara.Priority = prioridadDefault;
        }
    }

    private void OnEnable()
    {
        CameraEvents.OnCameraChange += ManejaCambioCamara;
    }

    private void OnDisable()
    {
        CameraEvents.OnCameraChange -= ManejaCambioCamara;
    }
}
