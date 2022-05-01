using PlayerOption.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerOption.Scripts.Components.LevelManegement
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
            
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}

