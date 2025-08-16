using Photon.Pun;
using UnityEngine;
using VContainer;

public class MainMenuReturner : MonoBehaviour
{
    [Inject] private UIMenu _menu;

    public void ReturnToMainMenu()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();

        _menu.Switch(UiTabType.Main);
    }
}

