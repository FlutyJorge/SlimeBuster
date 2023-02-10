using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;

    private bool firstPush = false;
    private bool goNextScene = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PressButton()
    {
        if (!firstPush)
        {
            audioSource.Play();
            fade.StartFadeOut();
            firstPush = true;
        }
    }

    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            if (SceneManager.GetActiveScene().name == "Title")
            {
                SceneManager.LoadScene("Game");
                goNextScene = true;
            }
            else if (SceneManager.GetActiveScene().name == "Clear")
            {
                SceneManager.LoadScene("Title");
                goNextScene = true;
            }
        }
    }
}
