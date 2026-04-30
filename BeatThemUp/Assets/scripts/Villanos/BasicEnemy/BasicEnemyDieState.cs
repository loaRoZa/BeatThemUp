using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDieState : EstadoFSM
{
    // Variables del estado
    private float animationTime;
    private bool finished;
    private Enemy myInfo;

    private Coroutine corutina;

    // Constructor
    public BasicEnemyDieState(FSM fsm, Animator animator)
        : base(fsm, animator)
    {
        myInfo = animator.GetComponentInParent<Enemy>();
    }

    public override void Enter()
    {
        base.Enter(); // llama al Enter() de la clase base (EstadoFSM)

        // Animación
        animator.SetTrigger("killed");

        // Cuanto tiempo dura la animación
        animationTime = animator.GetCurrentAnimatorStateInfo(0).length;
        finished = false;

        corutina = fsm.mono.StartCoroutine(AccionCorutina());

        // efecto de sonido de que se murió
        //AudioManager.GetAudioInstance().PlaySound(AudioManager.SFXList.enemyPunch);

        // efecto de camera shake
        //ScreenShake.Instance.Shake(1f);

    }

    public override void UpdateState()
    {
        // si ya terminó la animación
        if(finished)
        {
            // El objeto enemigo se destruye
            myInfo.Die();            
        }
    }

    public override void Exit()
    {
    }

    private IEnumerator AccionCorutina()
    {
        yield return new WaitForSeconds(animationTime);
        finished = true;
    }
}
