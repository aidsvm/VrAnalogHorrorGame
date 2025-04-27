using UnityEngine;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    [Tooltip("Drag your TMP text here")]
    public TextMeshProUGUI instructionText;

    /// <summary>
    /// Call this to change what's shown on the wall.
    /// </summary>
    public void ShowInstruction(string msg)
    {
        instructionText.text = msg;
    }
}
