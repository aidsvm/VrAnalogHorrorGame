using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public int transition = 0;

    [Header("Audio")]
    public AudioSource ambientAudio;
    [HideInInspector]
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

    [Header("Slenderman")]
    public GameObject Slender;

    // Reference to your jumpscare detection script
    private SlenderJumpscare jumpscareScript;

    void Start()
    {
        // Grab the AudioSource (static/jumpscare) on Slenderman
        glitchAudio = Slender.GetComponent<AudioSource>();

        // Cache and disable the SlenderJumpscare script until event 2
        jumpscareScript = Slender.GetComponent<SlenderJumpscare>();
        if (jumpscareScript == null)
        {
            Debug.LogWarning("SlenderJumpscare component missing on Slender GameObject.");
        }
        else
        {
            jumpscareScript.enabled = false;
        }
    }

    public void StartHorrorTransition()
    {
        Slender.SetActive(true);
        var mover = Slender.GetComponent<slenderMovement>();
        if (mover != null) mover.SetPositionIndex(0);

        StartCoroutine(TransitionSequence());
        transition++;
    }

    public void StartHorrorTransition2()
    {
        Slender.SetActive(true);
        var mover = Slender.GetComponent<slenderMovement>();
        if (mover != null)
            mover.SetPositionIndex(1);

        // play the new audio for transition 2
        if (transition2Audio != null)
            transition2Audio.Play();

        // start the flicker coroutines
        StartCoroutine(SequentialFlicker());
        StartCoroutine(TransitionSequence2());

        // ENABLE the jumpscare detection
        if (jumpscareScript != null)
            jumpscareScript.enabled = true;

        transition++;
    }

    private IEnumerator TransitionSequence()
    {
        ambientAudio.Stop();
        glitchAudio.Play();

        float flickerEnd = Time.time + flickerDuration;
        while (Time.time < flickerEnd)
        {
            foreach (var L in lightsToFlicker)
                L.enabled = !L.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        foreach (var L in lightsToFlicker)
        {
            L.enabled = true;
            L.color = endColor;
        }
    }

    private IEnumerator TransitionSequence2()
    {
        while (true)
        {
            var L = lightsToFlicker[1];
            if (Random.value > 0.5f)
                L.enabled = !L.enabled;

            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
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
                if (i == 2) continue;

                L.enabled = true;
                yield return new WaitForSeconds(seqOnDuration);

                L.enabled = false;
                yield return new WaitForSeconds(seqOffDuration);
            }
        }
    }
}
