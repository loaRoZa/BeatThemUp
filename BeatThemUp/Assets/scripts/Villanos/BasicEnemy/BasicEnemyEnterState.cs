using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyEnterState : EstadoFSM
{
    //variables del estado
    private float contadorTiempo;
    private bool finished;
    private Enemy myInfo;

    private Coroutine corutina;

   //constructor
   public BasicEnemyEnterState(FSM fsm, Animator anim): base(fsm, anim)
    {
        myInfo = anim.GetComponentInParent<Enemy>();
    }

   public override void Enter ()
    {
        Debug.Log("Enemigo base: Estado entrada");

        //Llama al Enter () de la clase base
        base.Enter();

        //Determinar cuanto tiempo estara en este estado
        contadorTiempo = Random.Range(0.5f, 3f);

        finished = false;

        //Empezar a contar
        fsm.mono.StartCoroutine(Corutina());
    }


    public override void UpdateState()
    {
        //preguntamos si ya acabo de contar
        if (finished)
        {
            //cambiar de estado
            fsm.ChangeState((myInfo as BasicEnemy).estadoMovimiento);
            //evitamos que ejecute otra cosa
            return;
        }
    }

    public override void Exit()
    {
        Debug.Log("Enemigo base = Sale de estado Entrada");
    }

    private IEnumerator Corutina()
    {
        //Eperamos el tiempo asignado
        yield return new WaitForSeconds(contadorTiempo);

        finished = true;
    }



}
