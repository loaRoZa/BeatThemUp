using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttackState : EstadoFSM
{
    // Variables del estado
    private float attackTime;
    private bool finished;
    private Enemy myInfo;

    private Coroutine corutina;

    // Constructor
    public BasicEnemyAttackState(FSM fsm, Animator animator)
        : base(fsm, animator)
    {
        myInfo = animator.GetComponentInParent<Enemy>();
    }

    public override void Enter()
    {
		
        base.Enter(); // llama al Enter() de la clase base (EstadoFSM)

        // Animaci�n de atacar
        animator.SetTrigger("attack");

        // Cuanto tiempo dura la animación de ataque
        attackTime = animator.GetCurrentAnimatorStateInfo(0).length;
        finished = false;

        corutina = fsm.mono.StartCoroutine(AccionCorutina());

        // efecto de sonido de dar golpe
        //AudioManager.GetAudioInstance().PlaySound(AudioManager.SFXList.enemyPunch);
    }

    public override void UpdateState()
    {
        // l�gica y f�sica de movimiento
        
        // verificar si el enemigo aún vive
        /*if(myInfo.isDead)
        {
            fsm.ChangeState((myInfo as BasicEnemy).estadoMuerte);
            return;
        }*/
        // verificar si el enemigo recibió golpe
        if(myInfo.wasHitted)
        {
            myInfo.ResetHitted();
            fsm.ChangeState((myInfo as BasicEnemy).estadoRecibirDaño);
            return;
        }

        // si ya terminó la animación de golpe
        if(finished)
        {
            // Cambia al estado de enter
            fsm.ChangeState((myInfo as BasicEnemy).estadoMovimiento);
            return;
        }
    }

    public override void Exit()
    {
    }

    private IEnumerator AccionCorutina()
    {
        yield return new WaitForSeconds(attackTime);
        finished = true;
    }
}
