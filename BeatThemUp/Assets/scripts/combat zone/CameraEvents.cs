using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CameraEvents
{
    public static event Action OnCameraBlock;
    public static event Action OnCameraUnblock;

    //evento para cambio de camara
    public static event Action<Cinemachine.CinemachineVirtualCamera> OnCameraChange;

    //metodos que permiten llamar a los eventos
    public static void BlockCamera()
    {
        OnCameraBlock?.Invoke();
    }
    
    public static void UnblockCamera()
    {
        OnCameraUnblock?.Invoke();
    }

    //metodo para invocar evento de cambiar camara
    public static void ActivarCambioCamara(Cinemachine.CinemachineVirtualCamera nueva)
    {
        OnCameraChange?.Invoke(nueva);
    }

}
