using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] public Transform millTriggerPoint;
    [SerializeField] public BuyMachine millBuyTriggerPrefab;
    [SerializeField] public Transform bakeTriggerPoint;
    [SerializeField] public BuyMachine bakeBuyTriggerPrefab;

    public GameObject MillTrigger { get; set; }
    public GameObject BakeTrigger { get; set; }
    
    public GameObject Mill { get; set; }
    public GameObject Bake { get; set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void CloseGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
