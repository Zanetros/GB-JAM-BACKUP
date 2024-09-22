using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }
    public bool isFancingRight { get; set; } = true;
    [field: SerializeField] public NavMeshAgent agent { get; set; }

    [field: SerializeField] public Animator animator { get; set; }
    public Rigidbody2D rb { get; set; } 

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyStopChasingState StopChasingState { get; set; }
    [field: SerializeField] public bool isAggroed { get; set; } = true;

    #endregion

    #region Idle Variables

    public float randomMovementRange = 5f;
    public float randomMovementSpeed = 1f;

    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        StopChasingState = new EnemyStopChasingState(this, StateMachine);

    }

    private void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        if (isAggroed )
        {
            StateMachine.Initialize(ChaseState);
        }
    }

    private void Update()
    {
        StateMachine.currentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void MoveEnemy(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.currentState.AnimationTriggerEvent(triggerType);
    }

    public void SetAggroStatus(bool aggroed)
    {
        isAggroed = aggroed;
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
    }

    public void VanishEnemy()
    {
        StartCoroutine(SetEnemyFalse(this.gameObject));
    }

    public IEnumerator SetEnemyFalse(GameObject enemy)
    {
        yield return new WaitForSeconds(4);
        Destroy(enemy);
    }
}
