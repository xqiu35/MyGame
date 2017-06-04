using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager2D : MonoBehaviour {

    // Singleton
    private static MusicManager2D musicManager;

    // Privates
    private AudioSource audioSource;

    // Publics
    public AudioClip[] clips;
    
    void Awake()
    {
        if(musicManager==null)
        {
            musicManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(musicManager!=this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        initPrivates();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        //SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Logger.log(scene.name + " scene loaded");
    }

    void initPrivates()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    void Save()
    {
        Music2D data = new Music2D();
        data.volume = this.audioSource.volume;
        GameIO.saveData(FileName.MUSIC2D, data);
    }

    void Load()
    {
        Music2D data = GameIO.loadData<Music2D>(FileName.MUSIC2D);
        audioSource.volume = data.volume;
    }

    public void SetVolume(float v)
    {
        audioSource.volume = v;
    }
}
