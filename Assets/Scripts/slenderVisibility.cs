using System.Collections;
using UnityEngine;

public class slenderVisibility : MonoBehaviour
{
    public float jumpscareDelay = 1.0f; // seconds after being seen
    private TransitionManager tm;
    private slenderMovement mover;
    private Animator anim;
    public Transform player; // The player object
    private Vector3 lastForwardDirection;
    private AudioSource glitchAudio;
    private bool playerTurnedAround = false;

    private void Start()
    {
        glitchAudio = GetComponent<AudioSource>();
        mover = GetComponent<slenderMovement>();
        anim = GetComponent<Animator>();
        tm = GetComponent<TransitionManager>();
        lastForwardDirection = player.forward; // Initial facing direction
    }

    private void Update()
    {
        if (PlayerTurnedAround() && (playerTurnedAround == false))
        {
            Debug.Log("Player turned around!");
            playerTurnedAround = true;
            StartCoroutine(TriggerJumpscareAfterDelay());
        }
    }

    bool PlayerTurnedAround()
    {
        // Use the dot product to check if the player has turned significantly
        // float angle = Vector3.Angle(lastForwardDirection, player.forward);

        // If the angle exceeds a certain threshold, consider it a "turn around"
        float angle = player.transform.rotation.y;
        return (angle > 0.9f || angle < -0.9f);
    }

    private IEnumerator TriggerJumpscareAfterDelay()
    {
        glitchAudio.Play();
        yield return new WaitForSeconds(jumpscareDelay);
        anim.SetBool("PlayerTurned", true);
    }

    public void OnAttackEnd()
    {
        Debug.Log("Attack animation finished!");
        anim.SetBool("HasAttacked", true);
    }
}
