using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot
{
    [SerializeField] private Item _item;
    [SerializeField] private int _amount;
    private GUIInvetoryItem _guiItem;
    
    public string Owner { get; set; }

    public Item Item
    {
        get => _item;
        private set => _item = value;
    }

    public int Amount
    {
        get => _amount;
        set {
            _amount = value;

            if (GUIItem != null)
            {
                GUIItem.ItemAmountText.text = _amount.ToString();
            }
        }
    }

    public Slot(Item item, int amount, string owner)
    {
        this.Item = item;
        this.Amount = amount;
        this.Owner = owner;
    }

    public GUIInvetoryItem GUIItem
    {
        get => _guiItem;
        set => _guiItem = value;
    }

    public SerializeSlot SerializedSlot()
    {
        SerializeSlot slot = new();
        slot.amount = Amount;
        slot.item = Item.item;
        slot.owner = Owner;
        return slot;
    }
}