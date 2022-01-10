using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }


    private void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connected To Server...");
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server.");
        base.OnConnectedToMaster();
        RoomOptions rOptions = new RoomOptions();
        rOptions.MaxPlayers = 10;
        rOptions.IsVisible = true;
        rOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("Room 1", rOptions, TypedLobby.Default);
    }




    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }




    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

