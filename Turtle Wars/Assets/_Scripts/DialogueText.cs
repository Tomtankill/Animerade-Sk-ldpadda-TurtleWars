using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueText : MonoBehaviour
{
    public bool trigger;
    public TextMeshProUGUI textDisplay;

    public string[] sentences;
    public string[] sentences1;
    public string[] sentences2;
    public string[] sentences3;
    public string[] sentences4;

    private int index;
    public float typingSpeed;
    public float sentencestart;
    private float sentencewait;
    public Image next;
    public GameObject cameraShot;
    private int runthrough;
    private int countClicks;

    // Camera Stuff
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject shot4;
    public GameObject shot5;
    public GameObject shot6;
    public GameObject shot7;
    public GameObject shot8;
    public GameObject shot9;
    public GameObject shot10;
    public GameObject shot11;

    //public GameObject FPC;  // Assign the First Person Controller to this in the Editor.


    void Start() {
        sentencewait = sentencestart;
        Dialoguestart();
        runthrough = 0;
    }

    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            sentencewait -= Time.deltaTime;
            if (sentencewait <= 0)
            {
                next.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    countClicks++;
                    NextSentence();
                    next.gameObject.SetActive(false);
                }
            }
        }

        ShotTransition();
    }

    private void ShotTransition() {
        switch (countClicks) {
            case 1:
                shot1.SetActive(true);
                shot2.SetActive(false);
                shot3.SetActive(false);
                shot4.SetActive(false);
                shot5.SetActive(false);
                shot6.SetActive(false);
                shot7.SetActive(false);
                shot8.SetActive(false);
                shot9.SetActive(false);
                shot10.SetActive(false);
                shot11.SetActive(false);
                break;
            case 7:
                shot1.SetActive(false);
                shot2.SetActive(false);
                shot3.SetActive(false);
                shot4.SetActive(false);
                shot5.SetActive(false);
                shot6.SetActive(false);
                shot7.SetActive(false);
                shot8.SetActive(false);
                shot9.SetActive(false);
                shot10.SetActive(false);
                shot11.SetActive(false);
                break;
            case 13:
                shot1.SetActive(false);
                shot2.SetActive(false);
                shot3.SetActive(false);
                shot4.SetActive(false);
                shot5.SetActive(false);
                shot6.SetActive(false);
                shot7.SetActive(false);
                shot8.SetActive(false);
                shot9.SetActive(false);
                shot10.SetActive(false);
                shot11.SetActive(false);
                break;
            case 20:
                shot1.SetActive(false);
                shot2.SetActive(false);
                shot3.SetActive(false);
                shot4.SetActive(false);
                shot5.SetActive(false);
                shot6.SetActive(false);
                shot7.SetActive(false);
                shot8.SetActive(false);
                shot9.SetActive(false);
                shot10.SetActive(false);
                shot11.SetActive(false);
                break;
            case 25:
                shot1.SetActive(false);
                shot2.SetActive(false);
                shot3.SetActive(false);
                shot4.SetActive(false);
                shot5.SetActive(false);
                shot6.SetActive(false);
                shot7.SetActive(false);
                shot8.SetActive(false);
                shot9.SetActive(false);
                shot10.SetActive(false);
                shot11.SetActive(false);
                break;
            case 33:
                shot1.SetActive(false);
                shot2.SetActive(false);
                shot3.SetActive(false);
                shot4.SetActive(false);
                shot5.SetActive(false);
                shot6.SetActive(false);
                shot7.SetActive(false);
                shot8.SetActive(false);
                shot9.SetActive(false);
                shot10.SetActive(false);
                shot11.SetActive(false);
                break;
            case 40:
                shot1.SetActive(false);
                shot2.SetActive(false);
                shot3.SetActive(false);
                shot4.SetActive(false);
                shot5.SetActive(false);
                shot6.SetActive(false);
                shot7.SetActive(false);
                shot8.SetActive(false);
                shot9.SetActive(false);
                shot10.SetActive(false);
                shot11.SetActive(false);
                break;
            case 45:
                shot1.SetActive(false);
                shot2.SetActive(false);
                shot3.SetActive(false);
                shot4.SetActive(false);
                shot5.SetActive(false);
                shot6.SetActive(false);
                shot7.SetActive(false);
                shot8.SetActive(false);
                shot9.SetActive(false);
                shot10.SetActive(false);
                shot11.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void Dialoguestart() {

        StartCoroutine(Type());

    }


    IEnumerator Type() {
        foreach(char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }

    }
    public void NextSentence() {
        if (index < sentences.Length - 1) {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            sentencewait = sentencestart;

        } else {
            textDisplay.text = "";
            sentencewait = sentencestart;
            runthrough++;
            UpdateSentences(runthrough);
        }
    }

    public void UpdateSentences(int timesThrough)
    {
        index = 0;
        switch (timesThrough)
        {
            case 1:
                sentences = sentences1;
                break;
            case 2:
                sentences = sentences2;
                break;
            case 3:
                sentences = sentences3;
                break;
            case 4:
                sentences = sentences4;
                break;
            default:
                break;
        }
        Dialoguestart();
    }

    ///https://www.youtube.com/watch?v=f-oSXg6_AMQ
}
