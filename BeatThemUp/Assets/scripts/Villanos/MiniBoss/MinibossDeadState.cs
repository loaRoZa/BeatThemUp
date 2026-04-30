using UnityEngine;

public class MinibossDeadState : EstadoFSM
{
    public MinibossDeadState(FSM fsm, Animator animator) :
         base(fsm, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetTrigger("dead");
    }

    public override void UpdateState()
    {
    }

    public override void Exit()
    {
    }
}
