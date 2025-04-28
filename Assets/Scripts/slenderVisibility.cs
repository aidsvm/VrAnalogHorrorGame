using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class slenderVisibility : MonoBehaviour
{
    public float jumpscareDelay = 0f;
    private TransitionManager tm;
    private slenderMovement mover;
    private Animator anim;
    public Transform player;
    private AudioSource glitchAudio;
    public AudioSource ambientAudio;
    public AudioSource endScreenAudio;
    private bool playerTurnedAround = false;
    public Image image;

    private void Start()
    {
        glitchAudio = GetComponent<AudioSource>();
        mover = GetComponent<slenderMovement>();
        anim = GetComponent<Animator>();
        tm = GetComponent<TransitionManager>();
    }

    private void Update()
    {   
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (PlayerTurnedAround() && !playerTurnedAround)
        {
            if (stateInfo.IsName("Idle"))
            {
                anim.speed = 3f;
            }
            Debug.Log("Player turned around!");
            playerTurnedAround = true;
            StartCoroutine(TriggerJumpscareAfterDelay());
        }
    }

    bool PlayerTurnedAround()
    {
        float angle = player.transform.rotation.y;
        return (angle > 0.9f || angle < -0.9f);
    }

    private IEnumerator TriggerJumpscareAfterDelay()
    {
        glitchAudio.Play();
        playerTurnedAround = true;
        yield return new WaitForSeconds(jumpscareDelay);
        anim.speed = 1f;
        anim.SetBool("PlayerTurned", true);
    }

    public void OnAttackEnd()
    {
        Debug.Log("Attack animation finished!");
        anim.SetBool("HasAttacked", true);
        glitchAudio.Stop();
        ambientAudio.Stop();
        endScreenAudio.Play();
        image.gameObject.SetActive(true);
    }
}
