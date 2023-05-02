using System;
using System.Collections;
using System.Collections.Generic;
using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/PauseMenuView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly PauseMenuView _view;
        private readonly Pause _pause;

        public PauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer, Pause pause)
        {
            _profilePlayer = profilePlayer;

            _pause = pause;
            Subscribe(pause);
            
            _view = LoadView(placeForUi);
            _view.Init(ContinueGame, OpenMainMenu);

            if (_pause.IsEnabled) _view.Show();
            else _view.Hide();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            Unsubscribe(_pause);
        }

        private void Subscribe(Pause pause)
        {
            pause.Enabled += OnPauseEnabled;
            pause.Disabled += OnPauseDisabled;
        }

        private void Unsubscribe(Pause pause)
        {
            pause.Enabled -= OnPauseEnabled;
            pause.Disabled -= OnPauseDisabled;
        }

        private void OnPauseEnabled() => _view.Show();
        private void OnPauseDisabled() => _view.Hide();

        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }

        private void ContinueGame() => _pause.Disable();
        private void OpenMainMenu() => _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
