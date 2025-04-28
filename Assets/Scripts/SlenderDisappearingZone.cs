using UnityEngine;

public class SlenderDisappearingZone : MonoBehaviour
{
    [Tooltip("Drag your Slenderman GameObject here")]
    public GameObject slenderman;
    public GameObject outsideZone;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            slenderman.SetActive(false);
            outsideZone.SetActive(false);

        }
    }
}
