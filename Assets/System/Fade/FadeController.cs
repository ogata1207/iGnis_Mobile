using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour {

    public SpriteMask mask;
    public SpriteRenderer renderer;
    [Range(0.0f, 0.1f)]
    public float speed;

    public Camera fadeCamera;

	// Use this for initialization
	void Start () {

	}

#if UNITY_EDITOR
    //デバッグ用
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        { StartCoroutine(FadeIn()); }

        if (Input.GetKeyDown(KeyCode.X))
        { StartCoroutine(FadeOut()); }
    }

#endif


    public IEnumerator FadeIn()
    {
        //カメラとスプライトの描画を有効にする
        fadeCamera.enabled = true;
        renderer.enabled = true;

        //トランジション
        float value = 0;
        while (value < 1.0f)
        {
            mask.alphaCutoff = value;
            value += speed;
            yield return null;
        }

        //重くなるので次使う時まで無効
        fadeCamera.enabled = false;
        renderer.enabled = false;
        
    }

    public IEnumerator FadeOut()
    {

        //カメラとスプライトの描画を有効にする
        renderer.enabled = true;
        fadeCamera.enabled = true;

        //トランジション
        float value = 0;
        while (value < 1.0f)
        {
            mask.alphaCutoff = 1 - value;
            value += speed;
            yield return null;
        }
    }

}
