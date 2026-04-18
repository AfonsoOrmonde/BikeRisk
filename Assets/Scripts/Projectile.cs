using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 target;
    protected bool hasTarget = false;
    [SerializeField] protected float speed;
    [SerializeField] float extraDistance;
    [SerializeField]protected float damage;
    public LayerMask tohit;

    public GameObject hit;
 
    public void SetTarget(GameObject newTarget)
    {
        hasTarget = true;
        target = newTarget.transform.position;
        Vector3 extraDistanceVector = (newTarget.transform.position - this.transform.position).normalized*extraDistance;
        target = target + extraDistanceVector;
        hit = newTarget;
    }
    public void SetHardTarget(Vector3 targetPoint, GameObject newTarget)
    {
        hasTarget = true;
        Vector3 extraDistanceVector  = (targetPoint - this.transform.position).normalized *extraDistance;
        target = targetPoint +  extraDistanceVector;
        if(newTarget != null)
            hit = newTarget;
    }


    protected virtual void Update()
    {
        if(!hasTarget) return;

        float step = speed *Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
        
        if(Vector3.Distance(transform.position,target)<= 0.5){
            Debug.Log("Destrying bullet");
            if (hit != null && hit.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}