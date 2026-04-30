using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Como es un estado, hereda de FSMSTATE
public class MinibossIdleState : EstadoFSM
{
    // Variables del estado
    private float idleTime;
    private Coroutine corutina;
    private bool finished;
    private Enemy myInfo;

    // Constructor
    public MinibossIdleState(FSM fsm, Animator animator) : 
        base(fsm, animator)
    {
        myInfo = animator.GetComponentInParent<Enemy>();
    }

    public override void Enter()
    {
        base.Enter();

        // Determinar cuanto tiempo estará en el estado
        idleTime = Random.Range(3f, 5f);
        finished = false;

        corutina = fsm.mono.StartCoroutine(ActionCoroutine());

        // habilitamos recibir golpes
        myInfo.GetComponent<Rigidbody>().isKinematic = false;
        myInfo.EnableCollider();
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
            // Cambia de estado
            fsm.ChangeState((myInfo as MinibossControl).minibossDash);
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
