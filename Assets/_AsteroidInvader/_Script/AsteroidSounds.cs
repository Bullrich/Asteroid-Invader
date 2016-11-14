using UnityEngine;
using System.Collections;
// by @JavierBullrich
[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AsteroidSounds : MonoBehaviour {
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static AsteroidSounds instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    public AudioClip[] music;
    int musicClips;

    bool audioOn;


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);

        audioOn = GetSoundOptions();

        musicClips = Random.Range(0, music.Length);
        PlanetManager.instance.GameRestarted.AddListener(CheckAudio);
    }

    void Start()
    {
        PlayMusic();
    }

    void CheckAudio()
    {
        audioOn = GetSoundOptions();
    }

    void MusicSetting()
    {
        PlayerPrefs.SetInt("music",
            PlayerPrefs.GetInt("music", 1) == 1 ? 0 : 1);

        musicSource.volume = (float)PlayerPrefs.GetInt("music", 1) / 2;
    }


    ///<summary>Used to play single sound clips.</summary>
    public void PlaySingle(AudioClip clip)
    {
        if (GetSoundOptions())
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;

            //Play the clip.
            efxSource.Play();
        }
    }

    void PlayMusic()
    {
        bool canPlayMusic = GetSoundOptions();
        musicSource.clip = music[musicClips];
        musicClips++;
        if (musicClips > music.Length - 1)
            musicClips = 0;
        musicSource.Play();
        float musicLength = musicSource.clip.length;
        Invoke("PlayMusic", musicLength);
    }


    ///<summary>RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.</summary>
    public void RandomizeSfx(AudioClip clipToPlay)
    {
        if (audioOn)
        {

            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            //Set the pitch of the audio source to the randomly chosen pitch.
            efxSource.pitch = randomPitch;

            //Set the clip to the clip at our randomly chosen index.
            efxSource.clip = clipToPlay;

            //Play the clip.
            efxSource.Play();
        }
        else
            CheckAudio();
    }



    // Sound off or on -----------------------------

    public void SetSound(bool state)
    {
        PlayerPrefs.SetInt("Sound", state ? 1 : 0);
        audioOn = state;
        print("Switched audio state to " + state);
    }

    public bool GetSoundOptions()
    {
        int value = PlayerPrefs.GetInt("Sound", 1);
        float musicLevel = (float)value;
        musicSource.volume = musicLevel / 2;

        if (value == 1)
            return true;
        else
            return false;
    }

}
