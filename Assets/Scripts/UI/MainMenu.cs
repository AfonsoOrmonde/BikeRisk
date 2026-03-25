using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] private StartMenu StartMenu;
    [SerializeField] private SettingsManager SettingsManager;
    [SerializeField] private CodexMenu CodexMenu;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OpenSettings()
    {
        CloseMenu();
        SettingsManager.Open();
    }
   public void OpenCodex()
    {
        CloseMenu();
        CodexMenu.Open();
    }
   public void OpenStart()
    {
        CloseMenu();
        StartMenu.Open();
    }

    public void OpenMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void CloseMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;  
    }

    public void Quit()
    {
        Application.Quit();
    }
}
