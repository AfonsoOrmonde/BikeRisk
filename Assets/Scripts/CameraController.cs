using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;
    public float sensitivity = 300f;
    private Vector2 lookInput;


    void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();

    }

    void LateUpdate()
    {

    }
}