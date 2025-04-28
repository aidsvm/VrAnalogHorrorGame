using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;

public class slenderMovement : MonoBehaviour
{
    public GameObject[] slenderPositions;
    public GameObject player;
    public int currPos = 0;
    public float rotationSpeed = 2f;
    public UnityEvent onPositionChanged;

    public void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
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