using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicZone : MonoBehaviour
{
    [Header("FMOD Event przypisany do tej strefy")]
    [SerializeField] private EventReference musicEvent;

    private EventInstance instance;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // jeśli coś już gra, zatrzymaj
        if (instance.isValid())
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }

        instance = RuntimeManager.CreateInstance(musicEvent);
        instance.start();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (instance.isValid())
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }
    }
}