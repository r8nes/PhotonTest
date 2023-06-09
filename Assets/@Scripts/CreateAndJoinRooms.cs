using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField RoomName;
    public Transform Content;
    public ListItem ItemPrefab;

    private string NickName = "Player";

    List<RoomInfo> AllRoomInfo = new List<RoomInfo>();

    private string GetDefaultPlayerName()
    {
        return "Player" + Random.Range(0, 9999).ToString();
    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4
        };

        PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("Main");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Room {PhotonNetwork.CurrentRoom.Name} created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Room failed");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            for (int i = 0; i < AllRoomInfo.Count; i++)
            {
                if (AllRoomInfo[i].masterClientId == info.masterClientId) return;
            }

            ListItem listItem = Instantiate(ItemPrefab, Content);

            if (listItem != null)
            {
                listItem.SetInfo(info);
                AllRoomInfo.Add(info);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        if (NickName == "")
            PhotonNetwork.NickName = GetDefaultPlayerName();
        else
            PhotonNetwork.NickName = NickName;

        PhotonNetwork.LoadLevel("Main");
    }

    public void JoinRandomRoomButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }
}