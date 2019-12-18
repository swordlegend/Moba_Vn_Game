using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;


public class RoomManager : MonoBehaviourPunCallbacks
{
    private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int MaxPlayersPerRoom = 2;
    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private TMP_InputField nameRoomInputField = null;
    [SerializeField] private Button btnCreateRoom = null;
    [SerializeField] private Button btnRefreshRoom = null;
    [SerializeField] private Button btnJoinRoom = null;
    [SerializeField] private GameObject scrollListRoom = null;
    [SerializeField] private Transform content = null;
    private const string PlayerPrefsNameKey = "PlayerName";
    [SerializeField] private TMP_Text statusText = null;
    private List<RoomListing> _listings = new List<RoomListing>();
    [SerializeField]
    private RoomListing roomListing;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ConnectServer();
    }

    private void Start() => SetUpInputField();
    
    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) return;
        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey); 
        nameInputField.text = defaultName;
    }

    public void SetPlayerName(string name)
    {
        btnRefreshRoom.interactable = !string.IsNullOrEmpty(name);
        btnJoinRoom.interactable = !string.IsNullOrEmpty(name);
    }
    
    public void SetRoomName(string name) => btnCreateRoom.interactable = !string.IsNullOrEmpty(name);

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;
        PhotonNetwork.NickName = playerName;
        PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
    }

    public void RefreshRoom()
    {
        Debug.Log(PhotonNetwork.CountOfRooms);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == roomInfo.Name);
                if (index != 1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                RoomListing listing = Instantiate(roomListing, content);
                if (listing)
                {
                    listing.SetRoomInfo(roomInfo);
                    _listings.Add(listing);
                }
            }
        }
    }
    
    public void CreateRoom()
    {
        if (isConnecting == false)
        {
            ConnectServer();
        }
        PhotonNetwork.CreateRoom(
            nameRoomInputField.text, 
            new RoomOptions {MaxPlayers = MaxPlayersPerRoom},
            TypedLobby.Default
        );
    }

    public void ConnectServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
            isConnecting = true;
        }
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log($"Client successfully joined a room with name {PhotonNetwork.CurrentRoom.Name}");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != MaxPlayersPerRoom)
        {
            Debug.Log("Waiting for an player...");
        }
        else
        {
            Debug.Log("Matching is ready to begin!");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log("Match is ready to begin");
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room is full or closed !");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room name existed!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master.");
        nameRoomInputField.interactable = true;
        PhotonNetwork.JoinLobby();
    }
}
