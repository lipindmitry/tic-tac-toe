using Photon.Pun;
using System;
using UniRx;
using VContainer;

public class MoveSynchronizer : MonoBehaviourPunCallbacks
{
    [Inject] private Game _game;
    [Inject] private PlayersOptions _playerOptions;
    [Inject] private IGameBoard _gameBoard;
    
    private IDisposable _subscription;

    private void Start()
    {
        _subscription = _gameBoard.LastMove
                                  .Skip(1)
                                  .Where(x => _game.IsMyMark(x.ContentType.Value))
                                  .Subscribe(SendMove);
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }

    private void SendMove(IBoardCell boardCell)
    {
        photonView.RPC(nameof(TransferOpponentMove), RpcTarget.Others, boardCell.Row, boardCell.Column);
    }

    [PunRPC]
    private void TransferOpponentMove(int row, int cell)
    {
        _game.MakeMove(row, cell, _playerOptions.OpponentId.Value);
    }
}

