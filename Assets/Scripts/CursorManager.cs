using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public Texture2D mainCursor;
    public Texture2D handCursor;

    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();
        ChangeCursor(mainCursor);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    private void ChangeCursor(Texture2D cursor)
    {
        Vector2 hotspot = new Vector2(cursor.width/2 , cursor.height/2);
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }

    private void Start()
    {
        controls.CursorControls.Click.started += _ => StartedClick();
        controls.CursorControls.Click.performed += _ => EndedClick();
    }

    private void StartedClick()
    {
        ChangeCursor(handCursor);
    }
    
    private void EndedClick()
    {
        ChangeCursor(mainCursor);

    }


}
