using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
    [SerializeField] private Text statusText = null;
    private List<RoomListing> _listings = new List<RoomListing>();
    [SerializeField]
    private RoomListing room;
    public EventSystem eventSystem;
    PointerEventData _pointerEventData;
    public GraphicRaycaster raycaster;

    public GameObject panelRoomWaitPlayer;
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
                RoomListing listing = Instantiate(room, content);
                if (listing != null)
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
            new RoomOptions {
                MaxPlayers = MaxPlayersPerRoom, 
                IsOpen = true, 
                IsVisible = true,
                CleanupCacheOnLeave = true,
                PublishUserId = true
            },
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
        gameObject.transform.Find("Manager").gameObject.active = false;
        panelRoomWaitPlayer.active = true;
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
        statusText.text = "Room is full or closed !";
        StartCoroutine(ResetTextStatus(2));
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room name existed!");
        statusText.text = "Room name existed!";
        StartCoroutine(ResetTextStatus(2));
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master.");
        nameRoomInputField.interactable = true;
        PhotonNetwork.JoinLobby();
        statusText.text = "Connected To Master";
        StartCoroutine(ResetTextStatus(2));
    }

    IEnumerator ResetTextStatus(float time)
    {
        yield return new WaitForSeconds(time);
        statusText.text = "";
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _pointerEventData = new PointerEventData(eventSystem);
            _pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(_pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("ROOM_ITEM"))
                {
                    RoomListing listing = result.gameObject.GetComponent<RoomListing>();
                    if (listing != null)
                    {
                        JoinRoomByClick(listing.RoomInfo);
                    }
                    
                }
            }
        }
    }

    public void JoinRoomByClick(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
    }
}

