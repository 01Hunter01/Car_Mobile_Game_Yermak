using Profile;
using Tool;
using UnityEngine;

namespace Ui
{ 
    internal class OpenPauseMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/OpenPauseMenuView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly OpenPauseMenuView _view;


        public OpenPauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(OpenPauseMenu);
        }

        private OpenPauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<OpenPauseMenuView>();
        }

        private void OpenPauseMenu() => 
            _profilePlayer.CurrentState.Value = GameState.PauseMenu;

    }
}
