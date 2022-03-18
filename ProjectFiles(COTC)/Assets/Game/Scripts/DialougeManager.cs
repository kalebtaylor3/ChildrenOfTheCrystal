using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    public Text nameText;
    public Text dialougeText;
    public Animator animator;

    private Queue<string> sentences;

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

        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        if(sentences.Count == 0)
        {
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
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialouge()
    {
        animator.SetBool("IsOpen", false);
    }
}
