using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    //Estado actual
    EstadoFSM currentState;

   public MonoBehaviour mono;

    public FSM(MonoBehaviour mono)
    {
        this.mono = mono;
    }

    public void Init (EstadoFSM inicial)
    {
        currentState = inicial;
        //en cuanto tenemos el estado inicial,
        //podemos ejecutar su Enter ()
        currentState.Enter();
    }

    public void Update()
    {
        currentState.UpdateState();
    }

    public void ChangeState (EstadoFSM next)
    {
        //no quiero que pueda transicionar al mismo
        //estado en el que está
        if(currentState != next)
        {
            //el estado actual ejecuta su salida
            currentState.Exit();
            //actualizo elestado actual
            currentState = next;
            //el nuevo estado, ejecuta su enter
            currentState.Enter();
        }
    }
}
