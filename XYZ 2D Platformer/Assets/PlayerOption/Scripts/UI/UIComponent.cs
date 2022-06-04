using UnityEngine;
using UnityEngine.UI;

namespace PlayerOption.Scripts.UI
{
    public class UIComponent : MonoBehaviour
    {
        public Image fadeScreen;
        public float _fadeSpeed;
        public bool _shouldFadeToBlack, _shouldFadeFromBlack;
        
        public static UIComponent instance;
        
        private void Awake()
        {
            instance = this;
            
           //fadeScreen.gameObject.SetActive(true);
        }

        private void Start()
        {
            FadeFromBlack();
        }

        private void Update()
        {
            if (_shouldFadeToBlack)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                    Mathf.MoveTowards(fadeScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));
                
                if (fadeScreen.color.a == 1f)
                {
                    _shouldFadeToBlack = false;
                }
            }

            if (!_shouldFadeFromBlack) return;
            
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));
                
            if (fadeScreen.color.a == 0f)
            {
                _shouldFadeFromBlack = false;
            }
        }

        public void FadeToBlack()
        {
            _shouldFadeToBlack = true;
            _shouldFadeFromBlack = false;
        }

        public void FadeFromBlack()
        {
            _shouldFadeFromBlack = true;
            _shouldFadeToBlack = false;
        }
    }
}
