using System.Linq;
using UnityEngine;

namespace PlayerOption.Scripts.Model
{
    public class GameSession : MonoBehaviour

    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        private void Awake()
        {
            if (ItSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }

        private bool ItSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            return sessions.Any(gameSession => gameSession != this); // foreach (var gameSession in sessions)
                                                                     // {
                                                                     //     if(gameSession != this)
                                                                     //         return true;
                                                                     // }
                                                                     // return false; 
        }
    }
}