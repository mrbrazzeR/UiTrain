using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;

public class CSVtoSO:MonoBehaviour
{
    public InventoryCollection invenCollection;
    private static string path = "/Resources/csv/inventory.csv";
    private static bool added=false;
    void Start()
    {
        string[] invenData = File.ReadAllLines(Application.dataPath + path);

        invenData = invenData.Skip(1).ToArray();

       foreach(string data in invenData)
        {
            string[] row = data.Split(new char[] {','});
            Item item=new Item();
            int.TryParse(row[0], out item.id);
            item.name = row[1];
            int.TryParse(row[2], out item.value);
            foreach (Item addedData in invenCollection.dataGroups)
            {
                if(addedData.id==item.id)
                {
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                invenCollection.dataGroups.Add(item);
            }
            else
            {
                added = false;
            }
        }
    }
    

}
