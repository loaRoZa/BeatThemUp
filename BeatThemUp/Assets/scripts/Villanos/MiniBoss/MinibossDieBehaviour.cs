using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossDieBehaviour : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // anuncia el evento de que murió el miniboss
        MinibossControl.MinibossDied();

        Destroy(animator.transform.parent.gameObject);
    }
}
