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
    // Ensures that the 4 cameras heights don't lock the player into the one spot
    private int transition;
    // Sets initial click point
    private Vector3 dragOrigin;
    // Sets the relative on how far the map moves vs cursor movement
    public float dragSpeed;

    // Sets bool that disables moveCamera whilst dragCamera is active
    private bool moveDisable = false;
    // Sets bool that disables dragCamera whilst moveCamera is active
    private bool dragDisable = false;
    // Sets Bool that disables jump transitions
    private bool jump = true;

    // Start is called before the first frame update
    void Start()
    {
        panDetect = Screen.width * 0.02f;
        transition = 1;
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

        if (zoomY < 40)
        {
            zoomY = 40;
        }
        else if (zoomY > 994)
        {
            zoomY = 994;
        }


        if (jump == true)
        {

            if (zoomY >= 130 && transition == 1)
            {
                zoomY = 250;
                transition = 2;
            }

            else if (zoomY >= 300 && transition == 2)
            {
                zoomY = 450;
                transition = 3;
            }

            else if (zoomY >= 500 && transition == 3)
            {
                zoomY = 750;
                transition = 4;
            }

            else if (zoomY >= 800 && transition == 4)
            { 
                zoomY = 994;
                transition = 5;
            }
            ////////////////////////////////////////////////////////////////////////////////////////
            else if (zoomY <= 950 && transition == 5)
            {
                zoomY = 750;
                transition = 4;
            }

            else if (zoomY <= 700 && transition == 4)
            {
                zoomY = 450;
                transition = 3;
            }

            else if (zoomY <= 400 && transition == 3)
            {
                zoomY = 250;
                transition = 2;
            }

            else if (zoomY <= 200 && transition == 2)
            {
                zoomY = 80;
                transition = 1;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) || xPos > 0 && xPos < panDetect) // MoveLeft
        {
            if (moveX > -30)
            {
                moveX -= panSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || xPos < Screen.width && xPos > Screen.width - panDetect) // MoveRight
        {
            if (moveX < 1030)
            {
                moveX += panSpeed;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || yPos < Screen.height && yPos > Screen.height - panDetect) // MoveUp
        {
            if (moveZ < 1020)
            {
                moveZ += panSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || yPos > 0 && yPos < panDetect) // MoveDown
        {
            if (moveZ > -20)
            {
                moveZ -= panSpeed;
            }
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
            //Debug.Log("Player started dragging");
        }
        if (Input.GetMouseButtonUp(2)) {
            moveDisable = false;
            //Debug.Log("Player stopped dragging");
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
