using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyRewardCollection : ScriptableObject
{
    public DailyRewardData[] dataGroups;
}
[System.Serializable]
public class DailyRewardData
{
    public int id;
    public string name;
}
