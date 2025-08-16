using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameStarter : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _start;

    [Inject] private Game _game;
    [Inject] private PlayersOptions _playersOptions;
    [Inject] private UIMenu _menu;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            ShowStartPreview();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            ShowStartPreview();
    }

    private void ShowStartPreview()
    {
        _menu.Switch(UiTabType.StartPreview);
        _start.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    [PunRPC]
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC(nameof(StartGame), RpcTarget.Others);

        var gameParameters = new GameParameters(
            _playersOptions.PlayerId.Value,
            PhotonNetwork.IsMasterClient ? CellContentType.Cross : CellContentType.Zero,
            _playersOptions.OpponentId.Value,
            PhotonNetwork.IsMasterClient ? CellContentType.Zero : CellContentType.Cross);

        _game.Start(gameParameters);
    }


}

