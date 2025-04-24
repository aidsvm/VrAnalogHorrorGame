using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slenderCollision : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("PlayerNear", true);
            Debug.Log("Player entered his domain");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("PlayerNear", false);
        }
    }

    private void Update()
    {
        Debug.Log(anim.GetBool("PlayerNear"));
    }
}