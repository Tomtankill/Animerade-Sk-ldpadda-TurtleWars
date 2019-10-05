using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowBuilding : MonoBehaviour
{
    // Letting the building know who the commander is
    public GameObject commander;

    // bools
    [SerializeField]private bool buildingFinish;
    private bool controls;

    // render
    [SerializeField]private Renderer buildingRender;

    // health values
    public float currentHealth;
    public float maxHealth;
    private float reg = 2f;

    // checkpoints
    private float checkpointOne;
    private float checkpointTwo;
    private float checkpointThree;

    // materials
    [SerializeField] private Material red;
    [SerializeField] private Material yellow;
    [SerializeField] private Material green;

    // Start is called before the first frame update
    void Start()
    {
        // bools are false on start
        buildingFinish = false;
        controls = false;
        buildingRender = GetComponent<Renderer>();
        currentHealth = 1;

        // setting maxHealth depending on what building it is
        if (gameObject.name.Contains("Townhall"))
        {
            commander.GetComponent<BuildBuildings>().townHallList.Add(gameObject);
            maxHealth = 150;
        }
        else if (gameObject.name.Contains("Barracks"))
        {
            commander.GetComponent<BuildBuildings>().barracksList.Add(gameObject);
            maxHealth = 100;
        }
        else if (gameObject.name.Contains("Armory"))
        {
            commander.GetComponent<BuildBuildings>().armoryList.Add(gameObject);
            maxHealth = 100;
        }
        else if (gameObject.name.Contains("Tower"))
        {
            commander.GetComponent<BuildBuildings>().towerList.Add(gameObject);
            maxHealth = 75;
        }

        // setting checkpoints
        checkpointOne = maxHealth / 3f;
        checkpointTwo = maxHealth / 2f;
        checkpointThree = maxHealth / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // destroy if helath is 0 or lower
        if (currentHealth <= 0)
        {
            
            Destroy(gameObject);
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
                currentHealth = maxHealth;
                Debug.Log("building has finished building? " + buildingFinish);

            }

        }
    }
}
