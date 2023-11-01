public class StartState : IState
{

    public string StateMnemonic { get; } = "start";
    public IState NextState { get; } = new AfterBuyMillState();
    
    public void OnStateSetAction()
    {
        GameManager gameManager = GameManager.instance;
        
        gameManager.MillTrigger = GameManager.Instantiate(gameManager.millBuyTriggerPrefab.gameObject);
        gameManager.MillTrigger.transform.position = gameManager.millTriggerPoint.transform.position;
    }

    public void BeforeStateCloseAction()
    {
        
    }

    public override string ToString()
    {
        return this.StateMnemonic;
    }
}