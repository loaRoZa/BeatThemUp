using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Como es un estado, hereda de FSMSTATE
public class MinibossAttackState : EstadoFSM
{
    // Variables del estado
    private Enemy myInfo;
    private float attackTime;
    private Coroutine corutina;
    private bool finished;

    // Constructor
    public MinibossAttackState(FSM fsm, Animator animator) : 
        base(fsm, animator)
    {
        myInfo = animator.GetComponentInParent<Enemy>();
    }

    public override void Enter()
    {
        base.Enter();

        animator.SetTrigger("attack");

         // Determinar cuanto tiempo estará en el estado
        attackTime = animator.GetCurrentAnimatorStateInfo(0).length;
        finished = false;

        corutina = fsm.mono.StartCoroutine(ActionCoroutine());

        // efecto de sonido
        AudioManager.GetAudioInstance().PlaySound(AudioManager.SFXList.enemyPunch);

        // le decimos al miniboss que se lance a donde está ahorita el jugador
        //myInfo.MoveToPlayer();

        // lo cambiaremos a una capa donde el jugador no pueda pegarle al mini boss
        int capa = LayerMask.NameToLayer("NoDamage");
        myInfo.gameObject.layer = capa;
    }

    public override void UpdateState()
    {
        // Verificamos si el enemigo aún vive
        if(myInfo.isDead)
        {
            fsm.ChangeState((myInfo as MinibossControl).minibossDied);
            return;
        }
		
		 // Verificamos si golpearon al enemigo
        if(myInfo.wasHitted)
        {
            // quizás hay que resetear la variable de washitted ?
            myInfo.ResetHitted();
            // hago la animación de pintar el sprite de rojo
            animator.SetTrigger("damage");
            return;
        }
		
        if (finished)
        {
            // Cambia de estado a idle
            fsm.ChangeState((myInfo as MinibossControl).minibossIdle);
            // para evitar que siga ejecutando cosas este estado
            return;
        }
    }

    public override void Exit()
    {
        // que el enemigo no siga moviéndose
        myInfo.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // lo cambiaremos a una capa donde el jugador pueda pegarle de nuevo al mini boss
        int capa = LayerMask.NameToLayer("Default");
        myInfo.gameObject.layer = capa;
    }

    private IEnumerator ActionCoroutine()
    {
        yield return new WaitForSeconds(attackTime);

        finished = true;
    }
}
