using PlayerOption.Scripts.Utils;
using UnityEngine;

namespace PlayerOption.Scripts.Components.Audio
{
    public class PlaySfxSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private AudioSource _source;

        public void Play()
        {
            if (_source == null)
                _source = AudioUntils.FindSfxSource();
            
            _source.PlayOneShot(_clip);
        }
    }
}