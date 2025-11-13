using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SingletonSFX : MonoBehaviour
{
    // Singleton
    #region Singleton
    private static SingletonSFX _instance;

    public static SingletonSFX Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SingletonSFX");
                _instance = go.AddComponent<SingletonSFX>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
    
    [Header("Audio Sources")]
    [Tooltip("The audio source used to play SFXs.")] public AudioSource _sfxSource;
    [Tooltip("The audio source used to play ambiant sounds.")] public AudioSource _ambiantSource;
    [Tooltip("The audio source used to play music.")] public AudioSource _musicSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _theOneAndOnlyMusic;
    [SerializeField] private AudioClip _theOneAmbiantSound;
    [SerializeField] private bool _hasToPlayTheOneAndOnlyMusicBecauseItsTooMuchAUnMoment;



    [Header("Sound Effect")]
    public AudioClip PageTurnSound;
    public AudioClip OnclickSound;
    public AudioClip OnclickSound2;
    public AudioClip SpawnSound;
    public AudioClip HitSound;
    public AudioClip NoiseSound;


    private void Start()
    {
        if (!_hasToPlayTheOneAndOnlyMusicBecauseItsTooMuchAUnMoment) return;
        PlayMusic(_theOneAndOnlyMusic, false);
    }

    public void PlaySound(AudioClip audioClip, bool isPitchRandom)
    {
        AudioSource audioSource = _sfxSource;

        audioSource.pitch = (!isPitchRandom) ? 1 : 1 + Random.Range(-0.2f, 0.2f);
        audioSource.clip = audioClip;
        audioSource.PlayOneShot(audioClip);
    }
    
    public async void PlayMusic(AudioClip music, bool loop)
    {
        _musicSource.clip = music;
        _musicSource.loop = loop;
        _musicSource.Play();
        // int TimeMusicHasFinished = Mathf.FloorToInt(music.length * 1000);
        // await Task.Delay(TimeMusicHasFinished);
        // var myRandomIndex = Random.Range(5, 15);
        // await Task.Delay(myRandomIndex * 1000);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void StopSound()
    {
        _sfxSource.Stop();
    }
}
