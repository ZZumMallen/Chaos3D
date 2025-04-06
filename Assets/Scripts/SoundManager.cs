using UnityEngine;
using UnityEngine.Rendering;

namespace Chaos
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Audio Source")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource SFXSource;

        [Header("Background Music")]

        [Range(0f,1f)]
        [SerializeField] private float _backgroundMusicVolume = 0.2f;
        public AudioClip background;

        [Header("Player Sound Effects")]
        public AudioClip playerJump;
        public AudioClip playerShootSpam;
        public AudioClip playerShootCharge;
        public AudioClip playerDeath;
        public AudioClip playerDamage;

        [Header("Enemy Sound Effects")]
        public AudioClip enemyDamage1;
        public AudioClip enemyDamage2;
        public AudioClip enemyDamage3;
        public AudioClip enemyDamage4;
        public AudioClip enemyDeath;

        private void Start()
        {
            musicSource.clip = background;
            musicSource.volume = _backgroundMusicVolume;;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

}