using System.Collections.Generic;
using System.Linq;
using Configs.Scriptables;
using Data.Configs;
using Data.ResourceData;
using Gameplay;
using UnityEngine;

public static class DataRepository
{
    private static readonly List<PlantationData> _buildingsData;
    public static IReadOnlyList<PlantationData> BuildingsData => _buildingsData;
    
    private static readonly Dictionary<ResourceName, MovableObject> _resourceModels;
    public static IReadOnlyDictionary<ResourceName, MovableObject> ResourceModels => _resourceModels;

    static DataRepository()
    {
        _buildingsData = Resources.Load<PlantationConfig>("Buildings/PlantationConfig").plantationformData;
        _resourceModels = Resources.LoadAll<ResourceConfig>("ResourceItems")
            .ToDictionary(resource => resource.resourceName, resource => resource.resourceModel);
    }
}
