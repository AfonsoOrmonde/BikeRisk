using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState: MonoBehaviour
{
    public static GameState Instance;
    public List<int> levels;
    public List<List<int>> skinsUnlocked = new List<List<int>>();
    public CharactersStorage storage;

    public Image godButton;

    public bool godMode = false;
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

    private void Start()
    {
        PopulateList();
    }

    public bool TurnGodMode()
    {
        godMode = !godMode;
        return godMode;
    }

    private void PopulateList()
    {
        for (int i = 0; i < storage.allCharacter.Count; i++)
            skinsUnlocked.Add(new List<int> { 1, 0 });
    }

    public void unlockSkin(int character)
    {
        skinsUnlocked[character][1] = 1;
    }

    public void unlockLevel(int level)
    {
        if(level < levels.Count)
            levels[level] = 1;
    }

    public bool checkSkin(int character, int indexSkin)
    {
        return skinsUnlocked[character][indexSkin] == 1;
    }

    public bool checkLevel(int level)
    {
      return levels[level] == 1;   
    }

}