using UnityEngine;
using TMPro;                        // ← for TextMeshProUGUI
using System.Collections;

public class SwitchController : MonoBehaviour
{

    public AudioSource myAmbientAudio;

    private TransitionManager _transitionManager;

    void Awake()
    {
        // find the TransitionManager anywhere in the scene
        _transitionManager = FindObjectOfType<TransitionManager>();

        if (_transitionManager == null)
            Debug.LogWarning("No TransitionManager found in scene!");
    }

    [Header("Knob Transform")]
    public Transform mainKnob;
    [Tooltip("How far the knob moves in local Y (if you still want positional fallback)")]
    public float travelDistance = 0.2f;

    [Header("Knob Rotation")]
    [Tooltip("Local X rotation when switch is ON")]
    public float onRotationX = -81.942f;

    [Header("Ceiling Lights")]
    [Tooltip("Drag all 7 ceiling Point‐Lights here")]
    public Light[] ceilingLights;

    [Tooltip("What color should lights be when OFF?")]
    public Color offColor = Color.red;

    // cached states
    private Vector3 downPos, upPos;
    private Vector3 downRotEuler, upRotEuler;
    private bool isOn = false;
    private Color[] originalColors;

    [Header("UI Text (World-Space Canvas)")]
    [Tooltip("Drag your wall-UI TextMeshProUGUI here")]
    public TextMeshProUGUI instructionText;

    void Start()
    {
        // cache original local position (in case you still want movement)
        downPos = mainKnob.localPosition;
        upPos = downPos + Vector3.up * travelDistance;

        // cache original rotation and compute "on" rotation
        downRotEuler = mainKnob.localEulerAngles;
        upRotEuler = new Vector3(onRotationX, downRotEuler.y, downRotEuler.z);

        // cache each light’s "on" color
        originalColors = new Color[ceilingLights.Length];
        for (int i = 0; i < ceilingLights.Length; i++)
            originalColors[i] = ceilingLights[i].color;
    }

    /// <summary>
    /// Call this from your XR interaction or button press
    /// </summary>
    public void Toggle()
    {
        isOn = !isOn;

        // optional: move the knob up/down (uncomment if needed)
        // mainKnob.localPosition = isOn ? upPos : downPos;

        // rotate the knob around its local X axis
        mainKnob.localEulerAngles = isOn ? upRotEuler : downRotEuler;

        // swap the light colors
        for (int i = 0; i < ceilingLights.Length; i++)
            ceilingLights[i].color = isOn
                ? originalColors[i]
                : offColor;

        // 1) Stop whatever audio the TransitionManager is playing:
        if (_transitionManager.ambientAudio.isPlaying)
            _transitionManager.ambientAudio.Stop();

        if (_transitionManager.glitchAudio.isPlaying)
            _transitionManager.glitchAudio.Stop();

        // 2) Start _your_ ambient audio
        if (!myAmbientAudio.isPlaying)
            myAmbientAudio.Play();

        if (isOn)
        {
            // **All** of your power-reset instructions live here now:
            instructionText.text =
                "Power system reset. Continue with your tasks please.\n" +
                "Our gas station must be in clean and immaculate conditions, " +
                "please pick up the trash outside by the gas pumps to ensure " +
                "a happy fueling experience!";
        }

    }
}
