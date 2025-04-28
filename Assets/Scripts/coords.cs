using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coords : MonoBehaviour
{
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.localPosition;
        StartCoroutine(LogPositionIfMoved());
    }

    private IEnumerator LogPositionIfMoved()
    {
        while (true)
        {
            if (transform.localPosition != lastPosition)
            {
                Debug.Log("Player moved to: " + transform.localPosition);
                lastPosition = transform.localPosition;
            }
            yield return new WaitForSeconds(1f); // check every 1 second
        }
    }
}
