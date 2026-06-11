using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Muzyka : MonoBehaviour
{
    public static Muzyka Instance;

    [Header("FMOD Event Path")]
    [SerializeField] private EventReference musicEvent;

    private EventInstance musicInstance;

    private void Awake()
    {
        // Singleton (żeby nie tworzyło się kilka muzyczek)
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        // jeśli już gra, nie twórz nowej
        if (musicInstance.isValid())
            return;

        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    public void StopMusic()
    {
        if (!musicInstance.isValid())
            return;

        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }

    private void OnDestroy()
    {
        StopMusic();
    }
}