using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float magnitude;
    public GunController gunCtrl;

    private Vector3 originalPos;

    public void Start()
    {
        Vector3 originalPos = transform.localPosition;
    }

    //画面の揺れ
    public IEnumerator Shake ()
    {
        while (gunCtrl.shooting)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            yield return new WaitForSeconds(0.2f);
        }
    }

    //カメラリセット
    public void ResetCamPos()
    {
        transform.localPosition = originalPos;
    }

}
