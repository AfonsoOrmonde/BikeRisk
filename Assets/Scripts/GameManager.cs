using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPaused = false;

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

    }
    public void StartGame()
    {
        AudioManager.Instance.playMusic("MainMusic");
    }

    public void EndGame()
    {
        AudioManager.Instance.stopMusic();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public IEnumerator SlowDownTime(float scale, float AmountOfTime)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSeconds(AmountOfTime);
        Time.timeScale = 1f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}