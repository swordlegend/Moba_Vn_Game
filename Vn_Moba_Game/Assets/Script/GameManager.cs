using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] redSpawnPos;
    [SerializeField]
    private GameObject[] blueSpawnPos;
    private int state = 0;
    private List<GameObject> listTeamRed = new List<GameObject>();
    private List<GameObject> listTeamBlue = new List<GameObject>();
    private string _labelCountPlayerWaiting;
    enum Team
    {
        Red, Blue
    }

    enum Champion
    {
        Riven,
        Yasuo
    }

    private void Start()
    {
        _labelCountPlayerWaiting = "Số người đang chờ: " + (listTeamRed.Count + listTeamBlue.Count);
    }

    void Connect()
    {
        PhotonNetwork.ConnectToBestCloudServer("1.0");
        OnJoinedLobby();
    }

    
    
    void OnJoinedLobby()
    {
        
        state = 1;
    }

    void OnJoinRandomRoomFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }
    
    void OnJoinedRoom()
    {
        state = 2;
    }

    
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 60, 200, 30), _labelCountPlayerWaiting);
        
        switch (state)
        {
         case 0:
             // STARTING SCEEN
             if (GUI.Button(new Rect(10, 10, 100, 30), "Connect"))
             {
                 Connect();
                 Debug.Log("click connect!");
             }
             break;
         case 1 :
             // CONNECT TO SERVER
             GUI.Label(new Rect(10, 40, 100, 30), "Connected");
             if (GUI.Button(new Rect(10, 10, 100, 30), "Search"))
             {
                 PhotonNetwork.JoinRandomRoom();
             }
             break;
         case 2:
             // CHAMPION SELECT
             GUI.Label(new Rect(10, 40, 100, 30), "Select Your Champion");
             if (GUI.Button(new Rect(10, 10, 100, 30), Champion.Riven.ToString()))
             {
                // GET CHAMPION PREFAB
                 var prefab = AssetDatabase.LoadAssetAtPath("Assets/SpawnPlayer/" + Champion.Riven.ToString() + ".prefab", typeof(GameObject));
                 var champion = (GameObject) Instantiate(prefab);
                 AddTeams(Team.Blue, champion);
             }
             break;
         case 3:
             // IN GAME
             
             break;
        }
    }

    void AddTeams(Team team, GameObject champion)
    {
        if (listTeamBlue.Count >= 5)
        {
            listTeamRed.Add(gameObject);
        }
        else
        {
            listTeamBlue.Add(gameObject);
        }
        Debug.Log("Add to team!");
    }

    void Spawn(GameObject[] red, GameObject[] blue)
    {
        
    }

    private void Update()
    {
        
    }
}
