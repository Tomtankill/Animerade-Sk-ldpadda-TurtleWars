using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    // Adjustable variables for X and Z movement
    public float panSpeed;
    // Adjustable variable for Y movement
    public float scrollSpeed;
    // Defines the distance from the edge of the screen the cursor has to be
    private float panDetect;
    // Ensures that the 2 cameras don't lock the player into the one spot
    private bool transition = true;
    // Sets initial click point
    private Vector3 dragOrigin;
    // Sets the relative on how far the map moves vs cursor movement
    public float dragSpeed;

    // Sets bool that disables moveCamera whilst dragCamera is active
    private bool moveDisable = false;
    // Sets bool that disables dragCamera whilst moveCamera is active
    private bool dragDisable = false;

    // Start is called before the first frame update
    void Start()
    {
        panDetect = Screen.width * 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        // This method will be controlling the movement of the camera on the X, Y and Z axis
        if (moveDisable == false){
            MoveCamera();
        }
        if (dragDisable == false) {
            DragCamera();
        }

    }

    void MoveCamera()
    {
        // defining the Camera location from the X, Y, Z positions to set a baseline
        float moveX = Camera.main.transform.position.x;
        float zoomY = Camera.main.transform.position.y;
        float moveZ = Camera.main.transform.position.z;

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        zoomY += (-Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);

        if (zoomY >= 600 && transition == true) { // number here has to be bigger than the number given in second if
            zoomY = 1300; // number here must be bigger than the second conditional if
            transition = false;
        }
        
        if (zoomY <= 900 && transition == false) { // number here has to be smaller than the number given above
            zoomY = 500; // number here must be smaller than the first conditional if
            transition = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || xPos > 0 && xPos < panDetect)
        {
            moveX -= panSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || xPos < Screen.width && xPos > Screen.width - panDetect)
        {
            moveX += panSpeed;
        }

        if (Input.GetKey(KeyCode.UpArrow) || yPos < Screen.height && yPos > Screen.height - panDetect)
        {
            moveZ += panSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || yPos > 0 && yPos < panDetect)
        {
            moveZ -= panSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || xPos > 0 && xPos < panDetect || xPos < Screen.width && xPos > Screen.width - panDetect || yPos < Screen.height && yPos > Screen.height - panDetect || yPos > 0 && yPos < panDetect){
            dragDisable = true;
        }
        else {
            dragDisable = false;
            //Debug.Log("Player isn't doing anything");
        }


        Vector3 newPos = new Vector3(moveX, zoomY, moveZ);
        //Debug.Log("moveX: " + moveX + ", moveY: " + zoomY + ", moveZ: " + moveZ);
        Camera.main.transform.position = newPos;
    }

    void DragCamera() {
        // disables other movement
        if (Input.GetMouseButtonDown(2)) {
            moveDisable = true;
            Debug.Log("Player started dragging");
        }
        if (Input.GetMouseButtonUp(2)) {
            moveDisable = false;
            Debug.Log("Player stopped dragging");
        }
        // beginning of actual processing
        if (Input.GetMouseButtonDown(2)) {
            dragOrigin = Input.mousePosition;
            return;
        }
        if (!Input.GetMouseButton(2)) return;


        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        transform.Translate(-move, Space.World);
    }
}
