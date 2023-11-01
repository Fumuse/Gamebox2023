using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Storage))]
public class MachineController : MonoBehaviour
{
    [SerializeField] private Item _machineRecipeItem;
    [SerializeField] private Item _machineFinalItem;
    [SerializeField] private float _craftTime = 3f;
    [SerializeField] private float _fillingTime = 0.5f;
    [SerializeField, CanBeNull] private TextMeshProUGUI _storageCounterText;
    
    private Storage _playerStorage;
    private Storage _innerStorage;
    private float _craftTimer = 0f;
    private float _fillingTimer = 0f;

    private void Start()
    {
        _innerStorage = GetComponent<Storage>();
        UpdateCounterGUI();
    }

    private void Update()
    {
        CraftItem();
    }

    private void CraftItem()
    {
        Slot slot = _innerStorage.Contains(_machineRecipeItem);
        if (slot != null)
        {
            _craftTimer += Time.deltaTime;
            if (_craftTimer >= _craftTime)
            {
                _craftTimer = 0;
                _innerStorage.Remove(_machineRecipeItem);
                _innerStorage.Add(_machineFinalItem);
                
                UpdateCounterGUI();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool hasStorage = other.TryGetComponent(out Storage playerStorage);
            if (hasStorage)
            {
                _playerStorage = playerStorage;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _fillingTimer += Time.deltaTime;
            if (_fillingTimer > _fillingTime)
            {
                _fillingTimer = 0;
                
                FillingRecipeItem();
                UnloadingTotalItem();
                
                UpdateCounterGUI();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStorage = null;
        }
    }

    private void FillingRecipeItem()
    {
        Slot slot = _playerStorage.Contains(_machineRecipeItem);
        if (slot != null)
        {
            _innerStorage.Add(_machineRecipeItem);
            _playerStorage.Remove(_machineRecipeItem);
        }
    }

    private void UnloadingTotalItem()
    {
        Slot finalItemSlot = _innerStorage.Contains(_machineFinalItem);
        if (finalItemSlot != null)
        {
            _innerStorage.Remove(_machineFinalItem);
            _playerStorage.Add(_machineFinalItem);
        }
    }

    private void UpdateCounterGUI()
    {
        if (_storageCounterText == null) return;
        
        string text = "";

        Slot recipeItemSlot = _innerStorage.Contains(_machineRecipeItem);
        text += $"{recipeItemSlot?.Item.Name ?? _machineRecipeItem.Name} - {recipeItemSlot?.Amount ?? 0}\n";
        
        Slot finalItemSlot = _innerStorage.Contains(_machineFinalItem);
        text += $"{finalItemSlot?.Item.Name ?? _machineFinalItem.Name} - {finalItemSlot?.Amount ?? 0}\n";

        _storageCounterText.text = text;
    }
}
