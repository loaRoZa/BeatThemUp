using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // SINGLETON
    private static AudioManager Instance;
    public static AudioManager GetAudioInstance() { return Instance; }

    private AudioSource ambientSource;

    // Use this for initialization
    void Awake()
    {
        //Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        //Get States from PlayerPrefs here to manage settings

        //Initializing static instance and Audio Sources
        InitializeAudioSources();

    }

    private void InitializeAudioSources()
    {
        ambientSource = transform.Find("Music").GetComponent<AudioSource>();
    }

    public void StopMusic()
    {
        if (ambientSource.isPlaying) ambientSource.Stop();
    }

    public void PlayMusic(MusicList sl, bool loop)
    {
        //si queremos que la musica se repita
        ambientSource.loop = loop;
        //buscar el archivo de audio a partir de su id
        ambientSource.clip = GetAudioClip(sl);
        //damos play al audio
        ambientSource.Play();
    }

    public void PlaySound(SFXList sl)
    {
        //Para un efecto de sonido hay que crear el audio source al momento
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.loop = false;
        source.volume = 0.5f;

        //obtenemos el clip de sfx a reproducir
        AudioClip sonido = GetAudioClip(sl);

        //asignamos el sonido al source
        source.clip = sonido;

        source.Play();

        //necesitamos borrar el audio source
        //cuando el sonido haya terminado
        Destroy(source, sonido.length);
    }

    private AudioClip GetAudioClip(SFXList sfxID)
    {
        foreach(SFXClips efecto in sfx)
        {
            //preguntamos si este es el efecto que busco por su ID
            if(sfxID == efecto.sfxID)
            {
                //regresamos al clip de audio
                return efecto.sfxClip;
            }
        }
        //si no lo encuentra regresamos a nulo
        return null;
    }

    private AudioClip GetAudioClip(MusicList musicID)
    {
        foreach(MusicClips cancion in music)
        {
            if(musicID == cancion.musicID)
            {
                return cancion.musicClip;
            }
        }
        //si sale del foreach, es que no encontró la cancion
        return null;
    }
   

    public void StartMusicFade(float duration, float finalVolume)
    {
        
    }

    /*private IEnumerator MusicFadeCoroutine(float duration, float finalVolume)
    {
        
    }*/

    public enum MusicList
    {
        menu, //es un 0
        levelZero, //es un 1
        startScreen,
        credits,
       minibossBattle,

    }

    public enum SFXList
    {
        playerPunch,
        playerDie,
        doorOpen,
        explosion,
    }

    

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //cuando cargue una escena ponemos una musica específica
        switch(scene.name)
        {
            case "Menu":

                break;
            case "LevelZero":
                PlayMusic(MusicList.levelZero, true);
                break;

            default:

                break;
        }
    }
    
    // called when the game is terminated
    void OnDisable()
    {
        
    }
    [System.Serializable] //esto hace que la clase...
    public class MusicClips
    {
        public MusicList musicID; //identificador
        public AudioClip musicClip; //el archivo
    }
    public List<MusicClips> music;
    
    [System.Serializable]
    public class SFXClips
    {
        public SFXList sfxID; //id del enmum
        public AudioClip sfxClip;// archivo

    }
    public List<SFXClips> sfx;
}
