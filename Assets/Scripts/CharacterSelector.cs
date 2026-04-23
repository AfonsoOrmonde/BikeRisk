using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector Instance;

    public List<GameObject> charactersList;
    public int indexOfSkin;
    public int indexOfCharacter;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    void Start()
    {
        indexOfCharacter = 0;
        indexOfSkin = 0;
    }

    public void ChooseCharacter(int indexCharacter, int indexSkin)
    {
        indexOfSkin = indexSkin;
        indexOfCharacter = indexCharacter;
    }

    public int getSkin()
    {
        return indexOfSkin;
    }

    public int getCharacter()
    {
        return indexOfCharacter;
    }

}