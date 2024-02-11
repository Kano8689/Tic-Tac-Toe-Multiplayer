using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;

public class Connection_Manager : MonoBehaviourPunCallbacks
{
    public GameObject connectScreen;
    public InputField userName, roomName;
    static Player[] playerList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    
    public void onClickConnectionBtn()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        connectScreen.SetActive(true);
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Join Lobby");
    }
    public override void OnLeftLobby()
    {
        Debug.Log("Left Lobby");
    }

    public void onClickCreateRoomBtn()
    {
        PhotonNetwork.CreateRoom(roomName.text, new RoomOptions { MaxPlayers = 2, IsOpen = true });
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Create is Failed " + message);
    }

    public void onClickJoinRoomBtn()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        PhotonNetwork.NickName = userName.text;
        PhotonNetwork.LoadLevel("Play");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Cannot Join Room Because " + message);
    }
}
