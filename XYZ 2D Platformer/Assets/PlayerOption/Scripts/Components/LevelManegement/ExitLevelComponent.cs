using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerOption.Scripts.Components.LevelManegement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        public void Exit()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}

