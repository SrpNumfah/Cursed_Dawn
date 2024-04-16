using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{

    public TMP_Text name_text;
    public TMP_Text conversation_Text;
    public GameObject dialogue_UI;
    public Animator dialogue_Anim;

    public CinemachineVirtualCamera dialogueCam;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogue_UI.SetActive(true);
        name_text.text = dialogue.name;
        dialogue_Anim.SetBool("IsOpen", true);
        Debug.Log(dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);

            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        

    }

    IEnumerator TypeSentence(string sentence)
    {
        conversation_Text.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            conversation_Text.text += letter;
            yield return null;
        }
    }

    public void EndDialigue()
    {
        dialogue_Anim.SetBool("IsOpen", false);
        dialogueCam.gameObject.SetActive(false);
       
    }
}
