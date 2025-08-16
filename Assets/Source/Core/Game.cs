using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using UniRx;

public class Game
{
    public event Action Started;
    public event Action<WinnerType> Finished;

    public IObservable<bool> IsPlayerTurn => _isPlayerTurn;
    private ReactiveProperty<bool> _isPlayerTurn = new();

    private readonly GameBoard _gameBoard;

    private GameParameters _gameParameters;
    private string _currentPlayerId;
    private bool _finished;
    
    public Game(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public void Start(GameParameters gameParameters)
    {
        if (gameParameters.PlayerContentType == CellContentType.None
            || gameParameters.PlayerContentType == CellContentType.Empty
            || gameParameters.OpponentContentType == CellContentType.None
            || gameParameters.OpponentContentType == CellContentType.Empty)
            throw new ArgumentException($"Доступные метки для игроков только крестики и нолики.");

        if (gameParameters.PlayerContentType == gameParameters.OpponentContentType)
            throw new ArgumentException($"Метки одинаковые у двух игроков. У обоих {gameParameters.PlayerContentType}.");

        _gameBoard.Clear();
        _finished = false;
        _gameParameters = gameParameters;
        _currentPlayerId = _gameParameters.PlayerContentType == CellContentType.Cross ?
            _gameParameters.PlayerId : _gameParameters.OpponentId;
        _isPlayerTurn.Value = _currentPlayerId == _gameParameters.PlayerId;

        Started?.Invoke();
        UnityEngine.Debug.Log("Стартовали");
    }

    public void Restart()
    {
        var gameParameters = new GameParameters(
            _gameParameters.PlayerId,
            playerContentType: _gameParameters.OpponentContentType,
            _gameParameters.OpponentId,
            opponentContentType: _gameParameters.PlayerContentType);
        
        Start(gameParameters);
    }

    public bool MakeMove(int row, int column, string playerId)
    {
        UnityEngine.Debug.Log($"Ход {row} {column} {playerId}");
        if (_finished)
            return false;

        if (playerId != _currentPlayerId)
            return false;

        if (!_gameBoard.IsEmpty(row, column))
            return false;

        var contentType = GetCurrentPlayerContentType();

        _gameBoard.SetContent(row, column, contentType);

        if (_gameBoard.IsFullLine(out var cellContentType))
        {
            _finished = true;
            var winner = _isPlayerTurn.Value == true ? WinnerType.Player : WinnerType.Opponent;
            Finished?.Invoke(winner);
        }
        else if (!_gameBoard.HasEmpty())
        {
            _finished = true;
            Finished?.Invoke(WinnerType.Draw);
        }
        else
        {
            _currentPlayerId = GetNextPlayer();
            _isPlayerTurn.Value = _currentPlayerId == _gameParameters.PlayerId;
        }

        return true;
    }

    public bool IsMyMark(CellContentType cellContentType)
    {
        return cellContentType == _gameParameters.PlayerContentType;
    }

    private string GetNextPlayer()
    {
        if (_currentPlayerId == _gameParameters.PlayerId)
            return _gameParameters.OpponentId;
        else
            return _gameParameters.PlayerId;
    }

    private CellContentType GetCurrentPlayerContentType()
    {
        if (_currentPlayerId == _gameParameters.PlayerId)
            return _gameParameters.PlayerContentType;
        else if (_currentPlayerId == _gameParameters.OpponentId)
            return _gameParameters.OpponentContentType;
        else
            throw new Exception($"Неопознанный игрок с id {_currentPlayerId} пытается сделать ход.");
    }
}

public enum WinnerType
{
    None = 0,
    Player = 1,
    Opponent = 2,
    Draw = 3
}

