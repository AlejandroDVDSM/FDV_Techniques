using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private int _spawnAmount = 3;
    [SerializeField] private float _repeatRate = 2.5f;

    private List<Item> _itemPool;
    private int _currentItemIndex;
    
    private void Start()
    {
        InitItemPool();
    }

    private void InitItemPool()
    {
        _itemPool = new List<Item>();

        for (int i = 0; i < _spawnAmount; i++)
        {
            _itemPool.Add(Instantiate(_itemPrefab, transform));
            _itemPool[i].gameObject.SetActive(false);
        }
        
        InvokeRepeating(nameof(SetPooledItemActive), 0, _repeatRate);
    }

    private void SetPooledItemActive()
    {
        
        if (_currentItemIndex < _itemPool.Count)
        {
            _itemPool[_currentItemIndex].gameObject.SetActive(true);
            
            _currentItemIndex++;
        }
        else
            _currentItemIndex = 0;
    }

    public void UnsubscribeItem(Item item)
    {
        _itemPool.Remove(item);
    }
}
