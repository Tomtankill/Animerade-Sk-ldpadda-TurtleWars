using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuildings : MonoBehaviour
{
    // Variables

    public GameObject commander;

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
    public bool healthupgrade = false;
    public int healthUpgradeAmount = 200;
    // resources
    public float r1 = 0;
    public float r2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        ghost = Instantiate(ghost, Vector3.down * 300, Quaternion.identity);
    }

    public void TownHall()
    {
        if (commander.GetComponent<TurnTimer>().p1Turn == true)
        {
            NameBuilding(townHallPrefab, townhallCount, "Townhall");
            townhallCount++;
        }
        else
        {
            Debug.Log("It's not your turn yet");
        }
    }

    public void Barracks()
    {
        if (commander.GetComponent<TurnTimer>().p1Turn == true)
        {
            NameBuilding(barracksPrefab, barracksCount, "Barracks");
            barracksCount++;
        }
        else
        {
            Debug.Log("It's not your turn yet");
        }
    }

    public void Armory()
    {
        if (commander.GetComponent<TurnTimer>().p1Turn == true)
        {
            NameBuilding(armoryPrefab, armoryCount, "Armory");
            armoryCount++;
        }
        else
        {
            Debug.Log("It's not your turn yet");
        }
    }

    public void Tower()
    {
        if (GetComponent<TurnTimer>().IsMyTurn() == true)
        {
            NameBuilding(towerPrefab, towerCount, "Tower");
            towerCount++;
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

                        ghost.transform.position = Vector3.down * 300;
                    }
                }
                else
                {
                    ghost.transform.position = Vector3.down * 300;
                }
            }
        }
    }

    public void Upgrades()
    {
        if (selected.GetComponent<SelfBuildingManager>().armoryControls == true)
        {

            healthupgrade = true;
            for (int i = 0; i < barracksList.Count; i++)
            {
                barracksList[i].GetComponent<SelfBuildingManager>().maxHealth += healthUpgradeAmount;
                barracksList[i].GetComponent<SelfBuildingManager>().currentHealth += healthUpgradeAmount;
            }
        }
    }

    public void PlayerWins()
    {
        Debug.Log("The Player that just lost the building loses");
    }
}