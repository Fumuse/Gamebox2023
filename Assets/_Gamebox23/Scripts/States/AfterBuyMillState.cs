public class AfterBuyMillState : IState
{
    public string StateMnemonic { get; } = "afterBuyMill";
    public IState NextState { get; } = new AfterBuyBakeState();
    
    public void OnStateSetAction()
    {
        GameManager gameManager = GameManager.instance;
        
        gameManager.Mill = GameManager.Instantiate(gameManager.millBuyTriggerPrefab.Machine);
        gameManager.Mill.transform.position = gameManager.MillTrigger.GetComponent<BuyMachine>()
            .MachineSpawnPoint.position;
        GameManager.Destroy(gameManager.MillTrigger);
        
        gameManager.BakeTrigger = GameManager.Instantiate(gameManager.bakeBuyTriggerPrefab.gameObject);
        gameManager.BakeTrigger.transform.position = gameManager.bakeTriggerPoint.transform.position;
    }

    public void BeforeStateCloseAction()
    {
        
    }
}