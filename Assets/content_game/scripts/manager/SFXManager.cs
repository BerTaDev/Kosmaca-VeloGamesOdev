using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class SFXManager : MonoBehaviour
    {
        public AudioSource source;
        public AudioClip coin_clip;
        public AudioClip fail_clip;
        [Space]
        public AudioClip button_clip;

        public static SFXManager singleton;
        private void Awake()
        {
            singleton = this;
        }

        public void PlayCoin() { source.PlayOneShot(coin_clip, 0.5f); }
        public void PlayFail() { source.PlayOneShot(fail_clip, 1.0f); }
        public void PlayButton() { source.PlayOneShot(button_clip, 1.0f); }
    }
}
