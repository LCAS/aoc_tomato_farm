using UnityEngine;

public class cameraController : MonoBehaviour
{
    private float dragSpeed = 10.0f; // Drag speed
    private float rotateSpeed = 10.0f; // Rotate speed
    private float scrollSpeed = 200.0f; // Scroll speed
    private Vector3 dragOrigin; // Drag origin

    public Vector2 sensitivity; // Camera sensitivity
    public Transform cameraTransform; // Camera transform

    private bool isDragging = false; // Dragging indicator
    private bool isRotating = false; // Rotating indicator

    void Move()
    {
        // Start dragging when the middle mouse button is pressed
        if (Input.GetMouseButtonDown(2))
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;
            return;
        }

        // Stop dragging when the middle mouse button is released
        if (Input.GetMouseButtonUp(2))
        {
            isDragging = false;
            return;
        }

        // Perform dragging while the middle mouse button is held down
        if (isDragging)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);
            transform.Translate(move, Camera.main.transform);
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X"); // Mouse movement in the X axis
        float mouseY = Input.GetAxis("Mouse Y"); // Mouse movement in the Y axis

        // Start rotating when the right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }

        // Stop rotating when the right mouse button is released
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        // Perform rotation while the right mouse button is held down
        if (isRotating)
        {
            if (mouseX != 0)
            {
                cameraTransform.Rotate(0, mouseX * sensitivity.x * rotateSpeed, 0);
            }

            if (mouseY != 0)
            {
                cameraTransform.Rotate(-mouseY * sensitivity.y * rotateSpeed, 0, 0);
            }
        }
    }

    void Scroll()
    {
        float scrollDelta = -Input.GetAxis("Mouse ScrollWheel"); // Change in the mouse scroll wheel
        Vector3 scrollDirection = transform.forward; // Scroll direction
        transform.Translate(-scrollDirection * scrollDelta * scrollSpeed * Time.deltaTime, Camera.main.transform);
    }

    void Update()
    {
        Move(); // Call the move function
        Rotate(); // Call the rotate function
        Scroll(); // Call the scroll function
    }
}
