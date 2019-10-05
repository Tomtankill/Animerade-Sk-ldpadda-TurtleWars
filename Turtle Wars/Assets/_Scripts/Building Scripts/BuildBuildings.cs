using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuildings : MonoBehaviour
{
    // Variables

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

    public GameObject selected;
    private GameObject building;
    private new string name;
    bool buildMode;
    public GameObject ghost;

    /// <summary>
    /// Upgrades for Armory
    /// </summary>
    public int healthUpgradeAmount = 200;

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

    public void Upgrades()
    {
        if (selected.GetComponent<SelfBuildingManager>().controls == true)
        {
            for (int i = 0; i < barracksList.Count; i++)
            {
                barracksList[i].GetComponent<SelfBuildingManager>().maxHealth += healthUpgradeAmount;
                barracksList[i].GetComponent<SelfBuildingManager>().currentHealth += healthUpgradeAmount;
            }
        }
    }
}