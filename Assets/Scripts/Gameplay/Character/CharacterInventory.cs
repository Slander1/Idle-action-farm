using System;
using System.Linq;
using Gameplay.Building;
using UnityEngine;
using Utils;

namespace Gameplay.Character
{
    public class CharacterInventory : TimerBehaviour
    {
        [SerializeField] protected ObjectContainer objectContainer;
        [SerializeField] private BuildingsBehaviourRepository behaviourRepository;
        [SerializeField] private float distanceForInteract;
        [SerializeField] private float transferCooldown;
        protected override float TimerCooldown => transferCooldown;


        protected override void OnTimer()
        {
            var interactBuildings = behaviourRepository.buildings.Where(building =>
                (building.transform.position - transform.position).sqrMagnitude < distanceForInteract * distanceForInteract);

            foreach (var buildingBehaviour in interactBuildings)
            {
                buildingBehaviour.TryToDeliverResources(objectContainer, objectContainer.MaxObjectsCount);
            }
        }
    }
}