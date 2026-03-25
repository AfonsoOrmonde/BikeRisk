using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CodexMenu : MonoBehaviour
{
    CanvasGroup canvasGroup;
    MainMenu mainMenuUIManager;

    List<ItemCodexButton> buttons;
    [SerializeField]TextMeshProUGUI descriptionText;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        mainMenuUIManager = FindAnyObjectByType<MainMenu>();
        buttons = GetComponentsInChildren<ItemCodexButton>().ToList();
        foreach(ItemCodexButton button in buttons){
            ItemCodexButton localButton = button; 
            localButton.ItemCodexButtonClicked.AddListener(() => setDescription(localButton.getItemDescription()));
        }
    }

    private void setDescription(string text)
    {
        descriptionText.text = text;
    }

        public void Open()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        public void Close()
        {            
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            
            if (mainMenuUIManager)
            {
                mainMenuUIManager.OpenMenu();
            }
        }
}
