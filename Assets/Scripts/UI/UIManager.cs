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
            if(!settingsOpen){
                SettingsMenu.Open();
                settingsOpen = true;
                GameManager.Instance.PauseGame();}
            else{
                SettingsMenu.Close();
                settingsOpen = false;
                GameManager.Instance.ContinueGame();}}
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