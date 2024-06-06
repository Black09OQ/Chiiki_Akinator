using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkScene
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioClip detectionStartSound;

        [SerializeField] AudioClip detectionCompleteSound;

        AudioSource audioSource;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayStartSound()
        {
            audioSource.PlayOneShot(detectionStartSound);
        }

        public void PlayCompleteSound()
        {
            audioSource.PlayOneShot(detectionCompleteSound);
        }
    }
}


