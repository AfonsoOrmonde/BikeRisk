using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private MainMenu mainMenu;
    private int currentlySelectedLevel;
    private int currentlySelectedSkin;
    private int currentlySelectedCharacter;

    [SerializeField]private GameObject startGameButton;
    [SerializeField] private CharactersStorage charactersStorage;
    [SerializeField] private List<GameObject> skinSelector;
    [SerializeField] private List<GameObject> levelSelector;

    [SerializeField] private TextMeshProUGUI textBoxDescriptionCharacter;

    private bool characterSelected;
    private bool levelSelected;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        mainMenu = FindAnyObjectByType<MainMenu>();
        levelSelector.ForEach(x =>
            {
            if (GameState.Instance.checkLevel(levelSelector.IndexOf(x)))
                {
                    x.SetActive(true);
                }        
            });
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
        if(characterSelected)
            skinSelector.ForEach(x => x.SetActive(false));
        mainMenu.OpenMenu();
    }

    public void SelectCharacter(int index)
    {
        if(characterSelected)
            skinSelector.ForEach(x => x.gameObject.SetActive(false));
        currentlySelectedCharacter = index;
        currentlySelectedSkin = 0;
        characterSelected = true;
        textBoxDescriptionCharacter.text = charactersStorage.allCharacter[index].description;
        skinSelector.ForEach(x => {
            if (GameState.Instance.checkSkin(currentlySelectedCharacter, skinSelector.IndexOf(x)))
            {
                x.SetActive(true);
            }
            }
        );
    }

    public void SelectSkin(int index)
    {
        currentlySelectedSkin = index;
    }

    public void SelectLevel(int index)
    {
        currentlySelectedLevel = index;
        levelSelected = true;
    }

    public void StartGame()
    {
        CharacterSelector.Instance.ChooseCharacter(currentlySelectedCharacter,currentlySelectedSkin);
        SceneManager.LoadScene(currentlySelectedLevel);
    }

}
