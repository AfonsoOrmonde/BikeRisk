using System.Collections;
using System.Drawing;
using UnityEngine;

public class Boss: MonoBehaviour, IDamageable
{
    [SerializeField] protected float health;
    [SerializeField] protected float speedMultiplier;
    private BossStateMachine bossStateMachine;
    private BossAttackState bossAttackState;
    private BossCooldownState bossCooldownState;
    private BoosWaitState bossWaitState;
    private BossPrepareState bossPrepareState;
    [SerializeField] GameObject projectilePrefab;
    public Transform pointOfAttackRight;
    public Transform pointOfAttackLeft;
    [SerializeField] float cooldown;
    private bool canAttack;
    public GameObject bodyBoss;

    private PlayerStats player;

    private Rigidbody _rb;

    void Start()
    {
        bossAttackState = new BossAttackState(this);
        bossCooldownState = new BossCooldownState(this);
        bossWaitState = new BoosWaitState(this);
        bossPrepareState = new BossPrepareState(this);
        bossStateMachine = new BossStateMachine(bossWaitState);
        _rb = GetComponent<Rigidbody>();
        player = FindAnyObjectByType<PlayerStats>();
    }

    public void Attack(Transform point)
    {
        EnemyProjectile bossProjectile = Instantiate(projectilePrefab, point.position, point.rotation).GetComponent<EnemyProjectile>();
        bossProjectile.SetHardPosition(new Vector3(player.transform.position.x,point.position.y, point.transform.position.z));
    }

    public void Activate()
    {
        bodyBoss.SetActive(true);
        bossStateMachine.ChangeState(bossPrepareState);
    }

    public void StartCooldown()
    {
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    public void ChangeToAttack()
    {
        bossStateMachine.ChangeState(bossAttackState);
    }
    public void ChangeToCooldown()
    {
        bossStateMachine.ChangeState(bossCooldownState);
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    void Update()
    {
        bossStateMachine.getCurrentState().During();
    }

    void Move()
    {
        
    }

    public bool getCanAttack()
    {
        return canAttack;
    }


    public void setVelocityBoss(Vector3 vector)
    {
        _rb.velocity = vector;
    }

    public float getBossSpeed()
    {
        return player.getSpeedBike()*speedMultiplier;
    }

    public void TakeDamage(float value)
    {
        health -= value;
        if(health <= 0)
            gameObject.SetActive(false);
    }
}