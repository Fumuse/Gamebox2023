using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private List<Slot> _storageSlots = new();

    public List<Slot> StorageSlots => _storageSlots;

    private void Awake()
    {
        foreach (SerializeSlot slot in SaveSerial.Storages)
        {
            if (slot.owner != gameObject.name) continue;
            
            Item item = Resources.Load<Item>(slot.item.assetName);
            _storageSlots.Add(new Slot(item, slot.amount, slot.owner));
        }
    }

    public bool Add(Item item, int amount = 1)
    {
        Slot storageSlot = Contains(item);
        if (storageSlot != null)
        {
            storageSlot.Amount += amount;
        }
        else
        {
            storageSlot = new Slot(item, amount, gameObject.name);
            _storageSlots.Add(storageSlot);
        }
        
        SaveSerial.AddStoragesSlot(storageSlot.SerializedSlot());

        return true;
    }
    
    public bool Remove(Item item, int amount = -1)
    {
        bool result = false;
        Slot storageSlot = Contains(item);
        if (storageSlot != null)
        {
            storageSlot.Amount += amount;
            SaveSerial.AddStoragesSlot(storageSlot.SerializedSlot());

            if (storageSlot.Amount <= 0)
            {
                if (storageSlot.GUIItem != null) Destroy(storageSlot.GUIItem.gameObject);
            
                SaveSerial.RemoveStoragesSlot(storageSlot.SerializedSlot());
                _storageSlots.Remove(storageSlot);
            }
            
            result = true;
        }

        return result;
    }

    [CanBeNull]
    public Slot Contains(Item item)
    {
        Slot desiredSlot = null;
        foreach (Slot slot in _storageSlots)
        {
            if (slot.Item == item)
            {
                desiredSlot = slot;
                break;
            }
        }

        return desiredSlot;
    }

#if UNITY_EDITOR
    public override string ToString()
    {
        string storage = "";
        
        foreach (Slot slot in _storageSlots)
        {
            storage += slot.Item.Name + " " + slot.Amount + "\n";
        }
        
        return storage;
    }
#endif
}
