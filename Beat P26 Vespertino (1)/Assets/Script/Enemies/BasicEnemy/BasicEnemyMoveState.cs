using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMoveState : EstadoFSM
{
	// TO DO...
	// variables del estado
	private float contadorTiempo;
	private bool finished;
	private Enemy myInfo;
	
	private Coroutine corutina;
	
	// constructor
	public BasicEnemyMoveState(FSM fsm, Animator anim)
		: base(fsm, anim)
	{
		myInfo = anim.GetComponentInParent<Enemy>();
	}	
	
	public override void Enter()
	{
		// llama al Enter() de la clase base
		base.Enter();
		
		// activar animación de caminata
		animator.SetTrigger("move");
	}
	
	public override void UpdateState()
	{
		if(myInfo.isDead)
        {
            fsm.ChangeState((myInfo as BasicEnemy).estadoMuerte);
            return;
        }
		// preguntamos si el enemigo recibió golpe
		if(myInfo.wasHitted)
		{
			myInfo.ResetHitted();
			fsm.ChangeState(
					(myInfo as BasicEnemy).estadoRecibirDaño);
			return;
		}
		
		// Ir hacia el jugador
		myInfo.MoveToPlayer();
		
		// Preguntamos si estamos a rango de ataque
		if(myInfo.DistanceToPlayer() < 
			myInfo.DistanceToAttack)
		{
			// cambiar al estado de golpear
			fsm.ChangeState(
				(myInfo as BasicEnemy).estadoAtacar);
			return;
		}
	}
	
	public override void Exit()
	{
	}
}
