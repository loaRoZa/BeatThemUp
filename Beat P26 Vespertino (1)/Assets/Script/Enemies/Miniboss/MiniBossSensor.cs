using System;
using Cinemachine;
using UnityEngine;

public class MiniBossSensor : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camaraBoss;

    // evento para avisar que el jugador llegó a la zona del minijefe
    public static  event Action OnPlayerGetToMiniboss;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Enfocar la cámara de la pelea del jefe
            CameraEvents.ActivarCambioCamara(camaraBoss);

            // lanzamos el evento de que el jugador llegó a la zona del miniboss
            OnPlayerGetToMiniboss?.Invoke();

            // desactivamos el sensor una vez que ha sido utilizado
            gameObject.SetActive(false);
        }
    }
}
