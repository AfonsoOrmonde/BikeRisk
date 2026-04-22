using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] SettingsManager SettingsMenu;
    bool settingsOpen = false;
    [SerializeField] WinningMenu WinningMenu;
    [SerializeField] DeathMenu DyingMenu;


    void Start()
    {
    }
    void OnEnable()
    {
        InputManager.Controls.UI.OpenSetingsMenu.performed += ctx =>
        {
            OpenCloseSettings();
        };  
    }
    void OnDisable()
    {
        InputManager.Controls.UI.OpenSetingsMenu.performed -= ctx =>
        {
            OpenCloseSettings();
        };    
    }

    public void OpenCloseSettings()
    {
        if(!GameManager.Instance.isPaused){
            if(!settingsOpen)
                SettingsMenu.Open();
            else
                SettingsMenu.Close();}
    }
    public void OpenDying()
    {
        DyingMenu.Open();
    }
    public void OpenWinnigMenu()
    {
        WinningMenu.Open();
    }
}