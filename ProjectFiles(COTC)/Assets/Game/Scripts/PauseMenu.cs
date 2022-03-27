using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    AudioMixerSnapshot unPausedSnap;
    [SerializeField]
    AudioMixerSnapshot pausedSnap;
    private bool paused = false;

    public GameObject pauseMenu;

    [SerializeField]
    private AudioMixer mixer;
    // Update is called once per frame


    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause(!paused);
    }

    private void Pause(bool pause)
    {
        paused = pause;
        pauseMenu.SetActive(pause);
        if(paused)
        {
            Time.timeScale = 0f;
            pausedSnap.TransitionTo(0.1f);
        }
        else
        {
            Time.timeScale = 1f;
            unPausedSnap.TransitionTo(0.1f);
        }
    }

    public void ExitGame()
    {
        Application.LoadLevel(0);
    }

    public void LoadCredits()
    {

    }

    public void SetMasterLevel(float sliderv)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderv) * 20f);
    }

    public void SetMusicLevel(float sliderv)
    {
        mixer.SetFloat("MusicVol", Mathf.Log(sliderv) * 20f);
    }

    public void SetEffectsLevel(float sliderv)
    {
        mixer.SetFloat("EffectVol", Mathf.Log(sliderv) * 20f);
    }
}
