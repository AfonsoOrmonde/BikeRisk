using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    private PlayerStats player;
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyAttackState enemyAttackState;
    protected EnemyChaseState enemyChaseState;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] protected float radiusOfAttack;
    [SerializeField] protected float cooldownOfAttack;
    private bool canAttack = true;

    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        if(enemyChaseState == null)
            Debug.LogError("Chase state is null it hasnt been initialized");
        enemyStateMachine = new EnemyStateMachine(enemyChaseState);
        player = FindAnyObjectByType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyStateMachine.getCurrentState().During();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"New health Enemy = {health}");
    }

    public void Move(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
    }

    public bool InRadius(GameObject target)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if(distanceToTarget <= radiusOfAttack)
        {
            return true;
        }
        return false;
    }

    public void Attack(GameObject player)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        if(newProjectile.TryGetComponent(out Projectile projectileNew))
        {
            projectileNew.SetTarget(player);
            StartCoroutine(Cooldown());
        }
        else
        {
            Debug.LogError("Projectile has no Projectile prefab");
        }
    }

    private IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldownOfAttack);
        canAttack = true;
    }

    public void ChangeToAttack()
    {
        enemyStateMachine.ChangeState(enemyAttackState);
    }
    public void ChangeToChase()
    {
        enemyStateMachine.ChangeState(enemyChaseState);
    }

    public EnemyStateMachine getStateMachine()
    {
        return enemyStateMachine;
    }

    public bool CanAttack()
    {
        return canAttack;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusOfAttack);
    }


    public PlayerStats getPlayer()
    {
        return player;
    }
}
