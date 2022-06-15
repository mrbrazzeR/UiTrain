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
    void Start()
    {
        string[] invenData = File.ReadAllLines(Application.dataPath + path);

        invenData = invenData.Skip(1).ToArray();

       foreach(string data in invenData)
        {
            string[] row = data.Split(new char[] {','});
            InventoryData inventoryData=new InventoryData();
            int.TryParse(row[0], out inventoryData.id);
            inventoryData.name = row[1];
            int.TryParse(row[2], out inventoryData.value);
            invenCollection.dataGroups.Add(inventoryData);
            Debug.Log(inventoryData.name);
        }
    }
    

}
