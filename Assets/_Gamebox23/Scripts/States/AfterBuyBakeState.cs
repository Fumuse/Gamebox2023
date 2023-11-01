public class AfterBuyBakeState : IState
{
    public string StateMnemonic { get; } = "afterBuyBake";
    public IState NextState { get; }
    
    public void OnStateSetAction()
    {
        GameManager gameManager = GameManager.instance;

        gameManager.Bake = GameManager.Instantiate(gameManager.bakeBuyTriggerPrefab.Machine);
        gameManager.Bake.transform.position = gameManager.BakeTrigger.GetComponent<BuyMachine>()
            .MachineSpawnPoint.position;
        
        GameManager.Destroy(gameManager.BakeTrigger);
    }

    public void BeforeStateCloseAction()
    {
        
    }
}