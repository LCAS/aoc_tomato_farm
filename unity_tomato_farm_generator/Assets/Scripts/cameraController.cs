using UnityEngine;

public class cameraController : MonoBehaviour
{
    private float dragSpeed = 10.0f;
    private float rotateSpeed = 10.0f;
    private float scrollSpeed = 200.0f;
    private Vector3 dragOrigin;

    public Vector2 sensitivity;
    public Transform cameraTransform;

    private bool isDragging = false;
    private bool isRotating = false;

    void Move()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButtonUp(2))
        {
            isDragging = false;
            return;
        }

        if (isDragging)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);
            transform.Translate(move, Camera.main.transform);
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            if (mouseX != 0)
            {
                cameraTransform.Rotate(0, -mouseX * sensitivity.x * rotateSpeed, 0);
            }

            if (mouseY != 0)
            {
                cameraTransform.Rotate(-mouseY * sensitivity.y * rotateSpeed, 0, 0);
            }
        }
    }

    void Scroll()
    {
        float scrollDelta = -Input.GetAxis("Mouse ScrollWheel");
        Vector3 scrollDirection = transform.forward;
        transform.Translate(-scrollDirection * scrollDelta * scrollSpeed * Time.deltaTime, Camera.main.transform);
    }

    void Update()
    {
        Move();
        Rotate();
        Scroll();
    }
}
