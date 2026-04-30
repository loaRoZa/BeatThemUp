using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
	// estados del personaje
	public EstadoFSM estadoEntrada;
	public EstadoFSM estadoMovimiento;
	public EstadoFSM estadoAtacar;
	public EstadoFSM estadoRecibirDaño;
	public EstadoFSM estadoMuerte;
	
	// override le indica que voy a sobrecargar
	// un método de la clase base
    protected override void Start()
    {
		// base llama a la clase base
        base.Start();
		
		// Crear o instanciar los estados
		estadoEntrada = new BasicEnemyEnterState(enemyFSM, enemyAnimator);
		estadoMovimiento = 
			new BasicEnemyMoveState(enemyFSM, enemyAnimator);
		estadoAtacar = new BasicEnemyAttackState
			(enemyFSM, enemyAnimator);
		estadoRecibirDaño = 
			new BasicEnemyReceiveHitState(enemyFSM, enemyAnimator);
		estadoMuerte = 
			new BasicEnemyDieState(enemyFSM, enemyAnimator);
		
		// Configuramos el estado inicial
		enemyFSM.Init(estadoEntrada);
    }

    protected override void Update()
    {
        base.Update();
    }
}
