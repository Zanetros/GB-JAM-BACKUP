using UnityEngine;

public class EnemyStopChasingState : EnemyState
{
    private Transform _leavePlayer;
    private PlayerController _playerController;

    public EnemyStopChasingState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _leavePlayer = GameObject.FindGameObjectWithTag("LeavePlayer").transform;
        _playerController = GameObject.FindAnyObjectByType<PlayerController>();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("STOP CHASE ON");

    }

    public override void ExitState()
    {
        base.ExitState();

    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        StopChasingPlayer();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public void StopChasingPlayer()
    {
        float moveX = _leavePlayer.position.x - enemy.agent.transform.position.x;
        float moveY = _leavePlayer.position.y - enemy.agent.transform.position.y;

        Vector2 targetPosition = this.enemy.agent.transform.position;

        enemy.animator.SetFloat("Horizontal", moveX);
        enemy.animator.SetFloat("Vertical", moveY);

        if (Mathf.Abs(moveY) > Mathf.Abs(moveX))
        {
            targetPosition.y = _leavePlayer.position.y;
            enemy.agent.SetDestination(targetPosition);
        }
        else if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
        {
            targetPosition.x = _leavePlayer.position.x;
            enemy.agent.SetDestination(targetPosition);
        }
        _playerController.isChasing = false;
        Debug.Log("Going back to point");
        enemy.VanishEnemy();

    }
}
