using System;
using TMPro;
using UnityEngine;
using VContainer;

public class PlayerNameView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerName;

    [Inject] private PlayersOptions _playersOptions;

    private void Start()
    {
        _playerName.onEndEdit.AddListener(OnEndEdit);
        (_playerName.placeholder as TMP_Text).SetText(_playersOptions.PlayerName.Value);
    }

    private void OnDestroy()
    {
        _playerName.onEndEdit.RemoveAllListeners();
    }

    private void OnEndEdit(string value)
    {
        string playerName = string.IsNullOrWhiteSpace(_playerName.text)
                            ? PlayersOptions.GetRandomName()
                            : _playerName.text;
        _playersOptions.PlayerName.Value = playerName;
    }
}

