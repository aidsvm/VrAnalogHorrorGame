using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statusText;  // assign in Inspector

    public void ShowPowerFailure()
    {
        statusText.text =
          "Power Failure Detected.\n" +
          "Beginning emergency battery backup system.\n" +
          "Please reset the power switch located in the back of the building.";
    }
}
