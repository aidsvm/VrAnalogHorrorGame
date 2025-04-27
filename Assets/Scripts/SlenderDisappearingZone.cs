using UnityEngine;

public class SlenderDisappearingZone : MonoBehaviour
{
    [Tooltip("Drag your Slenderman GameObject here")]
    public GameObject slenderman;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            slenderman.SetActive(false);
        }
    }
}
