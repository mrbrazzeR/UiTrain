using System.Text;
using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] TMP_Text ItemNameText;
    [SerializeField] TMP_Text ItemSlotText;
    [SerializeField] TMP_Text ItemStatsText;
    
    private StringBuilder sb= new StringBuilder();


    void Start()
    {
        HideToolTip();
    }


    public void ShowTooltip(Item item)
    {
        ItemNameText.text = item.name;
        ItemSlotText.text = "";
        ItemStatsText.text = "";
        gameObject.SetActive(true);
    }
    
    public void ShowTooltip(EquipableItem item)
    {
        ItemNameText.text = item.name;
        ItemSlotText.text = item.EquipmentType.ToString();
        sb.Length = 0;
        AddStat(item.strength,"Strength");
        AddStat(item.agility,"Agility");
        AddStat(item.intelligence,"Intelligence");
        AddStat(item.vitality,"vitality");
        ItemStatsText.text = sb.ToString();
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine(); 
            }

            if (value > 0)
            {
                sb.Append("+");
            }

            sb.Append(value);
            sb.Append(" ");
            sb.Append(statName);
        }
    }
}
