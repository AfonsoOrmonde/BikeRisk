using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningMenu: MonoBehaviour
{
    CanvasGroup group;


    public void Start()
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
        Close();
        SceneManager.LoadScene(0);
    }
}