using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundHandler : MonoBehaviour
{
    private static SoundHandler instance;

    public static SoundHandler Instance
    {
        get { return instance;}
    }

    [SerializeField] List<AudioClip> musicTracks;
    [SerializeField] AudioClip singleCardA;
    [SerializeField] AudioClip singleCardB;
    [SerializeField] AudioClip shuffleA;
    [SerializeField] AudioClip shuffleB;
    [SerializeField] [Range(0f, 1f)] float cardVolume = 1f;
    AudioSource musicPlayer;
    
    private List<AudioClip> shuffledTracks;
    private int index;
    System.Random rand = new System.Random();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        musicPlayer = GetComponent<AudioSource>();    
    }

    private void Update() {
        if (!musicPlayer.isPlaying) PlayMusic();
    }

    public void PlayMusic()
    {
        if (!musicPlayer.isPlaying)
        {
            shuffledTracks = musicTracks;
            Shuffle(shuffledTracks);

            musicPlayer.clip = shuffledTracks[index];
            musicPlayer.loop = false;
            musicPlayer.Play();

            index = ++index % musicTracks.Count;

            if (index == 0) Shuffle(shuffledTracks);
        }
        
    }

    public void PlaySingleCardA()
    {
        PlayClip(singleCardA, cardVolume);
    }

    public void PlaySingleCardB()
    {
        PlayClip(singleCardB, cardVolume);
    }

    public void PlayShuffleA()
    {
        PlayClip(shuffleA, cardVolume);
    }

    public void PlayShuffleB()
    {
        PlayClip(shuffleB, cardVolume);
    }


    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos,volume);
        }
    }

    void Shuffle<T>(IList<T> values)
    {
        for (int i = values.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            T value = values[k];
            values[k] = values[i];
            values[i] = value;
        }
    }
}
