using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item",menuName = "Resources/ScriptableObjects/items")]
public class Item:ScriptableObject
{
    public int id;
    public string name;
    public Sprite art;
    public int value;
}
