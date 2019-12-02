using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject cutsceneManager;
    private int runthrough;

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
                    NextSentence();
                    next.gameObject.SetActive(false);
                }
            }
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
        }
    }

    public void updateSentences(int timesThrough)
    {
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
    }

    ///https://www.youtube.com/watch?v=f-oSXg6_AMQ
}
