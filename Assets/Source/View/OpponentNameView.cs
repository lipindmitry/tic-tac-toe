using System;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

public class OpponentNameView : MonoBehaviour
{
    [SerializeField] private TMP_Text _opponentName;

    [Inject] private PlayersOptions _playersOptions;
    
    private IDisposable _subscription;

    private void Start()
    {
        _subscription = _playersOptions.OpponentName.Subscribe(SetText);
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }

    private void SetText(string value)
    {
        _opponentName.SetText(value);
    }
}

