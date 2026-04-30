using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //<- leer teclado
using UnityEngine.SceneManagement; // <- cambio de escenas

public class TitleScreen : MonoBehaviour
{
	private InputAction accionCualquierTecla;
	
    void Awake()
    {
        // creamos la acción de apretar tecla
		accionCualquierTecla = 
			new InputAction(binding: "<Keyboard>/anyKey");
			
		// Asignamos el evento
		accionCualquierTecla.performed += ctx => Cambio();
    }

    void Cambio()
    {
        SceneManager.LoadScene("Menu");
    }
	
	private void OnEnable()
	{
		accionCualquierTecla.Enable();
	}
	
	private void OnDisable()
	{
		accionCualquierTecla.Disable();
	}
}
