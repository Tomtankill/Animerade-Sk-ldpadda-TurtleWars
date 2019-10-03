using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildBuildings : MonoBehaviour
{
    // Variables
    private GameObject building;

    public GameObject commander;

    [SerializeField] private int healthUpgradeAmount = 200;

    public GameObject townHallPrefab;
    public GameObject barracksPrefab;
    public GameObject armoryPrefab;
    public GameObject towerPrefab;
    

    public List<GameObject> masterBuildingList = new List<GameObject>();
    public List<GameObject> townHallList = new List<GameObject>();
    public List<GameObject> barracksList = new List<GameObject>();
    public List<GameObject> armoryList = new List<GameObject>();
    public List<GameObject> towerList = new List<GameObject>();

    

    Vector3 worldMousePos;

    private int townhallCount = 0;
    private int barracksCount = 0;  
    private int armoryCount = 0;
    private int towerCount = 0;
    private int count = 0;

    private new string name;

    bool buildMode;
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        ghost = Instantiate(ghost, Vector3.zero * 1000, Quaternion.identity);
    }

    public void TownHall()
    {
        NameBuilding(townHallPrefab, townhallCount, "Townhall");
        townhallCount++;
    }

    public void Barracks() 
    {
        NameBuilding(barracksPrefab, barracksCount, "Barracks");
        barracksCount++;
    }

    public void Armory()
    {
        NameBuilding(armoryPrefab, armoryCount, "Armory");
        armoryCount++;
    }

    public void Tower()
    {
        NameBuilding(towerPrefab, towerCount, "Tower");
        towerCount++;
    }


    //HEALTHUPGRADE GOES ON A DIFFERENT SCRIPT

    public void HealthUpgrade()
    {
        for (int i = 0; i < commander.GetComponent<buildBuildings>().barracksList.Count; i++)
        {
            barracksList[i].GetComponent<Hello>().maxHealth += healthUpgradeAmount;
            barracksList[i].GetComponent<Hello>().currentHealth += healthUpgradeAmount;
        }
        
    }

    
    public void NameBuilding(GameObject setBuilding, int setCount, string setName)
    {
        building = setBuilding;
        count = setCount;
        name = setName;

        buildMode = true;
    }
    void Update()
    {
        if (buildMode)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    ghost.transform.position = hit.point;

                    if (Input.GetMouseButtonDown(0))
                    {
                        worldMousePos = hit.point;
                        GameObject go = Instantiate(building, worldMousePos, Quaternion.identity);
                        go.name = name + count.ToString();

                        buildMode = false;

                        ghost.transform.position = Vector3.zero * 1000;
                    }
                }
                else
                {
                    ghost.transform.position = Vector3.one * 1000;
                }
            }
        }
    }
}