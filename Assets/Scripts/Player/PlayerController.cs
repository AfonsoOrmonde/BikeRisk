using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 moveInput;
    private bool CanChangeGravity;
    private PlayerStats player;
    LayerMask playerMask;
    bool isGrounded;
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField]private Transform GravityAnchor;
    [SerializeField] private float Gravity;
    [SerializeField] private float rotationTimer;

    Coroutine rotationRoutine;

    Vector3 possibleNewDirection;

    Vector3 currenGravityDirection = new Vector3(0,0,0);
    // Start is called before the first frame update

    void Awake()
    { 
        playerMask= ~LayerMask.GetMask("Player");
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerStats>();
        if(player is null)
        {
            Debug.LogError("No PlayerStats in object");
        }
        currenGravityDirection = (GravityAnchor.position - transform.position).normalized;
        InputManager.Controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        InputManager.Controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        InputManager.Controls.Player.Shoot.performed += ctx => Shoot();
        InputManager.Controls.Player.ChangeGravity.performed += ctx => ChangeGravity();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGrounded();
        Movement();
        _rb.AddForce((GravityAnchor.position - transform.position).normalized * Gravity, ForceMode.Acceleration);
    }

 
    IEnumerator RotationTransition(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationTimer * Time.fixedDeltaTime
            );
            yield return new WaitForFixedUpdate();
        }

        transform.rotation = targetRotation;
        rotationRoutine = null;
    }

    void Movement()
    {
        //Vector3 currentVeloctiy = _rb.velocity;
        float speed =  player.getSpeedBike();

        if (moveInput.y > 0)
            speed *= player.getAcceleration();
        else if (moveInput.y < 0)
            speed *= player.getSlowDown();
            
        Vector3 gravityDirection = (GravityAnchor.position - transform.position).normalized;
        Vector3 currentGravityVelocity = Vector3.Project(_rb.velocity, gravityDirection);

        _rb.velocity = transform.forward * speed + currentGravityVelocity;

        if (isGrounded){
            transform.Rotate(Vector3.up * player.getSpeedTurning() * moveInput.x * Time.fixedDeltaTime);
        }
    }
    void Shoot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if(Physics.SphereCast(ray, player.getRadiusShooting(), out hit, 1000f, playerMask)){
            if(hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(player.getDamage());
            }
        }
        else 
        {
            Debug.Log("Hit nothing");
        }

    }

    void ChangeGravityToDirection(Vector3 newGravityDirection)
    {

        if (rotationRoutine != null) return;

        Debug.Log("In direction function of gravity");

        newGravityDirection.Normalize();
        currenGravityDirection = newGravityDirection;
        Vector3 newUp = newGravityDirection;

        Vector3 newForward = Vector3.ProjectOnPlane(transform.forward, newUp);

        if (newForward == Vector3.zero)
            newForward = Vector3.ProjectOnPlane(transform.right, newUp);

        newForward.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(newForward, newUp);

        _rb.velocity = Vector3.ProjectOnPlane(_rb.velocity, newGravityDirection);

        /*if (rotationRoutine != null)
            StopCoroutine(rotationRoutine);*/

        rotationRoutine = StartCoroutine(RotationTransition(targetRotation));
    }
    void ChangeGravity()
    {
        if(CanChangeGravity){
        Debug.Log($"Changing Gravity with vector = {possibleNewDirection}");
        ChangeGravityToDirection(possibleNewDirection);
        }
        CanChangeGravity = false;
    }

    public void DetectedNewFloor(Transform newFloorPosition)
    {
        Debug.Log($"Detect new Floor with vector = {newFloorPosition.forward}");
        CanChangeGravity = true;
        possibleNewDirection = newFloorPosition.forward;
    }
    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            currenGravityDirection,
            groundCheckDistance,
            playerMask
        );
    }

    void OnGUI()
    {
    float radius = player.getRadiusShooting() * 20f; // tweak multiplier to match visually
    Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

    // OR use GL for runtime:
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
