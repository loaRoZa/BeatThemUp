using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Como es un estado, hereda de FSMSTATE
public class MinibossEnterState : EstadoFSM
{
    // Variables del estado
    private float idleTime;
    private Coroutine corutina;
    private bool finished;
    private Enemy myInfo;

    // Constructor
    public MinibossEnterState(FSM fsm, Animator animator) : 
        base(fsm, animator)
    {
        myInfo = animator.GetComponentInParent<Enemy>();
    }

    public override void Enter()
    {
        base.Enter();

        // habilitar el sprite renderer
        animator.GetComponent<SpriteRenderer>().enabled = true;

        // Determinar cuanto tiempo estará en el estado
        idleTime = animator.GetCurrentAnimatorStateInfo(0).length;
        finished = false;

        corutina = fsm.mono.StartCoroutine(ActionCoroutine());

        // durante este estado no se le puede pegar al miniboss
        myInfo.GetComponent<Rigidbody>().isKinematic = true;
        myInfo.DisableCollider();
    }

    public override void UpdateState()
    {
        
        if (finished)
        {
            // Cambia de estado
            fsm.ChangeState((myInfo as MinibossControl).minibossIdle);
            // para evitar que siga ejecutando cosas este estado
            return;
        }
    }

    public override void Exit()
    {
        
    }

    private IEnumerator ActionCoroutine()
    {
        yield return new WaitForSeconds(idleTime);

        finished = true;
    }
}
