using System;
using System.Collections;
using PlayerOption.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PlayerOption.Scripts.Utils
{
    public class FadeScreen : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public static FadeScreen instance;
        
        private void Awake()
        {
            instance = this;
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
