using System.Collections;
using System.Collections.Generic;
using Enemy.Enemy_Specific.MyPreciousFox;
using TMPro;
using UnityEngine;
using CharacterController = Player.CharacterController;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI 
    nameTextMesh, 
    dialogueTextMesh;
    private int i = 1;

    private CharacterController cc;
    public Animator dialogueBoxAnim;

    public Queue<string> sentences;
    private static readonly int Open = Animator.StringToHash("open");
    void Start()
    {
        sentences = new Queue<string>();
    }
    private void Update()
    {
        if (GameObject.FindWithTag("Player") != null)
            cc = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBoxAnim.SetBool("open", true);
        nameTextMesh.text = dialogue.NPCName;
        cc.isDialogBoxOpen = true;
        cc.moveSpeed = 0f;
        cc.runSpeed = 0f;
        cc._animator.SetFloat("Speed", 0f);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        dialogueBoxAnim.SetBool(Open,false);
        cc.isDialogBoxOpen = false;
        cc.moveSpeed = 5f;
        cc.runSpeed = 7f;
        if(i == 2)
            if(GameObject.Find("Fox") != null)
                GameObject.Find("Fox").GetComponentInChildren<Animator>().SetBool("fade", true);
        i++;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTextMesh.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTextMesh.text += letter;
            yield return null;
        }
    }
    

}
