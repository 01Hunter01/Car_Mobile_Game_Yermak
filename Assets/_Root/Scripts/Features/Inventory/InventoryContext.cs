using Features.Inventory.Items;
using Profile;
using Tool;
using UnityEngine;

namespace Features.Inventory
{
    internal class InventoryContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new ("Prefabs/Inventory/InventoryView");
        private readonly ResourcePath _dataSourcePath = new ("Configs/Inventory/ItemConfigDataSource");

        public InventoryContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            IInventoryView view = LoadInventoryView(placeForUi);
            IItemsRepository repository = CreateInventoryRepository();
            InventoryController controller = new InventoryController(view, repository, profilePlayer.Inventory);
            AddController(controller);
        }
        
        private ItemsRepository CreateInventoryRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);
            return repository;
        }

        private InventoryView LoadInventoryView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);
            return objectView.GetComponent<InventoryView>();
        }
    }
}