[System.Serializable]
public class SerializeGatheringItem
{
    public float itemCount;
    public string owner;
    
    public static SerializeGatheringItem SerializedItem(IGatherableItem gatherItem, string objectName)
    {
        SerializeGatheringItem item = new();
        item.owner = objectName;
        item.itemCount = gatherItem.ItemCount;
        return item;
    }

    public override string ToString()
    {
        return $"{itemCount} {owner}";
    }
}
