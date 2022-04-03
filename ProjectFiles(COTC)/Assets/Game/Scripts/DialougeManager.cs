using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

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
    private AudioSource audio;
    public AudioClip letters;
    public AudioClip nextSentance;

    private float dVolume;

    public static event Action onNext;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        audio = GetComponent<AudioSource>();
        dVolume = audio.volume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            DisplayNextSentance();
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
            if (!audio.isPlaying)
            {
                audio.clip = nextSentance;
                audio.volume = 1;
                audio.Play();
            }
            return;
        }

        string sentance = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(DisplayText(sentance));
        if (!audio.isPlaying)
        {
            audio.clip = nextSentance;
            audio.volume = 1;
            audio.Play();
        }
    }

    IEnumerator DisplayText(string sentance)
    {
        dialougeText.text = "";

        foreach(char letter in sentance.ToCharArray())
        {
            dialougeText.text += letter;
            if (!audio.isPlaying)
            {
                audio.clip = letters;
                audio.volume = dVolume;
                audio.Play();
            }
            yield return new WaitForSeconds(0.03f);
        }
    }

    void EndDialouge()
    {
        animator.SetBool("IsOpen", false);
        trigger.Invoke();
    }
}
