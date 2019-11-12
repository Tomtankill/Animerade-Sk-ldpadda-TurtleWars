using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueText : MonoBehaviour
{
    public bool hashappened;
    //public GameObject gameover;
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public float sentencewait = 3f;
    //public GameObject FPC;  // Assign the First Person Controller to this in the Editor.


    void Start() {
        hashappened = false;

    }

    void Update() {

        //if (Camera.main.transform.parent.GetComponent<Sanity>().sanityCurrent <= 0)
        //{
            if (hashappened == false)
            {
                dialoguestart();

                hashappened = true;
            }
            //if (textDisplay.text == sentences[index])
            //{
            //    sentencewait -= Time.deltaTime;
            //    if (sentencewait <= 0)
            //    {
            //        NextSentence();
            //    }
            //}
        //}
            
        
    }
    public void dialoguestart() {
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
            sentencewait = 3f;

        } else {
            textDisplay.text = "";
            sentencewait = 3f;
        }
    }
    ///https://www.youtube.com/watch?v=f-oSXg6_AMQ
}
