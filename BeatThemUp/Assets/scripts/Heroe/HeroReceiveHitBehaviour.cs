using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroReceiveHitBehaviour : StateMachineBehaviour
{
    public MovimientoHeroe movH;
    public AtaqueHeroe ataqueH;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movH = animator.GetComponentInParent<MovimientoHeroe>();
        ataqueH = animator.GetComponent<AtaqueHeroe>();

        //deshabilitamos movimiento y ataque
        movH.enabled = false;
        ataqueH.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //saliendo de recibir daño, el jugador se puede mover
        movH.enabled = true;
        ataqueH.enabled = true;
        movH.CanMoveAgain();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
