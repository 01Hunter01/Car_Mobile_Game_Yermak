using Profile;
using System;
using System.Collections.Generic;
using Features.Inventory;
using UnityEngine;
using Features.Shed.Upgrade;
using JetBrains.Annotations;

namespace Features.Shed
{
    internal interface IShedController
    {
        
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly IShedView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;
        
        public ShedController(
            [NotNull] IShedView view,
            [NotNull] ProfilePlayer profilePlayer,
            [NotNull] IUpgradeHandlersRepository upgradeHandlersRepository)
        {
            _view
                = view ?? throw new ArgumentNullException(nameof(view));
            
            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));
            
            _upgradeHandlersRepository
                = upgradeHandlersRepository ?? throw new ArgumentNullException(nameof(upgradeHandlersRepository));

            _view.Init(Apply, Back);
        }
        
        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentCar,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Current Speed: {_profilePlayer.CurrentCar.Speed}");
            Log($"Apply. Current JumpHeight: {_profilePlayer.CurrentCar.JumpHeight}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Current Speed: {_profilePlayer.CurrentCar.Speed}");
            Log($"Back. Current JumpHeight: {_profilePlayer.CurrentCar.JumpHeight}");
        }
        
        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
