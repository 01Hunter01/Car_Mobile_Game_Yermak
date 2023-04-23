using System;
using Features.Inventory;
using Features.Shed.Upgrade;
using Profile;
using Tool;
using UnityEngine;

namespace Features.Shed
{
    internal class ShedContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new ("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new ("Configs/Shed/UpgradeItemConfigDataSource");
        
        public ShedContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (profilePlayer == null)
                throw new ArgumentNullException(nameof(profilePlayer));

            CreateController(profilePlayer, placeForUi);
        }

        private ShedController CreateController(ProfilePlayer profilePlayer, Transform placeForUi)
        {
            InventoryContext inventoryContext = CreateInventoryContext(placeForUi, profilePlayer);
            UpgradeHandlersRepository shedRepository = CreateRepository();
            ShedView shedView = LoadView(placeForUi);
            
            ShedController shedController = new ShedController(shedView, profilePlayer, shedRepository);

            return shedController;
        }
        
        private InventoryContext CreateInventoryContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var inventoryContext = new InventoryContext(placeForUi, profilePlayer);
            AddContext(inventoryContext);
            return inventoryContext;
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

    }
}