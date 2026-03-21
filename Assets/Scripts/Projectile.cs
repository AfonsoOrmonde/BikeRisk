using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] float speed;
    [SerializeField] float extraDistance;
    [SerializeField] float damage;
    void Start()
    {
        
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget.transform.position;
        Vector3 extraDistanceVector = newTarget.transform.position.normalized*extraDistance;
        target = target + extraDistanceVector;
    }
    void Update()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
        
        if(Vector3.Distance(transform.position,target)<= 0.5)
            Destroy(gameObject);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerStats player))
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}