public interface IGatherableItem
{
    public float ItemCount { get; set; }

    public float MaxCount { get; }
    
    public Item Item { get; }
    
    public void AfterGatherAction();
}
