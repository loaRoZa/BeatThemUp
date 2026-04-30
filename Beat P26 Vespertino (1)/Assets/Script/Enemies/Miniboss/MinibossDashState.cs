using UnityEngine;

public class MinibossDashState : EstadoFSM
{
    private MinibossControl myInfo;
    private Vector3 targetPosition;

    public MinibossDashState(FSM fsm, Animator animator) :
        base(fsm, animator)
    {
        myInfo = animator.GetComponentInParent<MinibossControl>();
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetTrigger("dash");

        // Capturamos posición fija al entrar, no seguimiento en tiempo real
        targetPosition = myInfo.Player.position;
    }

    public override void UpdateState()
    {
        // Verificamos muerte y golpes igual que en IdleState
        if (myInfo.isDead)
        {
            fsm.ChangeState(myInfo.minibossDied);
            return;
        }

        if (myInfo.wasHitted)
        {
            myInfo.ResetHitted();
            animator.SetTrigger("damage");
        }

        // Movemos hacia la posición objetivo fija
        myInfo.MoveTowards(targetPosition);

        // Cuando llega, transiciona a ataque
        if (myInfo.DistanceToPlayer() <= myInfo.DistanceToAttack)
        {
            fsm.ChangeState(myInfo.minibossAttack);
            return;
        }
    }

    public override void Exit()
    {
        myInfo.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}