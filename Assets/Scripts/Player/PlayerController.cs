using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 moveInput;
    private PlayerStats player;
    LayerMask playerMask;

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
        InputManager.Controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        InputManager.Controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        InputManager.Controls.Player.Shoot.performed += ctx => Shoot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentVeloctiy = _rb.velocity;
        currentVeloctiy.x = transform.forward.x * player.getSpeedBike();
        currentVeloctiy.z = transform.forward.z * player.getSpeedBike();
        
        if(moveInput.y > 0){ 
            currentVeloctiy.x *= player.getAcceleration();
            currentVeloctiy.z *= player.getAcceleration();

        }
        if(moveInput.y < 0) {
            currentVeloctiy.x *= player.getSlowDown();
            currentVeloctiy.z *= player.getSlowDown();
        }

        _rb.velocity = currentVeloctiy;
        
        
        transform.Rotate(Vector3.up * player.getSpeedTurning() * moveInput.x * Time.deltaTime);    
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
