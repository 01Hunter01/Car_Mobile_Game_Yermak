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
            IShedView view = LoadView(placeForUi);
            IUpgradeHandlersRepository upgradeHandlersRepository = CreateRepository();
            
            ShedController controller = new ShedController(placeForUi, view, upgradeHandlersRepository, profilePlayer);
            AddController(controller);
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