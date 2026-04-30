using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyReceiveHitState : EstadoFSM
{
    // Variables del estado
    private float animationTime;
    private bool finished;
    private Enemy myInfo;

    private Coroutine corutina;

    // Constructor
    public BasicEnemyReceiveHitState(FSM fsm, Animator animator)
        : base(fsm, animator)
    {
        myInfo = animator.GetComponentInParent<Enemy>();
    }

    public override void Enter()
    {
        base.Enter(); // llama al Enter() de la clase base (EstadoFSM)

        // Animación de ser golpeado
        animator.SetTrigger("receiveHit");

        // Cuanto tiempo dura la animación
        animationTime = animator.GetCurrentAnimatorStateInfo(0).length;
        finished = false;

        corutina = fsm.mono.StartCoroutine(AccionCorutina());

        // efecto de sonido de recibir el golpe
        //AudioManager.GetAudioInstance().PlaySound(AudioManager.SFXList.enemyPunch);
    }

    public override void UpdateState()
    {
        // l�gica y f�sica de movimiento
        
        // Si el enemigo muere que cambie al estado de muerte
        /*if(myInfo.isDead)
        {
            // cambiar al estado de muerte
            // detenemos la corutina para que no genere problemas
            fsm.mono.StopCoroutine(corutina);
            fsm.ChangeState((myInfo as BasicEnemy).estadoMuerte);
            return;
        }
		*/

        // si ya terminó la animación
        if(finished)
        {
            // Cambia al estado de enter
            fsm.ChangeState((myInfo as BasicEnemy).estadoEntrada);
            return;
        }
    }

    public override void Exit()
    {
        // permitimos que le vuelvan a pegar al enemigo
        //myInfo.EnableCollider();
    }

    private IEnumerator AccionCorutina()
    {
        yield return new WaitForSeconds(animationTime);
        finished = true;
    }
}
