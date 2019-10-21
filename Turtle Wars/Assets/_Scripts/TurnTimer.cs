using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnTimer : MonoBehaviour
{
     private float timerMax = 31.0f;
    [SerializeField] private float timerCurrent = 31.0f;
     private int iTimerCurrent;
    public bool p1Turn = true;
    public bool p2Turn = false;
    public TextMeshProUGUI notyourturn;

    public Image imagetimer;
    public TextMeshProUGUI timerText;

    public float filling;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // P1 turn
        timerCurrent -= Time.deltaTime;
        iTimerCurrent = (int) timerCurrent;
        filling = timerCurrent / timerMax;
        imagetimer.fillAmount = filling;
        timerText.text = iTimerCurrent.ToString();

        if (timerCurrent <= 0 & p1Turn == true)
        {
            p1Turn = false;
            timerCurrent = timerMax;
            p2Turn = true;
            imagetimer.GetComponent<Image>().color = new Color(1, 0, 0, 1);
            Debug.Log("It is P2's turn" + p2Turn);
        }
        if (timerCurrent <= 0 & p2Turn == true)
        {
            p2Turn = false;
            timerCurrent = timerMax;
            p1Turn = true;
            imagetimer.GetComponent<Image>().color = new Color(0, 1, 0, 1);
            Debug.Log("It is P1's turn: " + p1Turn);
        }
    }

    public bool IsMyTurn()
    {
        if (p1Turn == true)
        {
            return true;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray))
            {
                Destroy(Instantiate(notyourturn, transform.position, transform.rotation), 1);

            }
            return false;
        }
    }
}
