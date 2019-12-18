using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField] private TMP_Text textRoomName = null;
    [SerializeField] private TMP_Text textMapName = null;
    [SerializeField] private TMP_Text textPlayerCount = null;
    [SerializeField] private TMP_Text textRoomStatus = null;
    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        textRoomName.text = roomInfo.Name;
        textMapName.text = "Summoner rift";
        textPlayerCount.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
        textRoomStatus.text = "WAITING";
        if (roomInfo.PlayerCount == roomInfo.MaxPlayers)
        {
            textPlayerCount.color = Color.red;
            textRoomStatus.text = "FULL";
            textRoomStatus.color = Color.red;
        }
        if (!roomInfo.IsOpen)
        {
            textRoomStatus.text = "CLOSED";
            textRoomStatus.color = Color.red;
        }
    }
}