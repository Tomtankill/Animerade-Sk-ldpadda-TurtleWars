using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BuildBuildings : MonoBehaviour
{
    // Variables

    public GameObject commander;

    public GameObject townHallPrefab;
    public GameObject barracksPrefab;
    public GameObject armoryPrefab;
    public GameObject towerPrefab;

    public GameObject win;
    public GameObject lose;

    public List<GameObject> masterBuildingList = new List<GameObject>();
    public List<GameObject> townHallList = new List<GameObject>();
    public List<GameObject> barracksList = new List<GameObject>();
    public List<GameObject> armoryList = new List<GameObject>();
    public List<GameObject> towerList = new List<GameObject>();

    public List<GameObject> masterBuildingListAI = new List<GameObject>();
    public List<GameObject> townHallListAI = new List<GameObject>();
    public List<GameObject> barracksListAI = new List<GameObject>();
    public List<GameObject> armoryListAI = new List<GameObject>();
    public List<GameObject> towerListAI = new List<GameObject>();


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
    // Audio Sound Effect
    private AudioSource Sound;

    /// <summary>
    /// Upgrades for Armory
    /// </summary>
    public bool healthupgrade = false;
    public int healthUpgradeAmount = 200;
    // resources
    public float r1 = 0;
    public float r2 = 0;
    private int cost;
    private float cost2;
    public TextMeshProUGUI Resource1;
    public TextMeshProUGUI Resource2;


    // Start is called before the first frame update
    void Start()
    {
        //Sound Effect
        Sound = GetComponent<AudioSource>();
        //Resource1 = GetComponent<TextMeshProUGUI>();
        //Resource2 = GetComponent<TextMeshProUGUI>();
        ghost = Instantiate(ghost, Vector3.down * 300, Quaternion.identity);
        Resource1 = GameObject.Find("R1_text").GetComponent<TextMeshProUGUI>();
        Resource2 = GameObject.Find("R2_text").GetComponent<TextMeshProUGUI>();
    }

    public void TownHall()
    {
        if (GetComponent<TurnTimer>().IsMyTurn() == true)
        {
            NameBuilding(townHallPrefab, townhallCount, "Townhall", 5, 0);
            townhallCount++;
        }
    }

    public void Barracks()
    {
        if (GetComponent<TurnTimer>().IsMyTurn() == true)
        {
            NameBuilding(barracksPrefab, barracksCount, "Barracks", 10, 0);
            barracksCount++;
        }
    }

    public void Armory()
    {
        if (GetComponent<TurnTimer>().IsMyTurn() == true)
        {
            NameBuilding(armoryPrefab, armoryCount, "Armory", 15, 5);
            armoryCount++;         
        }
    }

    public void Tower()
    {
        if (GetComponent<TurnTimer>().IsMyTurn() == true)
        {
            NameBuilding(towerPrefab, towerCount, "Tower", 5, 1);
            towerCount++;         
        }
    }


    public void NameBuilding(GameObject setBuilding, int setCount, string setName, int setCost, int setCost2)
    {
        building = setBuilding;
        count = setCount;
        name = setName;
        cost = setCost;
        cost2 = setCost2;

        buildMode = true;
    }
    void Update()
    {
        // displays resources to the player
        Resource1.text = r1.ToString();
        Resource2.text = r2.ToString();

        if (buildMode)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    ghost.transform.position = hit.point;
                    ghost.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                    if (Input.GetMouseButtonDown(0))
                    {
                        Sound.Play();
                        if (r1 >= cost)
                        {
                            if (r2 >= cost2)
                            {

                                worldMousePos = hit.point;
                                GameObject go = Instantiate(building, worldMousePos, Quaternion.identity);
                                go.name = name + count.ToString();

                                r1 -= cost;
                                r2 -= cost2;
                            }
                        }
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

    public void PlayerWins()
    {
        win.SetActive(true);
    }

    public void AIWins()
    {
        lose.SetActive(true);
    }
}