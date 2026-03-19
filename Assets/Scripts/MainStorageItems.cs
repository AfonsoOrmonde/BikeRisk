using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DataBase", menuName = "ItemStorage")]
public class MainStorageItems:ScriptableObject
{
    public List<ItemData> allItems;
}