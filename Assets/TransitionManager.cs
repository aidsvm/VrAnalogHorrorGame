using UnityEngine;
// using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    // [Header("Effects")]
    // public PostProcessVolume postVolume;       
    // public float effectDuration = 2f;

    // [Header("Audio")]
    // public AudioSource ambientAudio;     
    // public AudioSource glitchAudio;           

    [Header("Lighting")]
    public Light[] lightsToFlicker;
    public float flickerDuration = 1f;

    // [Header("Spectral")]
    // public GameObject ghostPrefab;
    // public Transform ghostSpawnPoint;


    public void StartHorrorTransition()
    {
        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {
    
        // 3) Flicker lights
        float flickerEnd = Time.time + flickerDuration;
        while (Time.time < flickerEnd)
        {
            foreach (var L in lightsToFlicker)
                L.enabled = !L.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        // ensure lights end off (or on, your choice)
        foreach (var L in lightsToFlicker)
            L.enabled = false;
    }
}
