using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Transform buildingPrefab;
    [SerializeField] private Transform friendlyUnit;
    private GameObject theBuildingThing;

    private bool building;
    private bool barackIsBuild;
    private GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        building = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Fade");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnUnits();
        }
    }

    IEnumerator Fade()
    {
        Instantiate(buildingPrefab, new Vector3(20, 13), Quaternion.identity);
        theBuildingThing = GameObject.Find("Building(Clone)");
        if (building == true)
        {
            print("me live");
            print(theBuildingThing.name);
            theBuildingThing.transform.GetChild(0).gameObject.SetActive(true);
            theBuildingThing.transform.GetChild(1).gameObject.SetActive(false);
            theBuildingThing.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(5f);
        }

        if (building == true)
        {
            theBuildingThing.transform.GetChild(0).gameObject.SetActive(false);
            theBuildingThing.transform.GetChild(1).gameObject.SetActive(true);
            theBuildingThing.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(5f);
            print("I do my thing");
        }

        if (building == true)
        {
            theBuildingThing.transform.GetChild(0).gameObject.SetActive(false);
            theBuildingThing.transform.GetChild(1).gameObject.SetActive(false);
            theBuildingThing.transform.GetChild(2).gameObject.SetActive(true);
            barackIsBuild = true;
            yield return new WaitForSeconds(5f);
            print("I have done the thing");
        }
    }


    private void SpawnUnits()
    {
        if (barackIsBuild == true)
        {
            spawnPoint = GameObject.Find("EGO SpawnPoint");
            print("This works");
            Instantiate(friendlyUnit, spawnPoint.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
            
    }
}
