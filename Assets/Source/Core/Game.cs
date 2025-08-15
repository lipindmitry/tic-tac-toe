using System;
using UniRx;

public class Game
{
    public event Action Started;
    public event Action<bool> Finished;

    public IObservable<bool> IsPlayerTurn => _isPlayerTurn;
    private ReactiveProperty<bool> _isPlayerTurn = new();

    private readonly GameBoard _gameBoard;

    private GameParameters _gameParameters;
    private int _currentPlayerNumber;
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

        _finished = false;
        _gameParameters = gameParameters;
        _currentPlayerNumber = _gameParameters.PlayerContentType == CellContentType.Cross ?
            _gameParameters.PlayerNumber : _gameParameters.OpponentNumber;
        _isPlayerTurn.Value = _currentPlayerNumber == _gameParameters.PlayerNumber;

        Started?.Invoke();
    }

    public void Restart()
    {
        var gameParameters = new GameParameters(
            _gameParameters.PlayerNumber,
            playerContentType: _gameParameters.OpponentContentType,
            _gameParameters.OpponentNumber,
            opponentContentType: _gameParameters.PlayerContentType);
        
        Start(gameParameters);
    }

    public bool MakeMove(int row, int column, int playerNumber)
    {
        if (playerNumber != _currentPlayerNumber)
            return false;

        if (!_gameBoard.IsEmpty(row, column))
            return false;

        var contentType = GetCurrentContent();

        _gameBoard.SetContent(row, column, contentType);

        if (_gameBoard.IsFullLine(out var cellContentType))
        {
            Finished?.Invoke(_isPlayerTurn.Value);
            _finished = true;
        }
        else
        {
            _currentPlayerNumber = GetNextPlayer();
            _isPlayerTurn.Value = _currentPlayerNumber == _gameParameters.PlayerNumber;
        }

        return true;
    }

    private int GetNextPlayer()
    {
        if (_currentPlayerNumber == _gameParameters.PlayerNumber)
            return _gameParameters.OpponentNumber;
        else
            return _gameParameters.PlayerNumber;
    }

    private CellContentType GetCurrentContent()
    {
        if (_currentPlayerNumber == _gameParameters.PlayerNumber)
            return _gameParameters.PlayerContentType;
        else if (_currentPlayerNumber == _gameParameters.OpponentNumber)
            return _gameParameters.OpponentContentType;
        else
            throw new Exception($"Неопознанный игрок номер {_currentPlayerNumber} пытается сделать ход.");
    }
}

