using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesController
    { }

    internal class AbilitiesController : BaseController
    {
        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _abilityActivator;


        public AbilitiesController(
            [NotNull] IAbilitiesView view,
            [NotNull] IAbilitiesRepository repository,
            [NotNull] IEnumerable<IAbilityItem> abilityItems,
            [NotNull] IAbilityActivator abilityActivator)
        {
            _view
                = view ?? throw new ArgumentNullException(nameof(view));

            _repository
                = repository ?? throw new ArgumentNullException(nameof(repository));
            
            _abilityActivator
                = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));

            _view.Display(abilityItems, OnAbilityViewClicked);
        }
        
        
        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}
