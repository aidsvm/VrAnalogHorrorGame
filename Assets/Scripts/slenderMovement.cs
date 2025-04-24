using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;

public class slenderMovement : MonoBehaviour
{
    public GameObject[] slenderPositions;
    public int currPos = 0;
    public UnityEvent onPositionChanged;

    private void Awake()
    {
        // ApplyPosition();
    }

    public void SetPositionIndex(int index)
    {
        if (slenderPositions == null || index < 0 || index >= slenderPositions.Length)
        {
            Debug.LogWarning($"Invalid slenderPositions index: {index}");
            return;
        }

        currPos = index;
        ApplyPosition();
        onPositionChanged?.Invoke();
    }

    private void ApplyPosition()
    {
        transform.localPosition = slenderPositions[currPos].transform.localPosition;
        transform.localRotation = slenderPositions[currPos].transform.localRotation;
    }
}