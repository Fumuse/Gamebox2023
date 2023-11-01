using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StorageItem", menuName = "Storage Item", order = 51)]
[System.Serializable]
public class Item : ScriptableObject
{
    public ItemData item = new();
    [SerializeField] private Sprite _icon;

    public string Name
    {
        get { return item.itemName; }
    }

    public Sprite Icon
    {
        get { return _icon; }
    }

    public override string ToString()
    {
        return Name;
    }
}

[System.Serializable]
public class ItemData
{
    public string itemName;
    public string assetName;
}