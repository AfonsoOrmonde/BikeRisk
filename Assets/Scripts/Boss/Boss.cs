using UnityEngine;

public class Boss: MonoBehaviour
{
    [SerializeField] protected float health;
    private BossStateMachine bossStateMachine;
    private BossAttackState bossAttackState;
    private BossCooldownState bossCooldownState;
    [SerializeField] GameObject projectilePrefab;
    private bool canAttack;


    void Start()
    {
        bossAttackState = new BossAttackState(this);
        bossCooldownState = new BossCooldownState(this);
        bossStateMachine = new BossStateMachine(bossCooldownState);
    }

    void Update()
    {
        bossStateMachine.getCurrentState().During();
    }

    void Move()
    {
        
    }
}