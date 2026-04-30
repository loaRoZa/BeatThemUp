using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMoveState : EstadoFSM
{
    //variables del estado
    private float contadorTiempo;
    private bool finished;
    private Enemy myInfo;

    private Coroutine corutina;

   //constructor
   public BasicEnemyMoveState(FSM fsm, Animator anim): base(fsm, anim)
    {
        myInfo = anim.GetComponentInParent<Enemy>();
    }

   public override void Enter ()
    {
       

        //Llama al Enter () de la clase base
        base.Enter();

        //activar animacion de caminata
        animator.SetTrigger("move");

    }


    public override void UpdateState()
    {
        //preguntamos si el enemigo recibio golpe
        if(myInfo.wasHitted)
        {
            myInfo.ResetHitted();
            fsm.ChangeState((myInfo as BasicEnemy).estadoRecibirDaño);
            return;
        }

        //ir hacia el jugador
        myInfo.MoveToPlayer();

        if( myInfo.DistanceToPlayer() <
            myInfo.DistanceToAttack)
        {
            fsm.ChangeState(
                (myInfo as BasicEnemy).estadoAtacar);
            return;
        }
    }

    public override void Exit()
    {
       
    }



}
