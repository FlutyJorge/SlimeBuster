using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointText : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI pointTx; 

    // Start is called before the first frame update
    void Start()
    {
        pointTx = GetComponent<TextMeshProUGUI>();
        pointTx.SetText("Point 0");
    }
}
