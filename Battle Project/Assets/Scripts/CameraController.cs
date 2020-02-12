using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    [Header("Camera Settings:")]
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    [Header("Boundaries:")]
    public int MIN_X;
    public int MAX_X;
    public int MIN_Z;
    public int MAX_Z;
    public int MIN_ZOOM;
    public int MAX_ZOOM;

    Vector3 newPosition;
    Quaternion newRotation;
    Vector3 newZoom;

    // Variable for dragging
    Vector3 dragStartPosition;
    Vector3 dragCurrentPosition;
    Vector3 rotateStartPosition;
    Vector3 rotateCurrentPosition;

    private void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    private void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
        //CheckBoundaries();
    }

    void HandleMouseInput()
    {
        #region Dragging movement
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
        #endregion

        #region Dragging rotation
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));

        }
        #endregion
    }

    void HandleMovementInput()
    {
        #region Movement 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        #endregion

        #region Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        if(Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        #endregion

        #region Zoom
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            newZoom += zoomAmount;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            newZoom -= zoomAmount;
        }
        #endregion

        if (newPosition.x < MAX_X && newPosition.x > MIN_X && newPosition.z < MAX_Z && newPosition.z > MIN_Z)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        }
        else
        {
            newPosition = transform.position;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        if (newZoom.z < MAX_ZOOM && newZoom.z > MIN_ZOOM)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        }
        else
        {
            newZoom = cameraTransform.localPosition;
        }
    }
    
    void CheckBoundaries()
    {
        if(transform.position.x > 10)
        {
            transform.position = new Vector3(9, transform.position.y, transform.position.z);
        }
    }
}
