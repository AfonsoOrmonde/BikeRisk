using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyAttackState enemyAttackState;
    protected EnemyChaseState enemyChaseState;
    // Start is called before the first frame update
    void Start()
    {
        if(enemyChaseState == null)
            Debug.LogError("Chase state is null it hasnt been initialized");
        enemyStateMachine = new EnemyStateMachine(enemyChaseState);
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
}
