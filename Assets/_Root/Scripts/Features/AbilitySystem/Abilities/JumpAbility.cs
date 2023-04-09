using System;
using UnityEngine;
using JetBrains.Annotations;

namespace Features.AbilitySystem.Abilities
{
    internal class JumpAbility : IAbility
    {
        private readonly AbilityItemConfig _config;

        public JumpAbility([NotNull] AbilityItemConfig config)
        {
            _config = config ? config : throw new ArgumentNullException(nameof(config));
        }
        
        public void Apply(IAbilityActivator activator)
        {
            var car = activator.ViewGameObject.GetComponent<Rigidbody2D>();
            Vector3 jump = activator.ViewGameObject.transform.up * _config.Value;
            car.AddForce(jump, ForceMode2D.Force);
        }
    }
}