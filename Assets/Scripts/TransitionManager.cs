using UnityEngine;
// using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    // [Header("Effects")]
    // public PostProcessVolume postVolume;       
    // public float effectDuration = 2f;

    public int transition = 0;

    [Header("Audio")]
    public AudioSource ambientAudio;     
    public AudioSource glitchAudio;   
    [Tooltip("Audio to play when StartHorrorTransition2 runs")]
    public AudioSource transition2Audio;        

    [Header("Lighting")]
    public Light[] lightsToFlicker;
    public float flickerDuration = 1f;
    public Color endColor = Color.red;

    [Tooltip("How long each light stays on during sequential flicker")]
    public float seqOnDuration = 0.2f;
    [Tooltip("Pause between each light during sequential flicker")]
    public float seqOffDuration = 0.1f;

    public GameObject Slender;

    public void StartHorrorTransition()
    {
        Slender.SetActive(true);
        slenderMovement mover = Slender.GetComponent<slenderMovement>();
        if (mover != null)
        {
            mover.SetPositionIndex(0);
        }
        StartCoroutine(TransitionSequence());
        transition++;
    }

    public void StartHorrorTransition2()
    {
        Slender.SetActive(true);
        slenderMovement mover = Slender.GetComponent<slenderMovement>();
        if (mover != null)
            mover.SetPositionIndex(1);

        // play the new audio for transition 2
        if (transition2Audio != null)
            transition2Audio.Play();

        // start the sequential flicker
        StartCoroutine(SequentialFlicker());
        StartCoroutine(TransitionSequence2());
        transition++;
    }

    private IEnumerator TransitionSequence()
    {
        // 1) Fade out ambient audio
        ambientAudio.Stop();
        // 2) Play glitch audio
        Debug.Log("Glitched audio should play");
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
    private IEnumerator TransitionSequence2()
    {
        while (true)
        {
            var L = lightsToFlicker[1];
            if (Random.value > 0.5f)
            {
                L.enabled = !L.enabled;
            }
            float waitTime = Random.Range(0.05f, 0.3f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator SequentialFlicker()
    {
    foreach (var L in lightsToFlicker)
        L.enabled = false;

    while (true)
    {
        int i = 0;
        foreach (var L in lightsToFlicker)
        {
            i++;
            if (i == 2)
            {
                continue;
            }
            L.enabled = true;
            yield return new WaitForSeconds(seqOnDuration);

            L.enabled = false;
            yield return new WaitForSeconds(seqOffDuration);
        }
    }
    }
}
