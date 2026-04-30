using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControlUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        //suscribimos la función al evento
        MinibossControl.OnMinibossDead += ShowPanelControlUI;
    }

    void ShowPanelControlUI()
    {
        gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        MinibossControl.OnMinibossDead -= ShowPanelControlUI;
    
    }
   
    
}
