using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroComboControlBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //cada clip de anmacion de golpe tiene duracion diferente
        animator.GetComponent<AtaqueHeroe>().SetComboResetTime(stateInfo.length);

        //inicie el cronometro de ataque
        animator.GetComponent<AtaqueHeroe>().ResetAttackTimer();

        //sabemos que inicia la animacion del golpe, podemos ejecutar el sonido
        AudioManager.GetAudioInstance().PlaySound(AudioManager.SFXList.playerPunch);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
