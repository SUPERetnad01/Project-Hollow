using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DialogueManager : MonoBehaviour
    {
        public Text NameText;
        public Text DialogueText;

        public Animator Animator;

        private Queue<string> _sentences;

        // Start is called before the first frame update
        void Start()
        {
            _sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Animator.SetBool("IsOpen", true);
            NameText.text = dialogue.first_name;

            _sentences.Clear();

            foreach (var sentence in dialogue.sentences)
            {
                _sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }

        public void EndDialogue()
        {
            Animator.SetBool("IsOpen", false);
            Debug.Log("End of Conversation.");
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            var sentence = _sentences.Dequeue();
            DialogueText.text = sentence;
        }
    }
}