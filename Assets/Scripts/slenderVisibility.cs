using System.Collections;
using UnityEngine;

public class slenderVisibility : MonoBehaviour
{
    private Renderer rend;
    private bool hasTriggeredJumpscare = false;

    public float jumpscareDelay = 1.0f; // seconds after being seen

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>(); // or GetComponent<Renderer>() if it's on the same object
    }

    private void Update()
    {
        if (!hasTriggeredJumpscare && rend.isVisible)
        {
            hasTriggeredJumpscare = true;
            StartCoroutine(TriggerJumpscareAfterDelay());
        }
    }

    private IEnumerator TriggerJumpscareAfterDelay()
    {
        Debug.Log("Slenderman seen! Jumpscare will happen soon...");
        yield return new WaitForSeconds(jumpscareDelay);
        
        // Here you trigger your jumpscare
        Debug.Log("JUMPSCARE NOW!");

        // Example: Play scream animation, sounds, teleport, etc.
    }
}
