using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract indica que no puedo generar objetos
// de esta clase, que debo generar objetos
// que hereden de esta
public abstract class EstadoFSM
{
	// referencia a la máquina de estados a la que pertenece
	protected FSM fsm;
	protected Animator animator;
	
	// constructor
	public EstadoFSM(FSM fsm, Animator anim)
	{
		this.fsm = fsm;
		this.animator = anim;
	}
	
	// virtual es para decirle a las clases
	// que hereden de esta, que PUEDEN
	// sobrecargar el método
	public virtual void Enter()
	{
	}
	
	// abstract le dice a las clases que hereden
	// que DEBEN sobrecargar el método
	public abstract void UpdateState();
	
	public abstract void Exit();
	
}
