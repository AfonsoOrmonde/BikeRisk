using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemChoose : MonoBehaviour
{
    [SerializeField] ItemData itemOfChoice;
    [SerializeField] TextMeshProUGUI textBoxName;
    [SerializeField] TextMeshProUGUI textBoxDescription;
    [SerializeField] Image imageItem;
    ItemSelectorMenu itemSelectorMenu;

    void Start()
    {
        itemSelectorMenu = FindAnyObjectByType<ItemSelectorMenu>();
    }

    public void setUpButton(ItemData item)
    {
        itemOfChoice = item;
        textBoxName.text = item.itemName;
        textBoxDescription.text = item.description;
        imageItem.sprite = item.itemImage;
    }

    public void PickItem()
    {
        itemSelectorMenu.ChooseItem(itemOfChoice);
    }
}
