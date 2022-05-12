using PlayerOption.Scripts.Utils;
using UnityEngine;

namespace PlayerOption.Scripts.Components.LevelManegement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        public void Exit()
        {
           FadeScreen.instance.EndLevel();
        }
    }
}

