using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;

public class coords : MonoBehaviour
{
    private void Update()
    {
        Debug.Log("Player position: " + transform.localPosition);
    }
}