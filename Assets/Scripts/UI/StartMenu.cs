using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private MainMenu mainMenu;
    private int currentlySelectedLevel;
    private int currentlySelectedCharacter;

    [SerializeField]private GameObject startGameButton;
    [SerializeField] private CharactersStorage charactersStorage;

    [SerializeField] private TextMeshProUGUI textBoxDescriptionCharacter;

    private bool characterSelected;
    private bool levelSelected;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        mainMenu = FindAnyObjectByType<MainMenu>();
    }

    void Update()
    {
        if(levelSelected && characterSelected)
        {
            startGameButton.SetActive(true);
        }
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        characterSelected = false;
        levelSelected = false;
        startGameButton.SetActive(false); //to make sure the button doesnt appear without selection
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        startGameButton.SetActive(false); 
        textBoxDescriptionCharacter.text = "";
        mainMenu.OpenMenu();
    }

    public void SelectCharacter(int index)
    {
        currentlySelectedCharacter = index;
        characterSelected = true;
        textBoxDescriptionCharacter.text = charactersStorage.allCharacter[index].description;
    }

    public void SelectLevel(int index)
    {
        currentlySelectedLevel = index;
        levelSelected = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(currentlySelectedLevel);
    }

}
