using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMove : MonoBehaviour
{
    //public float valueX;
    //public float valueY;

    public float speed = 0.05f;

    Material thisMat;


    // Start is called before the first frame update
    void Start()
    {
        thisMat = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float xx = (Time.timeSinceLevelLoad * speed) % 1 ;

        thisMat.SetTextureOffset("_MainTex", new Vector2(xx, -xx));
    }
}
