using Photon.Pun;
using Photon.Realtime;
using VContainer;

public class PlayerOptionsSynchronizer : MonoBehaviourPunCallbacks
{
    [Inject] private PlayersOptions _playersOptions;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        _playersOptions.PlayerId.Value = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UnityEngine.Debug.Log($"Игрок {newPlayer.NickName} вошёл в лобби");
        _playersOptions.OpponentName.Value = newPlayer.NickName;
        _playersOptions.OpponentId.Value = newPlayer.ActorNumber.ToString();

        photonView.RPC(nameof(UpdateOpponentName), RpcTarget.Others, _playersOptions.PlayerName.Value);
        photonView.RPC(nameof(UpdateOpponentId), RpcTarget.Others, _playersOptions.PlayerId.Value);
    }

    [PunRPC]
    private void UpdateOpponentName(string name)
    {
        _playersOptions.OpponentName.Value = name;
    }

    [PunRPC]
    private void UpdateOpponentId(string name)
    {
        _playersOptions.OpponentId.Value = name;
    }
}
