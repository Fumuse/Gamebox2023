using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEngine;

public class SaveSerial : MonoBehaviour
{
    private static List<SerializeSlot> _storages = new();
    private static List<SerializeGatheringItem> _gatheringItems = new();
    
    private static string _state = "start";

    public static string State
    {
        get => _state;
        set
        {
            _state = value;
            SaveGame();
        }
    }

    public static List<SerializeSlot> Storages => _storages;
    public static List<SerializeGatheringItem> GatheringItems => _gatheringItems;

    public static void AddStoragesSlot(SerializeSlot slot)
    {
        SerializeSlot inStorageSlot = StorageContainsSlot(slot);

        if (inStorageSlot == null)
        {
            _storages.Add(slot);
        }
        else
        {
            inStorageSlot.amount = slot.amount;
        }

        SaveGame();
    }

    public static void RemoveStoragesSlot(SerializeSlot slot)
    {
        SerializeSlot inStorageSlot = StorageContainsSlot(slot);

        if (inStorageSlot != null)
        {
            _storages.Remove(inStorageSlot);
        }
        
        SaveGame();
    }

    [CanBeNull]
    public static SerializeSlot StorageContainsSlot(SerializeSlot slot)
    {
        SerializeSlot inStorageSlot = null;
        
        foreach (SerializeSlot storageSlot in _storages)
        {
            if (storageSlot.owner != slot.owner) continue;
            if (storageSlot.item.itemName != slot.item.itemName) continue;

            inStorageSlot = storageSlot;
            break;
        }

        return inStorageSlot;
    }
    
    
    public static void AddGatheringItemProgress(SerializeGatheringItem item)
    {
        SerializeGatheringItem inStorageItem = GatheringItemProgressContains(item);

        if (inStorageItem == null)
        {
            _gatheringItems.Add(item);
        }
        else
        {
            inStorageItem.itemCount = item.itemCount;
        }

        SaveGame();
    }

    public static void RemoveGatheringItemProgress(SerializeGatheringItem item)
    {
        SerializeGatheringItem inStorageItem = GatheringItemProgressContains(item);

        if (inStorageItem != null)
        {
            _gatheringItems.Remove(inStorageItem);
        }
        
        SaveGame();
    }

    [CanBeNull]
    public static SerializeGatheringItem GatheringItemProgressContains(SerializeGatheringItem item)
    {
        SerializeGatheringItem inStorageItem = null;
        
        foreach (SerializeGatheringItem storageItem in _gatheringItems)
        {
            if (storageItem.owner != item.owner) continue;
            
            inStorageItem = storageItem;
            break;
        }

        return inStorageItem;
    }
    
    [CanBeNull]
    public static SerializeGatheringItem GatheringItemProgressContains(string objectName)
    {
        SerializeGatheringItem inStorageItem = null;
        
        foreach (SerializeGatheringItem storageItem in _gatheringItems)
        {
            if (storageItem.owner != objectName) continue;
            
            inStorageItem = storageItem;
            break;
        }

        return inStorageItem;
    }

    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath + "/save.dat"); 
        
        SaveData data = new SaveData();

        data.Storages = Storages;
        data.Stage = State;
        data.GatheringItems = GatheringItems;
        
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            SaveData data = (SaveData) bf.Deserialize(file);
            file.Close();

            _storages = data.Storages;
            _gatheringItems = data.GatheringItems;
            State = data.Stage;
        }
    }

    private void Awake()
    {
        LoadGame();
    }
}
