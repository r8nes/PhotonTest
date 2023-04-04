using Photon.Pun;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _lastMessage;
    [SerializeField] TMP_InputField _messageField;

    private PhotonView PhotonView;

    private void Start()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    public void SendButton() 
    {
        PhotonView.RPC("SendData", RpcTarget.AllBuffered, PhotonNetwork.NickName, _messageField.text);
    }

    [PunRPC]
    private void SendData(string nick, string message) 
    {
        _lastMessage.text = $"{nick} : {message}";
    }
} 
