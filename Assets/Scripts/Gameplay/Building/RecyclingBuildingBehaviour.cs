using System.Collections.Generic;
using System.Linq;
using Data.Configs;
using UnityEngine;

namespace Gameplay.Building
{
    public class RecyclingBuildingBehaviour: BuildingBehaviour<ProductionPlantationData>
    {
        [SerializeField] private List<ObjectContainer> requiredResourceStorageList;
        [SerializeField] private ObjectContainer productionResourceStorage;
        [SerializeField] private Transform produceTransform;
        protected override float TimerCooldown => Data.produceCooldown;
        protected override void OnTimer()
        {
            var isAllRequiredObjectContains = requiredResourceStorageList.All(storage => storage.Objects.Count > 0);
            var productionObjectsCount = productionResourceStorage.Objects.Count;
            if (isAllRequiredObjectContains && productionObjectsCount < Data.productionData.maxResourceCount)
            {
                RecycleResource();
            }
        }

        private void RecycleResource()
        {
            var finishedMoveObjectsCount = 0;
            void OnObjectMoveEnd(MovableObject movableObject)
            {
                Destroy(movableObject.gameObject);
                finishedMoveObjectsCount++;
                if (finishedMoveObjectsCount >= requiredResourceStorageList.Count)
                {
                    Produce();
                }
            }

            if (requiredResourceStorageList == null || requiredResourceStorageList.Count == 0)
            {
                Produce();
            }
            else
            {
                foreach (var objectContainer in requiredResourceStorageList)
                {
                    var requiredObject = objectContainer.RemoveLastObject();
                    requiredObject.SetLocalPosition(produceTransform.localPosition, produceTransform, OnObjectMoveEnd);
                }
            }
        }

        private void Produce()
        {
                
            if(!DataRepository.ResourceModels.TryGetValue(Data.productionData.ResourceName, out var model))
                return;
            
            var newResourceModel = Instantiate(model);
            newResourceModel.transform.SetParent(produceTransform);
            newResourceModel.transform.position = produceTransform.position;
            newResourceModel.Init(Data.productionData.ResourceName);
            productionResourceStorage.AddObject(newResourceModel);
        }

        public override void TryToDeliverResources(ObjectContainer objectContainer, int maxObjectsCount)
        {
            for (var i = 0; i < requiredResourceStorageList.Count; i++)
            {
                var container = requiredResourceStorageList[i];
                var data = Data.requiredList[i];
                var requiredObjectsCount = container.Objects.Count;
                if (requiredObjectsCount >= data.maxResourceCount) continue;

                var reverseInventoryCollection = objectContainer.Objects.Reverse();
                foreach (var characterResource in reverseInventoryCollection)
                {
                    if (characterResource.Value.ResourceName != data.ResourceName) continue;

                    objectContainer.RemoveObject(characterResource.Key);
                    container.AddObject(characterResource.Value);
                    return;
                }
            }

            var freeSlots = maxObjectsCount - objectContainer.Objects.Count;
            if (freeSlots == 0 || productionResourceStorage.Objects.Count == 0)
                return;

            var produceObject = productionResourceStorage.RemoveLastObject();
            objectContainer.AddObject(produceObject);

        }
    }
}