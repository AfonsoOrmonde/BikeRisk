using System.Collections;
using UnityEngine;
using UnityEngine.UI;


    public class SettingsManager : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        MainMenu mainMenuUIManager;

        [SerializeField] Slider musicSlider;

        [SerializeField] Slider sfxSlider;

        public void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            mainMenuUIManager = FindAnyObjectByType<MainMenu>();
            musicSlider.value = AudioManager.Instance.getMusicVolume();
            sfxSlider.value = AudioManager.Instance.getSFXVolume();
        }


        public void sfxVolumeChange(float volume)
        {
            Debug.Log(volume);
            AudioManager.Instance.changeSFXVolume(volume);
        }

        public void musicVolumeChange(float volume)
        {
            AudioManager.Instance.changeMusicVolume(volume);

        }

        public void Open()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        public void Close()
        {            
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            
            if (mainMenuUIManager)
            {
                mainMenuUIManager.OpenMenu();
            }
        }

    }