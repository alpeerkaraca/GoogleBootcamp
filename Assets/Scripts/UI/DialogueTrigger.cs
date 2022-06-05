using UnityEngine;

namespace UI
{
    public class DialogueTrigger : MonoBehaviour
    {

        public Dialogue dialogue;
    
        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    
    
    }
}
