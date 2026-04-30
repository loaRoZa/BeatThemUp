using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyEnterState : EstadoFSM
{
	// TO DO...
	// variables del estado
	private float contadorTiempo;
	private bool finished;
	private Enemy myInfo;
	
	private Coroutine corutina;
	
	// constructor
	public BasicEnemyEnterState(FSM fsm, Animator anim)
		: base(fsm, anim)
	{
		myInfo = anim.GetComponentInParent<Enemy>();
	}	
	
	public override void Enter()
	{
		Debug.Log("Enemigo base: Estado entrada");
		
		// llama al Enter() de la clase base
		base.Enter();
		
		// Determinar cuanto tiempo estara en este estado
		contadorTiempo = Random.Range(0.5f, 3f);
		
		finished = false;
		
		// Empezar a contar
		corutina = fsm.mono.StartCoroutine(Corutina());
	}
	
	public override void UpdateState()
	{
		// Si el enemigo muere que cambie al estado de muerte
        if(myInfo.isDead)
        {
            // cambiar al estado de muerte
            // detenemos la corutina para que no genere problemas
            fsm.mono.StopCoroutine(corutina);
            fsm.ChangeState((myInfo as BasicEnemy).estadoMuerte);
            return;
        }
		
		// Preguntamos si ya acabó de contar
		if(finished) // if(finished == true)
		{
			// Cambiar de estado
			fsm.ChangeState(
				(myInfo as BasicEnemy).estadoMovimiento);
			// Evitamos que ejecute otra cosa
			return;
		}
	}
	
	public override void Exit()
	{
		Debug.Log("Enemigo base: Sale de estado entrada");
	}
	
	private IEnumerator Corutina()
	{
		// Esperamos el tiempo asignado
		yield return new WaitForSeconds(contadorTiempo);
		
		finished = true;
	}
}
