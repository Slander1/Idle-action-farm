using System;
using System.Collections.Generic;
using System.Linq;
using Data.ResourceData;
using UI;
using UnityEngine;

public class CharacterResourcesView : MonoBehaviour
{
    [SerializeField] private ObjectContainer playerObjectContainer;
    [SerializeField] private ResourceViewItem viewItemPrefab;

    private Dictionary<ResourceName, ResourceViewItem> _items = new Dictionary<ResourceName, ResourceViewItem>();

    private void Awake()
    {
        foreach (var keyValuePair in DataRepository.ResourceModels)
        {
            var resourceName = keyValuePair.Key;
            var newItem = Instantiate(viewItemPrefab, transform);
            newItem.Init(resourceName.ToString());
            _items.Add(keyValuePair.Key, newItem);
        }
    }

    private void OnEnable()
    {
        playerObjectContainer.OnObjectsChanged += OnItemsChanged;
    }

    private void OnDisable()
    {
        playerObjectContainer.OnObjectsChanged -= OnItemsChanged;
    }

    private void OnItemsChanged()
    {
        foreach (var resourceViewItem in _items)
        {
            resourceViewItem.Value.Amount = 0;
        }

        foreach (var keyValuePair in playerObjectContainer.Objects)
        {
            if (_items.TryGetValue(keyValuePair.Value.ResourceName, out var resourceViewItem))
            {
                resourceViewItem.Amount++;
            }
        }
    }
    
}
