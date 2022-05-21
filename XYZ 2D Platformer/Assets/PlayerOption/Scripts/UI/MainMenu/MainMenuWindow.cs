using System;
using PlayerOption.Scripts.UI.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerOption.Scripts.UI.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;
        
        public void OnShowSetting()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnStartGame()
        {
            _closeAction = () => { SceneManager.LoadScene("Level 0"); };
            Close();
        }

        public void OnExit()
        {
            _closeAction = () => 
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete();
        }
    }
}