using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DataBase", menuName = "Character")]
public class Character:ScriptableObject
{
    public PlayerStats allItems;
    public string description;
}