using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField CreateInput;
    public Transform Content;
    public ListItem ItemPrefab;

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4
        };

        PhotonNetwork.CreateRoom(CreateInput.text, roomOptions, TypedLobby.Default);
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
            ListItem listItem = Instantiate(ItemPrefab, Content);
            
            if (listItem != null)
                listItem.SetInfo(info);
            
        }
    }
}
