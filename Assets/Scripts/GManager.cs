using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    //public static GManager instance = null;
    public static GManager instance;

    [Header("参照")]
    public PlayerMovement playerMov;
    public MouseLook mouseLook;
    public GunController gunCtrl;
    public GameObject gameOverUI;
    public FadeImage fade;
    
    public bool gameOver = false;

    public TextMeshProUGUI gameOverText;

    private AudioSource audioSource;
    private bool firstPush;
    private bool goTitle = false;
    private bool retry = false;

    private void Awake()
    {
        instance = this;

        if (SceneManager.GetActiveScene().name == "Clear")
        {
            Debug.Log("解除");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        gameOverText = gameOverUI.transform.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameOver)
        {
            GameOver();
        }

        CheckCompFade();
    }

    private void GameOver()
    {
        //プレイヤーの行動を制限
        playerMov.enabled = false;
        mouseLook.enabled = false;
        gunCtrl.enabled = false;

        //UIの表示
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverUI.SetActive(true);

    }

    public void RetryOrTitle(string button)
    {
        switch (button)
        {
            case "Retry":
                if (!firstPush)
                {
                    audioSource.Play();
                    firstPush = true;
                    retry = true;
                    fade.StartFadeOut();
                    break;
                }
                break;

            case "Title":
                if (!firstPush)
                {
                    audioSource.Play();
                    firstPush = true;
                    goTitle = true;
                    fade.StartFadeOut();
                    break;
                }
                break;

            default:
                break;
        }
    }

    private void CheckCompFade()
    {
        if (goTitle && fade.IsFadeOutComplete())
        {
            goTitle = false;
            SceneManager.LoadScene("Title");
        }
        else if (retry && fade.IsFadeOutComplete())
        {
            retry = false;
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
