using UnityEngine;
using UnityEngine.Events;

public class TrashCan : MonoBehaviour
{
    [Tooltip("How many trash pieces must be dropped before the scare event")]
    public int totalTrashCount = 1;

    [Tooltip("Event to fire when all trash is in the can")]
    public UnityEvent onAllTrashCollected;

    private int _collected = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            _collected++;
            Debug.Log("Trash collected: " + _collected);

            // remove the trash from the scene
            Destroy(other.gameObject);

            if (_collected >= totalTrashCount)
                onAllTrashCollected.Invoke();
        }
    }
}
