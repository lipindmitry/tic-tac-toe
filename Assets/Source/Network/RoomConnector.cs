using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class RoomConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private Button _joinRoom;
    [SerializeField] private TMP_InputField _roomNameInput;

    private RoomOptions _roomOptions;
    private RoomOptions _customRoomOptions;
    private IDisposable _subscription;

    [Inject] private PlayersOptions _playersOptions;
    [Inject] private UIMenu _menu;

    private bool _createdCustomRoom;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        _roomOptions = new RoomOptions()
        {
            MaxPlayers = 2,
            IsVisible = true,
            IsOpen = true,
            EmptyRoomTtl = 0,
            PublishUserId = true
        };
        _customRoomOptions = new RoomOptions()
        {
            MaxPlayers = 2,
            IsVisible = false,
            IsOpen = true,
            EmptyRoomTtl = 0,
            PublishUserId = true
        };
        _subscription = _playersOptions.PlayerName.Subscribe(SetNickName);
        _joinRoom.onClick.AddListener(JoinRoom);
    }

    private void OnDestroy()
    {
        _subscription.Dispose();
    }

    private void SetNickName(string value)
    {
        PhotonNetwork.LocalPlayer.NickName = value;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinLobby();
        _menu.Switch(UiTabType.Main);
    }

    public void QuickGame()
    {
        _createdCustomRoom = false;
        _menu.Switch(UiTabType.SearchingOpponent);
        PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: _roomOptions);
    }

    public void CreateRoom()
    {
        _roomName.SetText(string.Empty);
        _createdCustomRoom = true;
        _menu.Switch(UiTabType.Room);
        string roomName = ((int)(UnityEngine.Random.value * 1000 + 1000)).ToString();
        PhotonNetwork.CreateRoom(roomName, _customRoomOptions);
    }

    private void JoinRoom()
    {
        _joinRoom.interactable = false;
        PhotonNetwork.JoinRoom(_roomNameInput.text);
    }

    public void OpenJoinRoomTab()
    {
        _menu.Switch(UiTabType.JoinRoom);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _menu.Show(UiTabType.Messenger);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Не удалось войти: {message}");
        _joinRoom.interactable = true;

        if (returnCode == ErrorCode.GameFull)
            Debug.Log("Комната уже заполнена (2/2 игроков)");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        _createdCustomRoom = false;
        _joinRoom.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (_createdCustomRoom)
            _roomName.SetText(PhotonNetwork.CurrentRoom.Name);
    }
}
