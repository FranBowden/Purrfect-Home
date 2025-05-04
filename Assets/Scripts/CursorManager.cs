using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D standardCursor;
    [SerializeField] private Texture2D PressedCursor;

    private Vector2 cursorHotspot;
    private void Start()
    {
        cursorHotspot = new Vector2(PressedCursor.width / 2, PressedCursor.height / 2);
        Cursor.SetCursor(PressedCursor, cursorHotspot, CursorMode.Auto);
    }

}
