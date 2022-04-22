using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    public Slider masterSlide;
    public Slider effectSlide;
    public Slider musicSlide;
    // Update is called once per frame


    private void Start()
    {
        pauseMenu.SetActive(false);
        LoadSounds();
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
        Application.Quit();
        Time.timeScale = 1f;
    }

    public void LoadCredits()
    {
        Application.LoadLevel(1);
        Time.timeScale = 1f;
    }

    private void LoadSounds()
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(masterSlide.value) * 20f);
        mixer.SetFloat("MusicVol", Mathf.Log(musicSlide.value) * 20f);
        mixer.SetFloat("EffectVol", Mathf.Log(effectSlide.value) * 20f);
    }

    public void SetMasterLevel()
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(masterSlide.value) * 20f);
    }

    public void SetMusicLevel()
    {
        mixer.SetFloat("MusicVol", Mathf.Log(musicSlide.value) * 20f);
    }

    public void SetEffectsLevel()
    {
        mixer.SetFloat("EffectVol", Mathf.Log(effectSlide.value) * 20f);
    }
}
