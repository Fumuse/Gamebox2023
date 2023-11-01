using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ItemToGatherController))]
public class BuyMachine : MonoBehaviour, IGatherableItem
{
    [SerializeField] private Item _item;
    [SerializeField] private float _maxCount = -10f;
    [SerializeField] private Transform _machineSpawnPoint;
    [SerializeField] private GameObject _machine;
    [SerializeField] private TextMeshProUGUI _costText;

    private float _itemCount = 0f;

    public float ItemCount
    {
        get => _itemCount;
        set
        {
            _itemCount = value;
            SaveSerial.AddGatheringItemProgress(SerializeGatheringItem.SerializedItem(this, gameObject.name));
        }
    }

    public GameObject Machine => _machine;
    public Transform MachineSpawnPoint => _machineSpawnPoint;

    public float MaxCount => _maxCount;
    public Item Item => _item;

    private void Awake()
    {
        SerializeGatheringItem saveData = SaveSerial.GatheringItemProgressContains(gameObject.name);
        _itemCount = saveData?.itemCount ?? _maxCount;
    }

    private void Start()
    {
        _costText.text = $"{_item.Name}:\n {Math.Abs(MaxCount)}";
    }
    
    public void AfterGatherAction()
    {
        SaveSerial.RemoveGatheringItemProgress(SerializeGatheringItem.SerializedItem(this, gameObject.name));
        StateMachine.instance.ChangeStateNext();
    }
}