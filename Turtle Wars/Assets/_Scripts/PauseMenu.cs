using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool Cancel = false;
    public bool PlayButton = false;
    public GameObject Pause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Cancel") && Cancel == false)
        {
            Pause.SetActive(true);
            Time.timeScale = 0;
            Cancel = true;
        }

        else if (Input.GetButtonUp("Cancel") || PlayButton == true && Cancel == true)
        {
            Pause.SetActive(false);
            Time.timeScale = 1;
            Cancel = false;
        }
        
    }

    public void Resume()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
        Cancel = false;
    }
}
