using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSelectorMenu : MonoBehaviour
{
    List<ItemChoose> buttonsOfItems = new List<ItemChoose>();
    [SerializeField] MainStorageItems storageItems;
    [SerializeField] GameManager gameManager;
    PlayerStats playerStats;
    CanvasGroup canvas;
    void Start()
    {
        buttonsOfItems = GetComponentsInChildren<ItemChoose>().ToList();
        canvas = GetComponent<CanvasGroup>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenItemSelection(PlayerStats playerStats)
    {
        ShowMenu();
        this.playerStats = playerStats;
        buttonsOfItems.ForEach(x => x.setUpButton(RandomItem()));
    }

    private ItemData RandomItem()
    {
        int fullCount = storageItems.allItems.Count;
        int randomNumber = Random.Range(0, fullCount);
        return storageItems.allItems[randomNumber];
    }

    public void ChooseItem(ItemData item)
    {
        CloseMenu();
        playerStats.EquipItem(item);
    }

    public void ShowMenu()
    {
        gameManager.PauseGame();
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts =true;
    }

    public void CloseMenu()
    {
        gameManager.ContinueGame();
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts =false;
    }
}
