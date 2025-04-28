using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slenderCollision : MonoBehaviour
{
    [Tooltip("Drag your Slenderman GameObject here")]
    private Animator anim;
    public GameObject Slender;
    public GameObject outsideZone;
    void Awake()
    {
        anim = Slender.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not found");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Scream beginning");
            anim.SetBool("Seen", true);
        }
    }

    // private void Update()
    // {
    //     Debug.Log(anim.GetBool("PlayerNear"));
    // }
}