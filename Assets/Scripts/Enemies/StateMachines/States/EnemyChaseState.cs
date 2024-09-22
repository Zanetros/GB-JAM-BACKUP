using System.Collections;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Transform _playerTransform;
    private PlayerController _playerController;

    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerController = GameObject.FindAnyObjectByType<PlayerController>();
        enemy.agent.updateRotation = false;
        enemy.agent.updateUpAxis = false;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("chase state on");
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("chase state off");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        ChasePlayer();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void ChasePlayer()
    {
        if (_playerController.isHiding)
        {
            enemy.StateMachine.ChangeState(enemy.StopChasingState);
        }

        else
        { 

            float moveX = _playerTransform.position.x - enemy.agent.transform.position.x;
            float moveY = _playerTransform.position.y - enemy.agent.transform.position.y;

            Vector2 targetPosition = enemy.agent.transform.position;

            enemy.animator.SetFloat("Horizontal", moveX);
            enemy.animator.SetFloat("Vertical", moveY);

            if (Mathf.Abs(moveY) > Mathf.Abs(moveX))
            {
                targetPosition.y = _playerTransform.position.y;
                enemy.agent.SetDestination(targetPosition);
            }
            else if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
            {
                targetPosition.x = _playerTransform.position.x;
                enemy.agent.SetDestination(targetPosition);
            }
        }       
    }   
}
