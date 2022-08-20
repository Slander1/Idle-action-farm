using Data.Configs;
using UnityEngine;
using Utils;

namespace Gameplay.Building
{
    public abstract class BuildingBehaviour : TimerBehaviour 
    {
        public abstract void TryToDeliverResources(ObjectContainer objectContainer, int maxObjectsCount);
    }
    public abstract class BuildingBehaviour<T> : BuildingBehaviour where T : PlantationData
    {
        [SerializeField] private int _buildingId;
        private T _data;
        protected T Data => _data ??= DataRepository.BuildingsData[_buildingId] as T;
    }
}