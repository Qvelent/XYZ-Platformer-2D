using PlayerOption.Scripts.Audio;
using UnityEngine;

namespace Assets.PlayerOption.Scripts.Audio
{
    public class PlaySfxSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private AudioSource _source;

        public void Play()
        {
            if (_source == null)
                _source = GameObject.FindWithTag
                    (PlaySoundsComponent.SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}