using UnityEngine.InputSystem;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    [SerializeField] private Camera cameraMain;

    private Vector2 mouseInput;
    private Cursor cursor;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseInput = Mouse.current.position.ReadValue();
        transform.position = mouseInput;
    }
}