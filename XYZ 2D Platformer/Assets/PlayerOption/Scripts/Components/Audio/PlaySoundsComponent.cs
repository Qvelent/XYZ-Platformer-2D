using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayerOption.Scripts.Audio
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        public const string SfxSourceTag = "SfxAudioSource";

        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _sounds;

        public void Play(string id)
        {
            foreach (var audioData in _sounds)
            {
                if (audioData.Id != id) continue;

                if(_source == null)
                    _source = GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
                    _source.pitch = Random.Range(.9f, 1.1f);
                
                _source.PlayOneShot(audioData.Clip);
                break;
            }
        }
        
        [Serializable]
        public class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }
}