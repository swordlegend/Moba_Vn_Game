using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] redSpawnPos;
    [SerializeField]
    private GameObject[] blueSpawnPos;
    enum Team
    {
        Red, Blue
    }

    enum Champion
    {
        Riven,
        Yasuo
    }
}
