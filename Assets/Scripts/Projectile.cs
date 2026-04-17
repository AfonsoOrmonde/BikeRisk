using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 target;
    private bool hasTarget = false;
    [SerializeField] float speed;
    [SerializeField] float extraDistance;
    [SerializeField] float damage;
    public LayerMask tohit;
    void Start()
    {
        
    }

    public void SetTarget(GameObject newTarget)
    {
        hasTarget = true;
        target = newTarget.transform.position;
        Vector3 extraDistanceVector = (newTarget.transform.position - this.transform.position).normalized*extraDistance;
        target = target + extraDistanceVector;
    }
    public void SetHardTarget(Vector3 targetPoint)
    {
        hasTarget = true;
        Vector3 extraDistanceVector  = (targetPoint - this.transform.position).normalized *extraDistance;
        target = targetPoint +  extraDistanceVector;
    }

    void Update()
    {
        if(hasTarget)
            transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
        
        if(Vector3.Distance(transform.position,target)<= 0.5){
            Debug.Log("Destrying bullet");
            Destroy(gameObject);
            }

    }

    void OnTriggerEnter(Collider other)
    {
        if((tohit.value & 1<<other.gameObject.layer) == 1<<other.gameObject.layer){
            if(other.TryGetComponent<IDamageable>(out IDamageable damageable)){
                damageable.TakeDamage(damage);}
            Destroy(gameObject);
        }
    }

}