using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
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
    public Renderer[] renderers;
    private Color[] originalColors;

    [SerializeField] Color hitColor;

    [SerializeField] protected float speed;

    [SerializeField] private float rotationSpeed;
    private Vector3 initialUp;
    // Start is called before the first frame update
    void Start()
    {
        initialUp = transform.up;
        setOriginalColors();
        if(enemyChaseState == null)
            Debug.LogError("Chase state is null it hasnt been initialized");
        player = FindAnyObjectByType<PlayerStats>();
        enemyStateMachine = new EnemyStateMachine(enemyChaseState);
    }


    private void setOriginalColors()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStateMachine == null) return;
        if(enemyStateMachine.getCurrentState() != null)
            enemyStateMachine.getCurrentState().During();

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(FlashDamage());
        Debug.Log($"New health Enemy = {health}");
        if(health<=0)
            Destroy(gameObject);
    }

    private IEnumerator FlashDamage()
    {
        for(int i = 0; i<renderers.Length; i++)
        {
           renderers[i].material.color = hitColor;
        }
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i<renderers.Length; i++)
        {
           renderers[i].material.color = originalColors[i];
        }
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

    public void RotateEnemy(GameObject target)
    {
        Vector3 lookVector = target.transform.position - transform.position;
        Vector3 projectedDirection = Vector3.ProjectOnPlane(lookVector, initialUp);
        if (projectedDirection == Vector3.zero) return;

        //if(rotationInitial.x == 0)
            //lookVector.y = 0f;
        //else
            //lookVector.z = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(projectedDirection, initialUp);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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


    public float getSpeed()
    {
        return speed;
    }

    public void setSpeed(float newSpeed)
    {
      speed = newSpeed;  
    }


    public PlayerStats getPlayer()
    {
        return player;
    }
}
