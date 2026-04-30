using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MinibossControl : Enemy
{
    public static event Action OnMinibossDead;
    // lista de estados del mini jefe
    public EstadoFSM minibossEnterState;
    public EstadoFSM minibossIdle;
    public EstadoFSM minibossAttack;
    public EstadoFSM minibossDied;
    public EstadoFSM minibossDash;


    private bool isActiveBoss = false;

    protected override void Start()
    {
        // para saber cuando inicia el miniboss a ejecutarse, esperamos el evento del jugador
        MiniBossSensor.OnPlayerGetToMiniboss += MinibossStartAction;

        base.Start();

        // iniciamos los estados
        minibossEnterState  = new MinibossEnterState(enemyFSM, enemyAnimator);
        minibossIdle        = new MinibossIdleState(enemyFSM, enemyAnimator);
        minibossAttack      = new MinibossAttackState(enemyFSM,enemyAnimator);
        minibossDash = new MinibossDashState(enemyFSM, enemyAnimator);
        minibossDied = new MinibossDeadState(enemyFSM, enemyAnimator);
    }

    public static void MinibossDied()
    {
        OnMinibossDead?.Invoke();
    }

    private void MinibossStartAction()
    {
        // habilitar la animación de entrada del miniboss
        enemyAnimator.SetTrigger("enableBoss");
        // asignamos estado inicial para arrancar la fsm
        enemyFSM.Init(minibossEnterState);

        isActiveBoss = true;
    }

    protected override void Update()
    {
        // actualice la fsm solo si ya está activo el boss
        if(isActiveBoss)
            base.Update();
    }
}
