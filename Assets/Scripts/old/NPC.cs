using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogueUI;
    public bool inRange;
    private Animator anim;

    public Dialogue Dialogue;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        dialogueUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogueUI.SetActive(true);
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        anim.SetBool("inRange", true);
        TriggerDialogue();
    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        anim.SetBool("inRange", false);
        dialogueUI.SetActive(false);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Dialogue);
    }
}
