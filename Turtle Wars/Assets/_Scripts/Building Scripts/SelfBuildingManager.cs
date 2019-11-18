using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SelfBuildingManager : MonoBehaviour
{
    // Letting the building know who the commander is
    public GameObject commander;
    public GameObject unitCamera;
    public GameObject AIcommander;
    public GameObject barracksBuilder;
    public int type;
    public bool AI;
    // bools
    public bool controls;
    [SerializeField]private bool buildingFinish;

    // render
    [SerializeField]private Renderer buildingRender;

    // Is the building selected?
    [HideInInspector] public bool currentlySelected = false;


    // This is where we will set the stats for all of our buildings.
    // The Stats will be changed in the switch statement
    // health values
    public float currentHealth;
    public float maxHealth;
    private float reg;
    // damage values
    public float dmg;
    // range values
    public float range;
    // cost
    public float cost;


    // Building Specific Control Enable
    public bool townHallControls;
    public bool barracksControls;
    public bool armoryControls;
    public bool towerControls;

    // checkpoints
    private float checkpointOne;
    private float checkpointTwo;
    private float checkpointThree;

    // materials
    public Material red;
    public Material yellow;
    public Material green;
    public Material purple;
    public Material blue;

    private void Awake()
    {
        AIcommander = GameObject.Find("MasterAI");
        if (tag == "Enemy")
        {
            AI = true;
        }
        else
        {
            AI = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // finds the commander and sets it to its appropriate variable
        commander = GameObject.Find("Commander");
        unitCamera = GameObject.Find("Main Camera");
        barracksBuilder = GameObject.Find("Building Control Barracks");
        //barracksBuilder.SetActive(false);
        // bools are false on start
        buildingFinish = false;
        controls = false;
        buildingRender = GetComponent<Renderer>();
        Camera.main.gameObject.GetComponent<Click>().selectableBuildings.Add(this);

        // setting maxHealth depending on what building it is
        if (gameObject.name.Contains("Townhall"))
        {
            type = 0;
        }
        else if (gameObject.name.Contains("Barracks"))
        {
            type = 1;
        }
        else if (gameObject.name.Contains("Armory"))
        {
            type = 2;
        }
        else if (gameObject.name.Contains("Tower"))
        {
            type = 3;
        }




        switch (type)
        {
            case 0: //Town Hall
                // Adding new object to list
                if (AI)
                {
                    commander.GetComponent<BuildBuildings>().townHallListAI.Add(gameObject);
                    commander.GetComponent<BuildBuildings>().masterBuildingListAI.Add(gameObject);
                }
                else
                {
                    commander.GetComponent<BuildBuildings>().townHallList.Add(gameObject);
                    commander.GetComponent<BuildBuildings>().masterBuildingList.Add(gameObject);
                }

                // Setting Stats
                maxHealth = 150;
                currentHealth = 1;
                reg = 2;
                dmg = 3;
                range = 3;

                BuildMe();

                break;
            case 1: // Barracks
                if (AI)
                {
                    commander.GetComponent<BuildBuildings>().barracksListAI.Add(gameObject);
                    commander.GetComponent<BuildBuildings>().masterBuildingListAI.Add(gameObject);
                }
                else
                {
                    commander.GetComponent<BuildBuildings>().barracksList.Add(gameObject);
                    commander.GetComponent<BuildBuildings>().masterBuildingList.Add(gameObject);
                }

                // Setting Stats
                maxHealth = 100;
                currentHealth = 1;
                reg = 2;

                BuildMe();

                break;
            case 2: //Armory
                if (AI)
                {
                    commander.GetComponent<BuildBuildings>().armoryListAI.Add(gameObject);
                    commander.GetComponent<BuildBuildings>().masterBuildingListAI.Add(gameObject);
                }
                else
                {
                    commander.GetComponent<BuildBuildings>().armoryList.Add(gameObject);
                    commander.GetComponent<BuildBuildings>().masterBuildingList.Add(gameObject);
                }

                // Setting Stats
                maxHealth = 100;
                currentHealth = 1;
                reg = 2;

                BuildMe();

                break;
            case 3: //Tower
                if (AI)
                {
                    commander.GetComponent<BuildBuildings>().towerListAI.Add(gameObject);
                    commander.GetComponent<BuildBuildings>().masterBuildingListAI.Add(gameObject);
                }
                else
                {
                    commander.GetComponent<BuildBuildings>().towerList.Add(gameObject);
                    commander.GetComponent<BuildBuildings>().masterBuildingList.Add(gameObject);
                }
                // Setting Stats
                maxHealth = 75;
                currentHealth = 1;
                reg = 2;
                dmg = 3;
                range = 3;

                BuildMe();

                break;
            default: // Default
                Debug.LogError("You done fucked up kid");

                // Setting Default Stats
                maxHealth = 10;
                currentHealth = 1;
                reg = 1;
                dmg = 0;
                range = 0;

                BuildMe();

                break;
        }




        // setting checkpoints
        checkpointOne = maxHealth / 3f;
        checkpointTwo = maxHealth / 2f;
        checkpointThree = maxHealth / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // destroy self if helath is 0 or lower
        if (currentHealth <= 0)
        {
            Die();
        }
        
        // if building is currently building run this
        if (buildingFinish == false)
        {
            // heals currenthealth overtime
            currentHealth += reg * Time.deltaTime;

            // changes color depending on amount of health
            if (currentHealth <= checkpointOne)
            {
                buildingRender.material = red;
            }

            else if (currentHealth <= checkpointTwo)
            {
                buildingRender.material = yellow;
            }

            else if (currentHealth <= checkpointThree)
            {
                buildingRender.material = green;
            }

            // if currentHealth is >= maxHealth make play able to control
            // stop healing over time and set currentHealth to maxHealth
            else if (currentHealth >= maxHealth)
            {
                buildingFinish = true;
                controls = true;
                // Controls
                if (controls)
                {
                    switch (type)
                    {
                        case 0: //  Town hall
                            townHallControls = true;
                            //Debug.Log(townHallControls);
                            break;
                        case 1: // Barracks
                            barracksControls = true;
                            break;
                        case 2: // Armory
                            armoryControls = true;
                            break;
                        case 3: // Tower
                            towerControls = true;
                            break;
                        default:
                            Debug.LogError("Didn't enable any controls");
                            break;
                    }
                }
                currentHealth = maxHealth;
                //Debug.Log("Has building finished building?: " + buildingFinish);

            }
        }
    }

    public void Die()
    {
        switch (type)
        {
            case 0:
                commander.GetComponent<BuildBuildings>().townHallList.Remove(gameObject);
                commander.GetComponent<BuildBuildings>().masterBuildingList.Remove(gameObject);

                break;
            case 1:
                commander.GetComponent<BuildBuildings>().barracksList.Remove(gameObject);
                commander.GetComponent<BuildBuildings>().masterBuildingList.Remove(gameObject);
                break;
            case 2:
                commander.GetComponent<BuildBuildings>().armoryList.Remove(gameObject);
                commander.GetComponent<BuildBuildings>().masterBuildingList.Remove(gameObject);
                break;
            case 3:
                commander.GetComponent<BuildBuildings>().towerList.Remove(gameObject);
                commander.GetComponent<BuildBuildings>().masterBuildingList.Remove(gameObject);
                break;
            default:
                Debug.LogError("Couldn't find object type");
                break;
        }

        Destroy(gameObject);

        if (commander.GetComponent<BuildBuildings>().masterBuildingList.Count == 0)
        {
            commander.GetComponent<BuildBuildings>().PlayerWins();
        }
    }

    public void IsSelected()
    {
        if (currentlySelected == false)
        {
            // not selected
            buildingRender.material = purple;

            if (type == 1)
            {
                barracksBuilder.SetActive(false);
            }
        }
        else
        {
            // selected
            if (unitCamera.GetComponent<Click>().selectedBuildings.Count == 1)
            {
                commander.GetComponent<BuildBuildings>().selected = this.gameObject;
            }
            else if (unitCamera.GetComponent<Click>().selectedBuildings.Count != 1)
            {
                commander.GetComponent<BuildBuildings>().selected = this.gameObject;
            }

            if (type == 1 && barracksControls == true)
            {
                barracksBuilder.SetActive(true);
            }

            buildingRender.material = blue;
        }

    }

    public void BuildMe()
    {
        if (commander.GetComponent<BuildBuildings>().healthupgrade == true)
        {
            maxHealth += commander.GetComponent<BuildBuildings>().healthUpgradeAmount;
        }
    }
}
