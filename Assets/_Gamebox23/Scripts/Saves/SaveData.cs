using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<SerializeSlot> Storages { get; set; }
    public string Stage { get; set; }
    public List<SerializeGatheringItem> GatheringItems { get; set; }
}