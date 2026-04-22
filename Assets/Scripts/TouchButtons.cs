using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
public class TouchButtons : MonoBehaviour
{
    PlayerController playerController;
    PlayerStats playerStats;
    bool active;
    [SerializeField] CanvasGroup group;

    float dx;
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        playerStats = FindAnyObjectByType<PlayerStats>();
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

    public void MovementButton(float value)
    {
        if(value > 0)
            playerStats.setChargeDash(true);
        else
            playerStats.setChargeDash(false);
        playerController.setMoveY(value);
    }

    public void ShootingButton()
    {
        playerController.Shoot();
    }
}