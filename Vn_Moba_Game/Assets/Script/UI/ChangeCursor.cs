using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [Header("Cursor Defaul"),SerializeField]
    private Texture2D texture2D;
    [SerializeField, Header("Cursor Click")]
    private Texture2D texture2D1;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private void Start()
    {
        Cursor.SetCursor(texture2D, hotSpot, cursorMode);
        Debug.Log("Cursor");
    }

    void Update()
    {
        var ML = Input.GetMouseButton(0);
        if (ML)
        {
            Cursor.SetCursor(texture2D1, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(texture2D, hotSpot, cursorMode);
        }
    }
}
