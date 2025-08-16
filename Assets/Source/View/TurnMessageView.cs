using System;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

public class TurnMessageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _message;

    [Inject] private Game _game;
    
    private IDisposable _subscription;

    private void Start()
    {
        _subscription = _game.IsPlayerTurn.Subscribe(SetMessage);
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }

    private void SetMessage(bool isPlayerTurn)
    {
        if (isPlayerTurn)
            _message.SetText("Ваш ход");
        else
            _message.SetText("Ход противника");
    }
}

