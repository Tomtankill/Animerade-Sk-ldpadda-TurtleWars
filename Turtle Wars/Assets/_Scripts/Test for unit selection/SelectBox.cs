using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox : MonoBehaviour
{
    public RectTransform selectSquareImages;

    Vector3 startPos;
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        selectSquareImages.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // when left click is held down
        if (Input.GetMouseButtonDown(0))
        {
            // shoots out raycast from camera to mouseposition
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                startPos = hit.point;
            }
        }

        // when left click is release deative sqare selecter images
        if (Input.GetMouseButtonUp(0))
        {
            selectSquareImages.gameObject.SetActive(false);
        }

        // when left click is press
        if (Input.GetMouseButton(0))
        {
            // if images is deative activate it
            if (!selectSquareImages.gameObject.activeInHierarchy)
            {
                selectSquareImages.gameObject.SetActive(true);
            }

            // endPos is the same as mouseposition
            endPos = Input.mousePosition;

            // 
            Vector3 sqareStart = Camera.main.WorldToScreenPoint(startPos);
            sqareStart.z = 0f;

            Vector3 center = (sqareStart + endPos) / 2f;

            // setting the position of sqare into the center
            selectSquareImages.position = center;

            float sizeX = Mathf.Abs(sqareStart.x - endPos.x);
            float sizeY = Mathf.Abs(sqareStart.y - endPos.y);

            selectSquareImages.sizeDelta = new Vector2(sizeX, sizeY);
        }
    }
}
