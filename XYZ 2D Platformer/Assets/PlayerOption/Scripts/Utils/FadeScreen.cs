using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace PlayerOption.Scripts.Utils
{
    public class FadeScreen : MonoBehaviour
    {
        public Image fadeScreen;
        
        public float _fadeSpeed;
        private bool _shouldFadeToBlack, _shouldFadeFromBlack;

        private void Awake()
        {
            fadeScreen.gameObject.SetActive(true);
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

            if (_shouldFadeFromBlack)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                    Mathf.MoveTowards(fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 0f)
                {
                    _shouldFadeFromBlack = false;
                }
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
