using Features.Inventory;
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
        
        private readonly InventoryView _view;
        private readonly IItemsRepository _repository;
        private readonly InventoryController _controller;

        public InventoryContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _view = LoadInventoryView(placeForUi);
            _repository = CreateInventoryRepository();
            _controller = new InventoryController(_view, _repository, profilePlayer.Inventory);
        }
        public override void Dispose()
        {
            _repository.Dispose();
            _controller.Dispose();
            Object.Destroy(_view.gameObject);
        }
        
        private ItemsRepository CreateInventoryRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            return repository;
        }

        private InventoryView LoadInventoryView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            return objectView.GetComponent<InventoryView>();
        }
    }
}