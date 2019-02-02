using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour {

    private Vector3 localPosition;
    private Coroutine shakeCoroutine;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            if(shakeCoroutine == null)
            shakeCoroutine = StartCoroutine(Shake(3.0f, 0.1f));
    }
    IEnumerator Shake(float power, float time)
    {
        //開始前の座標を保存
        localPosition = transform.localPosition;

        //開始時の時間を保存
        var startTime = Time.time;

        while(Time.time - startTime < time)
        {
            float x = Random.Range(-power, power);
            float y = Random.Range(-power, power);

            transform.localPosition = localPosition + new Vector3(x, y);

            yield return null;
        }
        
        transform.localPosition = localPosition;
        shakeCoroutine = null;
    }
}
