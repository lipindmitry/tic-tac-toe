using UnityEngine;
using VContainer;

public class UISwitcher : MonoBehaviour
{
    [Inject] private Game _game;
    [Inject] private UIMenu _menu;

    private void Start()
    {
        _game.Started += OnGameStarted;
    }

    private void OnGameStarted()
    {
        _menu.Switch(UiTabType.Game);
    }
}

