using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    public float lifeTime = 0.6f;
    public float minDist = 0f;
    public float maxDist = 0f;

    [HideInInspector] public TextMeshPro tmPro;
    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    private void Awake()
    {
        tmPro = GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //�e�L�X�g���΂�����
        float direction = Random.Range(-30, 120);
        //�e�L�X�g����ԋ���
        float dist = Random.Range(minDist, maxDist);

        //�e�L�X�g�̖ړI�n������
        iniPos = transform.position;
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //�e�L�X�g���J�����̌����ɌŒ�
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        timer += Time.deltaTime;
        float fraction = lifeTime / 2f;

        //�e�L�X�g�̔j��A�t�F�[�h�C��
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
        else if (timer > fraction)
        {
            tmPro.color = Color.Lerp(tmPro.color, Color.clear, (timer - fraction) / (lifeTime - fraction));
        }

        //�e�L�X�g�̈ʒu�Ƒ傫�������Ԍo�߂ɂ��ύX
        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifeTime));

        //SetDamageText();
    }

    //�X���C������_��������Ă΂��
    public void SetDamageText(int damage)
    {
        tmPro.SetText(damage.ToString());
    }
}
