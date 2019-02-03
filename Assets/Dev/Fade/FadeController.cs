using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour {

    public SpriteMask mask;
    public SpriteRenderer renderer;

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


    IEnumerator FadeIn()
    {
        renderer.enabled = true;

        float value = 0;
        while (value < 1.0f)
        {
            mask.alphaCutoff = value;
            value += 0.03f;
            yield return null;
        }
        renderer.enabled = false;
    }

    IEnumerator FadeOut()
    {
        renderer.enabled = true;

        float value = 0;
        while (value < 1.0f)
        {
            mask.alphaCutoff = 1 - value;
            value += 0.03f;
            yield return null;
        }
    }

}
