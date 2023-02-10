using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClearCollider : MonoBehaviour
{
    public GameObject clearTxObj;
    public PlayerStatus playerSta;
    public FadeImage fade;

    private TextMeshProUGUI clearTx;
    private bool firstPush = false;
    private bool clear = false;

    // Start is called before the first frame update
    void Start()
    {
        clearTx = clearTxObj.GetComponent<TextMeshProUGUI>();
        clearTxObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade.IsFadeOutComplete() && clear)
        {
            SceneManager.LoadScene("Clear");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            if (playerSta.coin != 30)
            {
                clearTxObj.SetActive(true);
                clearTx.SetText("Collect All Coins!");
            }
            else
            {
                clearTxObj.SetActive(true);
                clearTx.SetText("Press E!");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Capsule") && playerSta.coin == 30 && Input.GetKey(KeyCode.E) && !firstPush)
        {
            firstPush = true;
            clear = true;
            fade.StartFadeOut();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            clearTxObj.SetActive(false);
        }
    }
}
