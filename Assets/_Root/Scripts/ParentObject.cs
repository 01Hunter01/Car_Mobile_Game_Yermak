using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

internal class ParentObject: IDisposable
{
    private List<IDisposable> _disposables;
    private List<GameObject> _gameObjects;
    private bool _isDisposed;
    
    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;
        
        DisposeDisposables();
        DisposeGameObjects();

        OnDispose();
    }

    private void DisposeDisposables()
    {
        if (_disposables == null)
            return;

        foreach (IDisposable disposable in _disposables)
            disposable.Dispose();

        _disposables.Clear();
    }
    
    private void DisposeGameObjects()
    {
        if (_gameObjects == null)
            return;

        foreach (GameObject gameObject in _gameObjects)
            Object.Destroy(gameObject);

        _gameObjects.Clear();
    }

    protected virtual void OnDispose() { }

    protected void AddController(BaseController baseController) =>
        AddDisposable(baseController);

    protected void AddRepository(IRepository repository) =>
        AddDisposable(repository);

    protected void AddContext(BaseContext context) =>
        AddDisposable(context);
    
    protected void AddGameObject(GameObject gameObject)
    {
        _gameObjects ??= new List<GameObject>();
        _gameObjects.Add(gameObject);
    }

    private void AddDisposable(IDisposable disposable)
    {
        _disposables??= new List<IDisposable>();
        _disposables.Add(disposable);
        
    }
}
