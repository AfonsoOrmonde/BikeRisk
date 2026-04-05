using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    CanvasGroup group;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        group.blocksRaycasts = true;
        group.alpha = 1;
        group.interactable = true;
    }

    public void Close()
    {
        group.blocksRaycasts = false;
        group.alpha = 1;
        group.interactable = false;  
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
