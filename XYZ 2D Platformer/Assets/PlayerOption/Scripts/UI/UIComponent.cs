using System.Collections;
using PlayerOption.Scripts.Utils;
using UnityEngine;

namespace PlayerOption.Scripts.UI
{
    public class UIComponent : MonoBehaviour
    {
        private FadeScreen _fadeScreen;

        private IEnumerator StartFateScreen()
        {
            _fadeScreen.FadeToBlack();
            
            yield return new WaitForSeconds((1f / _fadeScreen._fadeSpeed)+ .2f);
            
            _fadeScreen.FadeFromBlack();
        }

        private IEnumerator EndFateScreen()
        {
            _fadeScreen.FadeToBlack();

            yield return new WaitForSeconds((1f / _fadeScreen._fadeSpeed)+ 3f); 
        }
    }
}
