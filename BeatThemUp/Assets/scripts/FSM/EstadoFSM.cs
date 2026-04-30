using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract indica que no puedo generar objetos de esta clase
public abstract class EstadoFSM
{
    // referencia a la maquina de estados que pertenece
    protected FSM fsm;
    protected Animator animator;

    //constructor
    public EstadoFSM(FSM fsm, Animator anim)
    {
        this.fsm = fsm;
        this.animator = anim;
    }

    //Virtual es para decirle a las clases que hereden de esta que
    //pueden sobrecargar el metodo
   
    public virtual void Enter ()
    {

    }

    //Abstract le dice a las clases que hereden que DEBEN 
    //sobrecagar el metodo
    public abstract void UpdateState();

    public abstract void Exit();

    
}
