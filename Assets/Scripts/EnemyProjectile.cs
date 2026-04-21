using UnityEngine;

public class EnemyProjectile : Projectile
{
    public void SetHardPosition(Vector3 targetPoint)
    {
        hasTarget = true;
        Vector3 extraDistanceVector  = (targetPoint - this.transform.position).normalized *extraDistance;
        target = targetPoint +  extraDistanceVector;
    }
    protected override void Update()
    {
        if(!hasTarget) return;

        float step = speed *Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
        
        if(Vector3.Distance(transform.position,target)<= 0.5){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if((tohit.value & 1<<other.gameObject.layer) != 0){
            if(other.TryGetComponent(out IDamageable damageable)){
                damageable.TakeDamage(damage);}
            Destroy(gameObject);
        }
    }

}