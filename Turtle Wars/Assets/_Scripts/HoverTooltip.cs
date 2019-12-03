using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTooltip : MonoBehaviour
{
    private bool mouseOver = false;
    public GameObject ToolTip;


    private void OnMouseEnter()
    {
        mouseOver = true;
        ToolTip.SetActive(true);
        print("Enter");
    }
    private void OnMouseExit()
    {
        mouseOver = false;
        ToolTip.SetActive(false);
        print("Exit");
    }


}
