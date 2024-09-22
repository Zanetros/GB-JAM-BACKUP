using System;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 _targetPosition;
    private Vector3 _direction;
    
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter Idle State");
    }

    public override void ExitState()
    {
        base .ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        /*
        if (enemy.isAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }*/
        
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }  
}

