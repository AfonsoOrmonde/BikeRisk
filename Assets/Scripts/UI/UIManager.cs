using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] SettingsManager SettingsMenu;
    [SerializeField] WinningMenu WinningMenu;
    [SerializeField] DeathMenu DyingMenu;

    public void OpenSettings()
    {
        SettingsMenu.Open();
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