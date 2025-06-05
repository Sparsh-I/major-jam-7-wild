using UnityEngine;

namespace Managers
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] musicClips;
        [SerializeField] private int currentTrack;
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        
        private void Start()
        {
            currentTrack = 0;
            audioSource.clip = musicClips[currentTrack];
            audioSource.Play();
        }

        public void NextSong()
        {
            currentTrack++;
            if (currentTrack == musicClips.Length) currentTrack = 0;
            
            audioSource.clip = musicClips[currentTrack];
            audioSource.Play();
        }
    }
}
