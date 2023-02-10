using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraTransform.Rotate(0, 2 * Time.deltaTime, 0);
    }
}
