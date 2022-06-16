using System.Collections;
using System.Collections.Generic;
using UiTrain.CharacterStats;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;
    private CharacterStat[] stats;
    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }

    public void SetStats(params CharacterStat[] characterStats)
    {
        stats = characterStats;
        if (stats.Length > statDisplays.Length)
        {
            Debug.Log("error");
            return;
        }

        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i<statDisplays.Length);
            if (i < stats.Length)
            {
                statDisplays[i].Stat = stats[i];    
            }
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].UpdateValue();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].Name = statNames[i];
        }
    }
}
