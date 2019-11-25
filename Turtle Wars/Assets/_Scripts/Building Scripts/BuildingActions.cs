using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActions : MonoBehaviour
{
    
    //public GameObject worker;
    public GameObject crab;
    public GameObject swordfish;
    public GameObject squid;
    public GameObject flyingFish;
    public Transform spawnpoint;
    public LayerMask terrainLayer;
    private GameObject commander;

    // Start is called before the first frame update
    void Start()
    {
        commander = GameObject.Find("commander");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Building Actions

    //public void Worker()
    //{
    //    GameObject go = Instantiate(worker, new Vector3 (spawnpoint.position.x, 5 ,spawnpoint.position.z), Quaternion.identity);
    //    Ray ray = new Ray (go.transform.position, Vector3.down);
    //    if (Physics.Raycast(ray, out RaycastHit hit, 1000000.0f, terrainLayer))
    //    {
    //        go.transform.position = hit.point + new Vector3(0, 1, 0);
    //    }
    //}

    public void Crab(Vector3 pos)
    {
        spawnpoint.position = pos;
        GameObject go = Instantiate(crab, new Vector3 (spawnpoint.position.x, 5 ,spawnpoint.position.z), Quaternion.identity);
        Ray ray = new Ray (go.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000000.0f, terrainLayer))
        {
            go.transform.position = hit.point + new Vector3(0, 1, 0);
        }
    }
    public void Swordfish(Vector3 pos)
    {
        spawnpoint.position = pos;
        GameObject go = Instantiate(swordfish, new Vector3 (spawnpoint.position.x, 5 ,spawnpoint.position.z), Quaternion.identity);
        Ray ray = new Ray (go.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000000.0f, terrainLayer))
        {
            go.transform.position = hit.point + new Vector3(0, 1, 0);
        }
    }
    public void Squid(Vector3 pos)
    {
        spawnpoint.position = pos;
        GameObject go = Instantiate(squid, new Vector3 (spawnpoint.position.x, 5 ,spawnpoint.position.z), Quaternion.identity);
        Ray ray = new Ray (go.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000000.0f, terrainLayer))
        {
            go.transform.position = hit.point + new Vector3(0, 1, 0);
        }
    }
    public void FlyingFish(Vector3 pos)
    {
        spawnpoint.position = pos;
        GameObject go = Instantiate(flyingFish, new Vector3 (spawnpoint.position.x, 5 ,spawnpoint.position.z), Quaternion.identity);
        Ray ray = new Ray (go.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000000.0f, terrainLayer))
        {
            go.transform.position = hit.point + new Vector3(0, 1, 0);
        }
    }



    // Town Hall Upgrades
    //public void HealthUpgrade()
    //{
    //    
    //    if (commander.GetComponent<BuildBuildings>().selected.GetComponent<SelfBuildingManager>().townHallControls == true)
    //    {
    //        healthupgrade = true;
    //        for (int i = 0; i < masterBuildingList.Count; i++)
    //        {
    //            masterBuildingList[i].GetComponent<SelfBuildingManager>().maxHealth += healthUpgradeAmount;
    //            masterBuildingList[i].GetComponent<SelfBuildingManager>().currentHealth += healthUpgradeAmount;
    //        }
    //    }
    //}

}
