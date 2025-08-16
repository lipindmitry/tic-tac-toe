using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class RematchSynchronizer : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _rematch;

    [Inject] private Game _game;

    private bool _opponentRequestedRematch;
    private bool _playerRequestedRematch;

    private void Start()
    {
        _game.Finished += OnGameFinished;
        _rematch.onClick.AddListener(RequestRematch);
    }

    private void OnDestroy()
    {
        _game.Finished -= OnGameFinished;
        _rematch.onClick.RemoveAllListeners();
    }

    private void RequestRematch()
    {
        _playerRequestedRematch = true;
        _rematch.interactable = false;

        photonView.RPC(nameof(OpponentRequestRematch), RpcTarget.Others);

        if (NeedRestart())
            RestartGame();
    }

    private void OnGameFinished(WinnerType winner)
    {
        _rematch.interactable = true;
        _opponentRequestedRematch = false;
        _playerRequestedRematch = false;
    }

    private bool NeedRestart()
    {
        return PhotonNetwork.IsMasterClient && _playerRequestedRematch && _opponentRequestedRematch;
    }

    [PunRPC]
    public void OpponentRequestRematch()
    {
        _opponentRequestedRematch = true;

        if (NeedRestart())
            RestartGame();
    }

    [PunRPC]
    public void RestartGame()
    {
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC(nameof(RestartGame), RpcTarget.Others);

        _game.Restart();
    }
}

