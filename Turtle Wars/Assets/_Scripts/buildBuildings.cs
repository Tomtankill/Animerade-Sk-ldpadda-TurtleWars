using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildBuildings : MonoBehaviour
{
    // Variables
    private GameObject building;

    public GameObject townHallPrefab;
    public GameObject barracksPrefab;
    public GameObject armoryPrefab;
    public GameObject towerPrefab;

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
    }

    public void Barracks() 
    {
        NameBuilding(barracksPrefab, barracksCount, "Barracks");
    }

    public void Armory()
    {
        NameBuilding(armoryPrefab, armoryCount, "Armory");
    }

    public void Tower()
    {
        NameBuilding(towerPrefab, towerCount, "Tower");
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
                        go.name = name + count;
                        count++;

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