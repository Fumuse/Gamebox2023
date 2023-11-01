using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class PlayerGUI : MonoBehaviour
{
    [SerializeField] private Image _inventoryFolder;
    [SerializeField] private PlayerController _player;
    [SerializeField] private GUIInvetoryItem _storageItemPrefab;

    private Storage _storage;
    
    private void Start()
    {
        _storage = _player.GetComponent<Storage>();
    }

    private void Update()
    {
        DrawStorageItems();
    }

    private void DrawStorageItems()
    {
        foreach (Slot slot in _storage.StorageSlots)
        {
            if (slot.GUIItem == null)
            {
                GUIInvetoryItem item = Instantiate(_storageItemPrefab, _inventoryFolder.transform);
                item.ItemIcon.sprite = slot.Item.Icon;
                item.ItemAmountText.text = slot.Amount.ToString();

                slot.GUIItem = item;
            }
        }
    }
}