using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    [Header("�ŏ��Ƀt�F�[�h�C�����������Ă��邩")] public bool firstFadeInComp;
    [HideInInspector] public bool compFadeIn = false;

    private Image img;
    private float timer = 0.0f;
    private int frameCount = 0;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private bool compFadeOut = false;

    //�t�F�[�h�C�����J�n����
    public void StartFadeIn()
    {
        if (fadeIn)
        {
            return;
        }
        fadeIn = true;
        compFadeIn = false;
        timer = 0.0f;
        img.color = new Color(1, 1, 1, 1);
        img.raycastTarget = true;
    }

    //�t�F�[�h�C��������������
    public bool IsFadeInComplete()
    {
        return compFadeIn;
    }

    //�t�F�[�h�A�E�g���J�n����
    public void StartFadeOut()
    {
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeOut = true;
        compFadeOut = false;
        timer = 0.0f;
        img.color = new Color(1, 1, 1, 0);
        img.raycastTarget = true;
    }

    //�t�F�[�h�A�E�g������������
    public bool IsFadeOutComplete()
    {
        return compFadeOut;
    }

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();

        if (firstFadeInComp)
        {
            FadeInComplete();
        }
        else
        {
            StartFadeIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�V�[���J�ڎ��̏������s�̂���2�t���[���҂�
        if (frameCount > 2)
        {
            if (fadeIn)
            {
                FadeInUpdate();
            }
            else if (fadeOut)
            {
                FadeOutUpdate();
            }
        }
        ++frameCount;
    }

    //�t�F�[�h�C����
    private void FadeInUpdate()
    {
        if (timer < 1f)
        {
            img.color = new Color(1, 1, 1, 1 - timer);
        }
        else
        {
            FadeInComplete();
        }
        timer += Time.deltaTime;
    }

    //�t�F�[�h�A�E�g��
    private void FadeOutUpdate()
    {
        if (timer < 1f)
        {
            img.color = new Color(1, 1, 1, timer);
        }
        else
        {
            FadeOutComplete();
        }
        timer += Time.deltaTime;
    }

    //�t�F�[�h�C������
    private void FadeInComplete()
    {
        img.color = new Color(1, 1, 1, 0);
        img.raycastTarget = false;
        timer = 0.0f;
        fadeIn = false;
        compFadeIn = true;
    }

    //�t�F�[�h�A�E�g����
    private void FadeOutComplete()
    {
        img.color = new Color(1, 1, 1, 1);
        img.raycastTarget = false;
        timer = 0.0f;
        fadeOut = false;
        compFadeOut = true;
    }
}
