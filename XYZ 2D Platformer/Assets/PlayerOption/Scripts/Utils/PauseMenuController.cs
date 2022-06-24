using System;
using System.Collections;
using PlayerOption.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PlayerOption.Scripts.Utils
{
    public class PauseMenuController : AnimatedWindow
    {
        [SerializeField] private string _sceneName,_mainMenu;
        [SerializeField] private GameObject _pauseScreen;

        private float _defaultTimeScale;

        public bool _isPaused;

        public static PauseMenuController instance;
        
        private void Awake()
        {
            instance = this;
        }

        protected override void Start()
        {
            base.Start();

            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }

        public void PauseUnPause()
        {
            if (_isPaused)
            {
                _isPaused = false;
                _pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                _isPaused = true;
                _pauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(_mainMenu);

            Time.timeScale = 1f;
        }
        
        public void OnShowSetting()
        {
            WindowUntils.CreateWindow("UI/SettingsWindow");
        }

        public void EndLevel()
        {
            StartCoroutine(EndLevelCo());
        }

        private IEnumerator EndLevelCo()
        {
            UIComponent.instance.FadeToBlack();

            yield return new WaitForSeconds((1/ UIComponent.instance._fadeSpeed)+ 0.1f); 
            
            SceneManager.LoadScene(_sceneName);
        }
    }
}
