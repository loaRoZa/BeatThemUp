using UnityEngine;
using System; // <- para Action

public static class CameraEvents
{
	// Eventos para notificar cuando la cámara se bloquea y desbloquea
	public static event Action OnCameraBlock;
	public static event Action OnCameraUnblock;
	
	// Evento para cambio de cámara
	public static event Action<Cinemachine.CinemachineVirtualCamera> OnCameraChange;
	
	// Métodos que permiten llamar a los eventos
	public static void BlockCamera()
	{
		OnCameraBlock?.Invoke();
	}
	
	public static void UnblockCamera()
	{
		OnCameraUnblock?.Invoke();
	}
	
	// Método para invocar evento de cambiar cámara
	public static void ActivarCambioCamara
		(Cinemachine.CinemachineVirtualCamera nueva)
	{
		OnCameraChange?.Invoke(nueva);
	}
}
