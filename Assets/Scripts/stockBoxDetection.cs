using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class stockBoxDetection : MonoBehaviour
{
   public int itemsNeeded = 4;
   public int stockedItems = 0;
   public GameObject stockArea;
   public UnityEvent onStocked;
   public InstructionManager uiManager;


   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("StockableItem"))
       {
           stockedItems++;
           if (stockedItems >= itemsNeeded){
            onStocked.Invoke();
            stockArea.SetActive(false);
            uiManager.ShowInstruction(
                "Power Failure Detected.\n" +
                "Beginning emergency battery backup system.\n" +
                "Please reset the power switch located in the back of the building."
            );
           }
       }
   }

}
