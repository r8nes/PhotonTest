using System.Collections;
using Photon.Pun;
using UnityEngine;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    public TMPro.TextMeshProUGUI Text;
    public Region region;

    private string NickName = "";

    //вынести регион за пределы
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region.ToString());
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        if (NickName == "")
            PhotonNetwork.NickName = GetDefaultPlayerName();
        else
            PhotonNetwork.NickName = NickName;
    }

    public override void OnJoinedLobby()
    {
        StartCoroutine(LoadLevelAsync());
    }

    private string GetDefaultPlayerName()
    {
        return "Player " + Random.Range(0, 9999).ToString();
    }

    private IEnumerator LoadLevelAsync()
    {
        PhotonNetwork.LoadLevel("Lobby");

        while (PhotonNetwork.LevelLoadingProgress < 1)
        {
            Text.text = "Loading: %" + (int)(PhotonNetwork.LevelLoadingProgress * 100);

            yield return new WaitForEndOfFrame();
        }
    }
}

public enum Region 
{
    ru = 0,
    eu = 1
}