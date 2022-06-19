using UnityEngine;

namespace PlayerOption.Scripts.Utils
{
   
    
    public class AudioUntils
    {
        public const string SfxSourceTag = "SfxAudioSource";
        
        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}