using UnityEngine;

public class EnemyProjectile : Projectile
{

    protected override void Update()
    {
        if(!hasTarget) return;

        float step = speed *Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
        
        if(Vector3.Distance(transform.position,target)<= 0.5){
            Debug.Log("Destrying bullet");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if((tohit.value & 1<<other.gameObject.layer) != 0){
            Debug.Log("Entering Here");
            if(other.TryGetComponent(out IDamageable damageable)){
                Debug.Log("Entering damage dealer");
                damageable.TakeDamage(damage);}
            Destroy(gameObject);
        }
    }

}