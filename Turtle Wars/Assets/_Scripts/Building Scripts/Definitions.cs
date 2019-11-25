using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Definitions : MonoBehaviour
{
    public static GameObject commander;
    public static GameObject unitCamera;
    public static GameObject barracksBuilder;
    public static GameObject townhallBuilder;
    public static Button swordFish;
    public static Button flyingFish;
    public static Button crab;
    public static Button squid;

    public void Awake()
    {
        commander = GameObject.Find("Commander");
        unitCamera = GameObject.Find("Main Camera");
        barracksBuilder = GameObject.Find("Building Control Barracks");
        townhallBuilder = GameObject.Find("Building Control TownHall");
        swordFish = barracksBuilder.transform.Find("Building Swordfish Button").GetComponent<Button>();
        flyingFish = barracksBuilder.transform.Find("Building Flying Fish Button").GetComponent<Button>();
        squid = barracksBuilder.transform.Find("Building Squid Button").GetComponent<Button>();
        crab = townhallBuilder.transform.Find("Building Unit Button").GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
