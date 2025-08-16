using TMPro;
using UnityEngine;
using VContainer;

public class WinnerShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _message;

    [Inject] private UIMenu _menu;
    [Inject] private Game _game;

    [Inject]
    private void Initialize()
    {
        _game.Finished += OnGameFinished;
    }

    private void OnDestroy()
    {
        _game.Finished -= OnGameFinished;
    }

    private void OnGameFinished(WinnerType winner)
    {
        switch (winner)
        {
            case WinnerType.Player:
                _message.SetText("Вы победили!!!");
                break;
            case WinnerType.Opponent:
                _message.SetText("Вы проиграли...");
                break;
            case WinnerType.Draw:
                _message.SetText("Ничья");
                break;
            default:
                throw new System.Exception($"Неизвестный победитель {winner}");
        }

        _menu.Show(UiTabType.Winner);
    }
}

