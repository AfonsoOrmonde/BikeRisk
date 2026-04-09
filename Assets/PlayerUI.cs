using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update

    public TouchButtons androiControls;

    [SerializeField] Slider healthSlider;
    [SerializeField] Slider dashSlider;

    private PlayerStats playerStats;
    void Awake()
    {
        #if UNITY_ANDROID
            androiControls.Activate();
        #endif

        playerStats = FindAnyObjectByType<PlayerStats>();
        SetMaxHealth();

    }

    void Update()
    {
        SetMaxDash();
    }

    public void SetMaxDash()
    {
        dashSlider.maxValue = playerStats.getMaxDash();
        SetDash();
    }

    public void SetDash()
    {
        dashSlider.value = playerStats.getDash();
    }

    public void SetMaxHealth()
    {
        healthSlider.maxValue = playerStats.getMaxHealth();
        healthSlider.value = playerStats.getHealth();
    }

    public void SetHealth()
    {
        healthSlider.value = playerStats.getHealth();
    }

    void OnEnable()
    {
        playerStats.HealthRegained.AddListener(SetHealth);
        playerStats.HealthIncreased.AddListener(SetMaxHealth);
        Debug.Log("Got to enable listernes of health");
    }
    void OnDisable()
    {
        playerStats.HealthRegained.RemoveAllListeners();
        playerStats.HealthIncreased.RemoveAllListeners();
    }
}
