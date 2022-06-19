using PlayerOption.Scripts.Components.Audio;
using PlayerOption.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerOption.Scripts.UI.Widgets
{
    public class ButtomSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _audioClip;
        private AudioSource _source;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_source == null)
                _source = AudioUntils.FindSfxSource();

            _source.PlayOneShot(_audioClip);
        }
    }
}