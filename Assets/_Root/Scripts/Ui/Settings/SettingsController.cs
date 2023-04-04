using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class SettingsController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/SettingsMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsView _view;


        public SettingsController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(BackToMainMenu);
        }

        private SettingsView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsView>();
        }

        private void BackToMainMenu() => 
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
