using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] float speedBike;
    private Bker controls;
    private Vector2 moveInput;
    [SerializeField] float speedTurning;
    [SerializeField] float acceleration;
    [SerializeField] float slowDown;
    // Start is called before the first frame update

    void Awake()
    {
        controls = new Bker();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        InputManager.Controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        InputManager.Controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentVeloctiy = _rb.velocity;
        currentVeloctiy.x = transform.forward.x * speedBike;
        currentVeloctiy.z = transform.forward.z * speedBike;
        
        if(moveInput.y > 0){ 
            currentVeloctiy.x *= acceleration;
            currentVeloctiy.z *= acceleration;

        }
        if(moveInput.y < 0) {
            currentVeloctiy.x *= slowDown;
            currentVeloctiy.z *= slowDown;
        }

        _rb.velocity = currentVeloctiy;
        
        
        transform.Rotate(Vector3.up * speedTurning * moveInput.x * Time.deltaTime);    
        }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
