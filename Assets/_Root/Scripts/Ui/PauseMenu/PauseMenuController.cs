using System.Collections;
using System.Collections.Generic;
using Profile;
using Tool;
using UnityEngine;

namespace Ui
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/PauseMenuView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly PauseMenuView _view;


        public PauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(ContinueGame, OpenMainMenu);
        }

        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }

        private void ContinueGame() => 
            _profilePlayer.CurrentState.Value = GameState.Game;
        private void OpenMainMenu() => 
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
