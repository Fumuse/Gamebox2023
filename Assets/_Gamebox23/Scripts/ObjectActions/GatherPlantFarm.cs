using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[RequireComponent(typeof(ItemToGatherController))]
public class GatherPlantFarm : MonoBehaviour, IGatherableItem
{
    [SerializeField] private Item _item;
    [SerializeField, Min(1f)] private float _maxCount = 3f;
    [SerializeField] private float _growthTime = 5f;

    private Animator _animator;
    private ItemToGatherController _controller;
    private float _itemCount = 0f;
    private float _growthTimer = 0f;
    public float MaxCount => _maxCount;

    public float ItemCount
    {
        get => _itemCount;
        set {
            _itemCount = value;
            Growth();
        }
    }

    public Item Item => _item;
    
    
    private void Awake()
    {
        SerializeGatheringItem saveData = SaveSerial.GatheringItemProgressContains(gameObject.name);
        _itemCount = saveData?.itemCount ?? _maxCount;
    }
    
    private void Start()
    {
        _controller = GetComponent<ItemToGatherController>();
        _animator = GetComponent<Animator>();

        Animate();
    }

    private void Update()
    {
        if (ItemCount != MaxCount)
        {
            _growthTimer += Time.deltaTime;
            
            if (_growthTimer >= _growthTime)
            {
                _growthTimer = 0;

                ItemCount++;
            }
        }
    }

    private void Growth()
    {
        if (_itemCount != 0)
        {
            _controller.ReInitialization();
        }
        
        SaveSerial.AddGatheringItemProgress(SerializeGatheringItem.SerializedItem(this, gameObject.name));
        Animate();
    }

    private void Animate()
    {
        _animator?.SetFloat("GrowthStage", ItemCount / MaxCount);
    }

    public void AfterGatherAction()
    {
    }
}
