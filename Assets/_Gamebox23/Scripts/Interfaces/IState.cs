public interface IState
{
    public string StateMnemonic { get; }
    
    public IState NextState { get; }
    
    public void OnStateSetAction();

    public void BeforeStateCloseAction();
}