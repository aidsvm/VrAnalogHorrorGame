using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class stockBoxDetection : MonoBehaviour
{
   public int itemsNeeded = 4;
   public int stockedItems = 0;
   public UnityEvent onStocked;

   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("StockableItem"))
       {
           stockedItems++;
           Debug.Log("Stocked item: " + stockedItems);
           if (stockedItems >= itemsNeeded)
           {
               onStocked.Invoke();
               Debug.Log("All items stocked!");
           }
       }
   }

}
