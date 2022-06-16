using UnityEngine;
using System.IO;
using System.Linq;

public class CSVtoSO:MonoBehaviour
{
    public InventoryCollection invenCollection;
    private static string path = "/Resources/csv/inventory.csv";
    private static bool added=false;
    void Start()
    {
        if (!File.Exists(path))
        {
            // Create a file to write to.
            string[] createText = { "Hello", "And", "Welcome" };
            File.WriteAllLines(Application.dataPath+path, createText);
        }
        string[] invenData = File.ReadAllLines(Application.dataPath + path);

        invenData = invenData.Skip(1).ToArray();

       foreach(string data in invenData)
        {
            string[] row = data.Split(new char[] {','});
            Item item=new Item();
            int.TryParse(row[0], out item.id);
            item.name = row[1];
            int.TryParse(row[2], out item.strength);
            int.TryParse(row[3], out item.agility);
            int.TryParse(row[4], out item.intelligence);
            int.TryParse(row[5], out item.vitality);
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
