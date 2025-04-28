using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlenderJumpscare : MonoBehaviour
{
    [Header("References")]
    public Transform player;                // your VR camera/player
    public AudioSource ambientAudio;        // background music
    public AudioSource endScreenAudio;      // “game over” music
    public Image endScreenImage;            // full-screen “you died” UI

    [Header("Settings")]
    public float detectionAngle = 20f;      // degrees cone
    public float jumpscareDelay   = 0.5f;   // seconds between static and animation

    private Animator anim;
    private AudioSource glitchAudio;
    private bool hasTriggered = false;

    private void Start()
    {
        // grab your Animator and existing AudioSource on this GameObject
        anim        = GetComponent<Animator>();
        glitchAudio = GetComponent<AudioSource>();

        if (anim == null)
            Debug.LogError("SlenderJumpscare: no Animator found on Slenderman!");
        if (glitchAudio == null)
            Debug.LogError("SlenderJumpscare: no AudioSource found for static noise!");
    }

    private void Update()
    {
        if (!hasTriggered && PlayerIsLookingAtMe())
        {
            hasTriggered = true;
            StartCoroutine(DoJumpscare());
        }
    }

    private bool PlayerIsLookingAtMe()
    {
        Vector3 toSlender = (transform.position - player.position).normalized;
        float   angle     = Vector3.Angle(player.forward, toSlender);
        return angle < detectionAngle;
    }

    private IEnumerator DoJumpscare()
    {
        // stop ambience, play static
        ambientAudio?.Stop();
        glitchAudio.Play();

        yield return new WaitForSeconds(jumpscareDelay);

        // now trigger the jumpscare animation
        if (anim != null)
            anim.SetTrigger("Jumpscare");
    }

    // put an Animation Event at the end of your Scream/Jumpscare clip to call this:
    public void OnJumpscareEnd()
    {
        glitchAudio.Stop();
        endScreenAudio?.Play();
        endScreenImage.gameObject.SetActive(true);
    }
}
