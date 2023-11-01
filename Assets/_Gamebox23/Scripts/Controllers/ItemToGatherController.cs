using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(IGatherableItem))]
public class ItemToGatherController : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _counterSpeed = 0.75f;
    [SerializeField, CanBeNull] private TextMeshProUGUI _counterText;

    private Storage _playerStorage;
    private IGatherableItem _gatherItem;
    private float _storageCounter = 0;  
    private float _counterSpeedWithDeltaTime;
    private bool _isEmpty = false;

    private void Start()
    {
        _gatherItem = GetComponent<IGatherableItem>();
        SetCounterText();
    }

    private void Update()
    {
        _counterSpeedWithDeltaTime = _counterSpeed * Time.deltaTime * 100;
    }

    /// <summary>
    /// Отображение счётчика количества в UI
    /// </summary>
    private void SetCounterText()
    {
        if (_counterText != null) 
            _counterText.text = GetCounterCount().ToString();
    }

    /// <summary>
    /// Получение текущего кол-ва предмета
    /// </summary>
    /// <returns></returns>
    private float GetCounterCount()
    {
        return _gatherItem.MaxCount > 0
            ? _gatherItem.ItemCount
            : Math.Abs(_gatherItem.MaxCount) + _gatherItem.ItemCount;
    }

    private bool CounterActive()
    {
        float count = GetCounterCount();
        if (_gatherItem.MaxCount > 0 && count <= 0) return false;
        if (_gatherItem.MaxCount < 0 && count >= Math.Abs(_gatherItem.MaxCount)) return false;
        return true;
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
        if (_isEmpty) return;

        if (other.CompareTag("Player"))
        {
            if (!CounterActive())
            {
                OnGatherEnd();
                return;
            }
            
            bool canChangeCounter = true;
            if (_playerStorage != null)
            {
                _storageCounter += _counterSpeedWithDeltaTime;
                
                if (_storageCounter > 1)
                {
                    _storageCounter--;
                    
                    if (_gatherItem.MaxCount > 0)
                    {
                        _playerStorage.Add(_gatherItem.Item);
                        _gatherItem.ItemCount--;
                    }
                    else if (_gatherItem.MaxCount < 0)
                    {
                        canChangeCounter = _playerStorage.Remove(_gatherItem.Item);
                        if (canChangeCounter) _gatherItem.ItemCount++;
                    }
                    else canChangeCounter = false;
                    
                    if (canChangeCounter)
                    {
                        SetCounterText();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isEmpty) return;
        if (other.CompareTag("Player"))
        {
            if (!CounterActive())
            {
                OnGatherEnd();
            }

            _playerStorage = null;
        }
    }

    public void OnGatherEnd()
    {
        _isEmpty = true;
        _gatherItem.AfterGatherAction();
    }

    public void ReInitialization()
    {
        _isEmpty = false;
        _storageCounter = 0;
    }
}
