﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutscenePlay : MonoBehaviour
{
    public GameObject timelineDisable;
    public GameObject timelineEnable;
    private bool hashappened;

    // Start is called before the first frame update
    void Start()
    {
        hashappened = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hashappened)
        {
            print("i'm trying");
            timelineEnable.SetActive(true);
            hashappened = false;
            //timelineDisable.SetActive(false);
        }

    }
}
