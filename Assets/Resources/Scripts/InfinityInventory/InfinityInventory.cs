using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityInventory : Inventory
{
   [SerializeField] private GameObject itemSlotPrefab;
   [SerializeField] private int maxSlots;

   public int MaxSlots
   {
      get
      {
         return maxSlots;
      }
      set
      {
         SetMaxSLots(value);
      }
   }

   protected override void Start()
   {
      SetMaxSLots(maxSlots);
      base.Start();
   }

   private void SetMaxSLots(int value)
   {
      if (value <= 0)
      {
         maxSlots = 1;
      }
      else
      {
         maxSlots = value;
      }

      if (maxSlots < itemSlots.Count)
      {
         for (int i = 0; i < itemSlots.Count; i++)
         {
            Destroy(itemSlots[i].transform.parent.gameObject);
         }

         int diff = itemSlots.Count - maxSlots;
         itemSlots.RemoveRange(maxSlots,diff);
      }
      else if(maxSlots>itemSlots.Count)
      {
         int diff = maxSlots - itemSlots.Count;
         for (int i = 0; i < diff; i++)
         {
            GameObject itemSlotGameObject = Instantiate(itemSlotPrefab, itemsParent, false) as GameObject;
            itemSlots.Add(itemSlotGameObject.GetComponentInChildren<ItemSlot>());
         }
      }
   }
}
