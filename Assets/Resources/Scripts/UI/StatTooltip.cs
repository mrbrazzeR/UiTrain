using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UiTrain.CharacterStats;
using Unity.VisualScripting;
using UnityEngine;

public class StatTooltip : MonoBehaviour
{
    [SerializeField] TMP_Text StatNameText;
    [SerializeField] TMP_Text StatModifierLabelText;
    [SerializeField] TMP_Text StatModifiersText;
    
    private StringBuilder sb= new StringBuilder();

    
    void Start()
    {
        HideToolTip();
    }
    public void ShowTooltip(CharacterStat stat, string statName)
    {
        StatNameText.text = GetStatNameText(statName);
        StatModifierLabelText.text = GetStatTopText(stat, statName);
        StatModifiersText.text = GetStatModifiersText(stat);
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

    private string GetStatNameText(string statName)
    {
        sb.Length = 0;
        sb.Append(statName);
        return sb.ToString().ToUpper();
    }
    private string GetStatTopText(CharacterStat stat, string statName)
    {
        sb.Length = 0;
        sb.Append(stat.Value);
        if (stat.Value > stat.BaseValue)
        {
            sb.Append(" (");

            sb.Append(stat.BaseValue);
            if (stat.Value > stat.BaseValue)
            {
                sb.Append("+");
                sb.Append(stat.Value - stat.BaseValue);
            }
            sb.Append(")");
        }
        return sb.ToString();
    }

    private string GetStatModifiersText(CharacterStat stat)
    {
        sb.Length = 0;
        foreach (StatModifier mod in stat.StatModifiers)
        {
            EquipableItem item=mod.Source as EquipableItem;

            if (item != null)
            {
                sb.Append(" ");
                sb.Append(item.name);
            }
            if (mod.Value > 0)
            {
                sb.Append("+");
            }

            sb.Append(mod.Value);
            sb.Append("\n");
        }
        return sb.ToString();
    }
}
