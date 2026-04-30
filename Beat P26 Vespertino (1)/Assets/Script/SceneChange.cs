using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // <- para cambiar escena

public class SceneChange : MonoBehaviour
{
    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
	
	public void CerrarJuego()
	{
		Application.Quit();
	}
}
