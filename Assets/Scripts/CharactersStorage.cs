using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DataBase", menuName = "CharacterStorage")]
public class CharactersStorage:ScriptableObject
{
    public List<Character> allCharacter;
}