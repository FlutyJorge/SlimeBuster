using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker: MonoBehaviour
{
    private Light gunLight;
    public float interval = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        //���C�g�R���|�[�l���g�擾
        gunLight = GetComponent<Light>();
        gunLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine("Blink");
        }
        else
        {
            //�R���[�`���I���Ɠ����Ƀ��C�g������
            StopCoroutine("Blink");
            gunLight.enabled = false;
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            //�_�ŏ���
            gunLight.enabled = !gunLight.enabled;

            yield return new WaitForSeconds(interval);
        }
    }
}
