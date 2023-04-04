using Photon.Pun;
using TMPro;

public class ExitPunCallback : MonoBehaviourPunCallbacks
{
    public void LeaveButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}