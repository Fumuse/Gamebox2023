[System.Serializable]
public class SerializeSlot
{
    public int amount;
    public ItemData item;
    public string owner;

    public override string ToString()
    {
        return $"{item.itemName} {amount} {owner}";
    }
}