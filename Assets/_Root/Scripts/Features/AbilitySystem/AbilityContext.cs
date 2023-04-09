using System.Collections.Generic;
using Features.AbilitySystem.Abilities;
using Tool;
using UnityEngine;

namespace Features.AbilitySystem
{
    internal class AbilityContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new ("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _dataSourcePath = new ("Configs/Ability/AbilityItemConfigDataSource");

        public AbilityContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            IAbilitiesView view = LoadView(placeForUi);
            IEnumerable<AbilityItemConfig> abilityItemConfigs = LoadItemConfigs();
            IAbilitiesRepository repository = CreateRepository(abilityItemConfigs);
            AbilitiesController controller = new AbilitiesController(view, repository, abilityItemConfigs, abilityActivator);
            AddController(controller);
        }
        
        private AbilityItemConfig[] LoadItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourcePath);

        private AbilitiesRepository CreateRepository(IEnumerable<AbilityItemConfig> abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);
            return repository;
        }

        private AbilitiesView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);
            return objectView.GetComponent<AbilitiesView>();
        }
    }
}