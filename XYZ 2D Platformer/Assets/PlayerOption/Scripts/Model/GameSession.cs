using System.Linq;
using PlayerOption.Scripts.Model.Data;
using PlayerOption.Scripts.Utils.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerOption.Scripts.Model
{
    public class GameSession : MonoBehaviour

    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        private PlayerData _save;
        
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public QuickInventoryModel QuickInventory { get; private set; }
        
        
        
        private void Awake()
        {
            LoadHud();

            if (ItSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
               // Save();
                InitModels();
                DontDestroyOnLoad(this);
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(_data);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
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