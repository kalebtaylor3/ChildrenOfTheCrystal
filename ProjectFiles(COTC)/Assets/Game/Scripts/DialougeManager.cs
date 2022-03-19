using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialougeManager : MonoBehaviour
{
    public Text nameText;
    public Text dialougeText;
    public Animator animator;

    [HideInInspector]
    public Queue<string> sentences;

    [SerializeField] private UnityEvent trigger;
    [SerializeField] private UnityEvent door;
    bool trap;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialouge(Dialouge dialouge)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialouge.name;
        sentences.Clear();

        foreach(string sentence in dialouge.sentances)
        {
            sentences.Enqueue(sentence);
        }
        trap = dialouge.trap;
        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        if(sentences.Count == 0)
        {
            if (trap)
                door.Invoke();
            EndDialouge();
            return;
        }

        string sentance = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(DisplayText(sentance));
    }

    IEnumerator DisplayText(string sentance)
    {
        dialougeText.text = "";

        foreach(char letter in sentance.ToCharArray())
        {
            dialougeText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    void EndDialouge()
    {
        animator.SetBool("IsOpen", false);
        trigger.Invoke();
    }
}
