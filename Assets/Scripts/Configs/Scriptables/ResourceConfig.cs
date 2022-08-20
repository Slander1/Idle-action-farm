using Data.ResourceData;
using Gameplay;
using UnityEngine;

namespace Configs.Scriptables
{
    [CreateAssetMenu(fileName = nameof(ResourceConfig), menuName = "Data/" + nameof(ResourceConfig))]
    public class ResourceConfig : ScriptableObject
    {
        public ResourceName resourceName;
        public MovableObject resourceModel;
        public int resourceCost;
    }
}