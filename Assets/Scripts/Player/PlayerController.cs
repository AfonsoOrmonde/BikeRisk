using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 moveInput;

    private float gyroSpeed = 0; //this variable is only used for when in android
    private PlayerStats player;
    [SerializeField] LayerMask toNotHitMask;
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField]private Transform GravityAnchor;
    [SerializeField] private float Gravity;
    [SerializeField] private float rotationTimer;
    [SerializeField] private float rateOfVelocityInterpolation;
    [SerializeField] private GameObject projectile;
    private bool canShoot = true;
    Vector3 possibleNewDirection;

    UIManager uIManager;


    void Awake()
    { 
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerStats>();
        if(player is null)
        {
            Debug.LogError("No PlayerStats in object");
        }
        InputManager.Controls.Player.Move.performed += ctx =>{ 
            moveInput = ctx.ReadValue<Vector2>();
            if(moveInput.y>0)
                player.setChargeDash(true);
        };
        InputManager.Controls.Player.Move.canceled += ctx =>{ 
            moveInput = Vector2.zero;
            player.setChargeDash(false);
        };
        InputManager.Controls.Player.Tilt.performed += ctx => {
                Vector3 accel = ctx.ReadValue<Vector3>();
                gyroSpeed = accel.x;
            };
        InputManager.Controls.Player.Tilt.canceled  += ctx => gyroSpeed = 0f;
        InputManager.Controls.Player.Shoot.performed += ctx => Shoot();
        uIManager = FindAnyObjectByType<UIManager>();
        StartCoroutine(ApplyBySecondEffects());
        //InputManager.Controls.Player.ChangeGravity.performed += ctx => ChangeGravity();
    }

    void OnDisable()
    {
        InputManager.Controls.Player.Move.performed -= ctx =>{ 
            moveInput = ctx.ReadValue<Vector2>();
            player.setChargeDash(true);
        };
        InputManager.Controls.Player.Move.canceled -= ctx =>{ 
            moveInput = Vector2.zero;
            player.setChargeDash(false);
        };
        InputManager.Controls.Player.Tilt.performed -= ctx => {
                Vector3 accel = ctx.ReadValue<Vector3>();
                gyroSpeed = accel.x;
            };
        InputManager.Controls.Player.Tilt.canceled  -= ctx => gyroSpeed = 0f;
        InputManager.Controls.Player.Shoot.performed -= ctx => Shoot(); 
    }

    IEnumerator ApplyBySecondEffects()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            player.HealByRate();
            if(player.getCanSlowDonw()){
                player.CheckTimeStop();}
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        _rb.AddForce((GravityAnchor.position - transform.position).normalized * Gravity, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (moveInput.y>0)
            {
                enemy.TakeDamage(player.getDamage());
            }
            player.TakeDamage(player.getCrashingDamage());

            AudioManager.Instance.playSFX("Landing");
        }
    }
    void Movement()
    {
        float speed =  player.getSpeedBike();
        float combinedX = moveInput.x + gyroSpeed;
        if (moveInput.y > 0 && player.getDash() > 0)
            speed *= player.getAcceleration(); 
        else if (moveInput.y < 0)
            speed *= player.getSlowDown();
            
        Vector3 gravityDirection = (GravityAnchor.position - transform.position).normalized;
        Vector3 currentGravityVelocity = Vector3.Project(_rb.velocity, gravityDirection);

        _rb.velocity = transform.forward * speed + (transform.right * (combinedX * player.getSpeedTurning())) + currentGravityVelocity;
    }

    public void Shoot()
    {
        if(!canShoot){return;}

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        AudioManager.Instance.playSFX("Shooting");

        Vector3 targetPoint ;
        if (Physics.Raycast(ray, out hit, 1000f, ~toNotHitMask))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(1000f);

        Projectile bullet = Instantiate(projectile, this.transform.position, this.transform.rotation).GetComponent<Projectile>();
        
        if(hit.collider is null)
            bullet.SetHardTarget(targetPoint, null);
        else
            bullet.SetHardTarget(targetPoint, hit.collider.gameObject);

        StartCoroutine(ShootCooldown());
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(player.getShootCooldown());
        canShoot = true;
    }

    public void PlayerDied()
    {
        uIManager.OpenDying();
        Destroy(this);
    }
    void ChangeGravityToDirection(Vector3 newUp, RideableWall.WallType type)
    {
        Vector3 currentEuler = transform.rotation.eulerAngles;
        switch(type)
        {
            case RideableWall.WallType.Bot:
                transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, 0);
                break;
            case RideableWall.WallType.Top:
                transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, 180);
                break;
            case RideableWall.WallType.Right:
                transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, 90);
                break;
            case RideableWall.WallType.Left:
                transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, -90);
                break;
        }
    }

    public void DetectedNewFloor(GameObject newFloor,RideableWall.WallType type)
    {
        Transform newFloorPosition = newFloor.transform;
        possibleNewDirection = newFloorPosition.forward;
        ChangeGravityToDirection(possibleNewDirection,type);
    }


    public void setMoveY(float value)
    {
        moveInput.y = value;
    }

    void OnGUI()
    {
    float radius = player.getRadiusShooting() * 20f; // tweak multiplier to match visually
    Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

    DrawCircle(center, radius);
    }

    void DrawCircle(Vector2 center, float radius)
{
    int segments = 64;
    float angleStep = 360f / segments;

    Vector3 prevPoint = center + new Vector2(radius, 0);
    GUI.color = Color.red;
    for (int i = 1; i <= segments; i++)
    {
        float angle = i * angleStep * Mathf.Deg2Rad;
        Vector3 newPoint = new Vector2(center.x + Mathf.Cos(angle) * radius, center.y + Mathf.Sin(angle) * radius);
        GUI.DrawTexture(new Rect(newPoint.x, newPoint.y, 2, 2), Texture2D.whiteTexture);
        prevPoint = newPoint;
    }
}
}
