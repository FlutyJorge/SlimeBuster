using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetItem : MonoBehaviour
{
    [Header("Reference")]
    public PlayerMovement playerMov;
    public TextMeshProUGUI coinTx;
    public PlayerStatus playerSta;

    // Start is called before the first frame update
    void Start()
    {
        coinTx.SetText("Coin " + playerSta.coin.ToString() + "/30");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GetHeartOrCoin(other));
    }

    IEnumerator GetHeartOrCoin(Collider other)
    {
        //ハート取得
        if (other.CompareTag("Heart") && this.gameObject.CompareTag("Capsule"))
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
            other.enabled = false;
            mesh.enabled = false;
            audioSource.Play();
            playerMov.playerHP = (int)Mathf.Clamp(playerMov.playerHP + 2, 0, 100);
            playerMov.hpBer.value = playerMov.playerHP;

            yield return new WaitForSeconds(5f);
            Destroy(other.gameObject);
            yield break;
        }

        //コイン取得
        if (other.CompareTag("Coin") && this.gameObject.CompareTag("Capsule"))
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
            other.enabled = false;
            mesh.enabled = false;
            audioSource.Play();
            playerSta.coin++;
            coinTx.SetText("Coin " + playerSta.coin.ToString() + "/30");

            yield return new WaitForSeconds(5f);
            Destroy(other.gameObject);
            yield break;
        }
        yield break;
    }
}
