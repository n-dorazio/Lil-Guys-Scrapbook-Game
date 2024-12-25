using UnityEngine;

public class WindowManager : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 offset;

    void Start()
    {
        // Make window stay on top and be transparent
        Application.runInBackground = true;
        SetWindowTransparent();
    }

    void Update()
    {
        HandleWindowDrag();
    }

    void HandleWindowDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPosition;
        }
    }

    void SetWindowTransparent()
    {
        // Set window to be transparent and clickthrough except for UI elements
        #if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            // Windows-specific transparency code would go here
        #endif
    }
} 