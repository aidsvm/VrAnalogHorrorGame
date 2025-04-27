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
    public AudioSource ambientAudio;     
    public AudioSource glitchAudio;           

    [Header("Lighting")]
    public Light[] lightsToFlicker;
    public float flickerDuration = 1f;
    public Color endColor = Color.red;
    public GameObject Slender;

    // [Header("Spectral")]
    // public GameObject ghostPrefab;
    // public Transform ghostSpawnPoint;


    public void StartHorrorTransition()
    {
        Slender.SetActive(true);
        slenderMovement mover = Slender.GetComponent<slenderMovement>();
        Animation anim = Slender.GetComponent<Animation>();
        if (mover != null)
        {
            mover.SetPositionIndex(0);
            anim.Play("Idle");
        }

        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {
        // 1) Fade out ambient audio
        ambientAudio.Stop();
        // 2) Play glitch audio
        glitchAudio.Play();

        // 3) Flicker lights
        float flickerEnd = Time.time + flickerDuration;
        while (Time.time < flickerEnd)
        {
            foreach (var L in lightsToFlicker)
                L.enabled = !L.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    
        foreach (var L in lightsToFlicker) {
            L.enabled = true;
            L.color   = endColor;
        }
    }
}
