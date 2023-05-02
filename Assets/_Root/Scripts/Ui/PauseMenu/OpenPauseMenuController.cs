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
        private readonly Pause _pause;

        public OpenPauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(OpenPauseMenu);

            _pause = new Pause();
            CreatePauseMenuController(placeForUi, profilePlayer, _pause);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            if (_pause.IsEnabled)
                _pause.Disable();
        }

        private OpenPauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<OpenPauseMenuView>();
        }

        private void OpenPauseMenu() => _pause.Enable();

        private PauseMenuController CreatePauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer, Pause pause)
        {
            var pauseMenuController = new PauseMenuController(placeForUi, profilePlayer, pause);
            AddController(pauseMenuController);

            return pauseMenuController;
           
        }

    }
}
