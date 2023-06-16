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
    [SerializeField] AudioClip dragonFlight;
    [SerializeField] AudioClip dragonStart;
    [SerializeField] AudioClip catPurr;
    [SerializeField] AudioClip foxCall;
    [SerializeField] AudioClip falconCall;
    [SerializeField] AudioClip dragonRoar;
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
        musicPlayer.volume = 0.35f;   
        musicPlayer.clip = musicTracks[5];
        musicPlayer.loop = false;
        musicPlayer.Play(); 
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

    public void PlayDragonFlight()
    {
        PlayClip(dragonFlight, cardVolume);
    }

    public void PlayDragonBegin()
    {
        PlayClip(dragonStart, cardVolume);
    }

    public void PlayCatSound()
    {
        PlayClip(catPurr, cardVolume);
    }

    public void PlayFoxSound()
    {
        PlayClip(foxCall,cardVolume);
    }

    public void PlayFalconSound()
    {
        PlayClip(falconCall, .6f);
    }

    public void PlayDragonSound()
    {
        PlayClip(dragonRoar, cardVolume);
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
