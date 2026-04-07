using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
public class TouchButtons : MonoBehaviour
{
    PlayerController playerController;
    bool active;
    CanvasGroup group;

    float dx;
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();

    }

    void OnEnable()
    {
        if (Accelerometer.current != null){
            InputSystem.EnableDevice(Accelerometer.current);
            Debug.Log("Gyroscope present");
        
        }
    }

    void OnDisable()
    {
        if (Accelerometer.current != null){
            InputSystem.DisableDevice(Accelerometer.current);
            Debug.Log("Gyroscope present");
        
        }
    }


    public void Activate()
    {
        active = true;
        group.interactable = true;
        group.blocksRaycasts = true;
        group.alpha = 1;
    }

    public void BackButton()
    {
        
    }

    public void ForwardButton()
    {
        
    }
}