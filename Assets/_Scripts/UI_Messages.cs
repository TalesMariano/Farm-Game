using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Messages : MonoBehaviour
{
    public static UI_Messages instance;

    public CanvasGroup cg;
    public TMP_Text textBox;


    float showDuration = 0.3f;
    float hideDuration = 0.3f;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cg.alpha = 0;
        transform.localScale = Vector3.zero;
    }

    [ContextMenu("Test Mesage")]
    void TestMesgae()
    {
        ReceiveMessage("Test");

    }


    public void ReceiveMessage (string msg)
    {
        textBox.text = msg;
        ShowBox();
    }

    void ShowBox()
    {
        StopAllCoroutines();
        StartCoroutine( IShow() );
    }

    public void HideBox()
    {
        StopAllCoroutines();
        StartCoroutine( IHide() );
    }

    IEnumerator IShow()
    {
        for (float i = 0; i < showDuration; i+= Time.deltaTime)
        {
            cg.alpha = i / showDuration;
            transform.localScale = Vector3.one * i / showDuration;
            yield return null;
        }

        cg.alpha = 1;
        transform.localScale = Vector3.one;
    }

    IEnumerator IHide()
    {
        for (float i = hideDuration; i > 0; i -= Time.deltaTime)
        {
            cg.alpha = i / hideDuration;
            transform.localScale = Vector3.one * i / hideDuration;
            yield return null;
        }

        cg.alpha = 0;
        transform.localScale = Vector3.zero;
    }

}
