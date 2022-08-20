using System;
using System.Collections.Generic;
using Data.ResourceData;

namespace Data.Configs
{
    [Serializable]
    public abstract class PlantationData
    {
        
    }
    
    [Serializable]
    public class ProductionPlantationData : PlantationData
    {
        public float produceCooldown;

        public List<ProductionData> requiredList;
        public ProductionData productionData;
    }

    public class ProductionData
    {
        public ResourceName ResourceName;
        public int maxResourceCount;
    }
}