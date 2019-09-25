using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material green;

    private MeshRenderer myRend;

    [HideInInspector] public bool currentlySelected = false;

    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        ClickMe();
    }

    public void ClickMe()
    {
        if(currentlySelected == false)
        {
            myRend.material = red;
        }
        else
        {
            myRend.material = green;
        }
        
    }
}
