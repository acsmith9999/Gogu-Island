using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue2 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay, nameText;
    [TextArea(3, 10)]
    public string[] sentences;
    public string NPCName;
    private int index;
    public float typingSpeed;
    public GameObject dialogueUI;
    public GameObject continueButton;

    public bool inRange;
    private Animator anim;

    private Pause pause;

    private void Start()
    {
        anim = GetComponent<Animator>();
        pause = FindObjectOfType<Pause>();
        dialogueUI.SetActive(false);
    }
    private void Update()
    {
        if (inRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogueUI.SetActive(true);
                pause.lockCursor();
                nameText.text = NPCName;
                if(index ==0)
                {
                    NextSentence();
                }
            }
        }
        if (textDisplay.text == sentences[index-1])
        {
            continueButton.SetActive(true);
        }

    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if(index < sentences.Length)
        {
            textDisplay.text = "";
            StartCoroutine(Type());
            index++;
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            dialogueUI.SetActive(false);
            index = 0;
            pause.enableCursor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        anim.SetBool("inRange", true);
    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        anim.SetBool("inRange", false);
        dialogueUI.SetActive(false);
    }
}
