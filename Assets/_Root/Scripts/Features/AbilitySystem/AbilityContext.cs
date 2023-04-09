using System.Collections.Generic;
using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem
{
    internal class AbilityContext : BaseContext
    {
        private readonly ResourcePath _abilitiesViewPath = new ("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _abilitiesDataSourcePath = new ("Configs/Ability/AbilityItemConfigDataSource");
        
        private readonly AbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly AbilitiesController _abilitiesController;

        public AbilityContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            _view = LoadView(placeForUi);
            var abilityItemConfigs = LoadItemConfigs();
            _repository = CreateRepository(abilityItemConfigs);
            
            _abilitiesController = new AbilitiesController(_view, _repository, abilityItemConfigs, abilityActivator);
        }

        public override void Dispose()
        {
            _repository.Dispose();
            _abilitiesController.Dispose();
            Object.Destroy(_view.gameObject);
        }

        private AbilityItemConfig[] LoadItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_abilitiesDataSourcePath);

        private AbilitiesRepository CreateRepository(IEnumerable<AbilityItemConfig> abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            return repository;
        }

        private AbilitiesView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_abilitiesViewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            return objectView.GetComponent<AbilitiesView>();
        }
    }
}