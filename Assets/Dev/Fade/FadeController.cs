using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour {

    public SpriteMask mask;
    public SpriteRenderer renderer;
    [Range(0.0f, 0.1f)]
    public float speed;


	// Use this for initialization
	void Start () {
        mask.frontSortingOrder = 1;
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
        renderer.enabled = true;

        float value = 0;
        while (value < 1.0f)
        {
            mask.alphaCutoff = value;
            value += speed;
            yield return null;
        }
        renderer.enabled = false;
    }

    public IEnumerator FadeOut()
    {
        mask.frontSortingOrder = 15;
        renderer.enabled = true;

        float value = 0;
        while (value < 1.0f)
        {
            mask.alphaCutoff = 1 - value;
            value += speed;
            yield return null;
        }
    }

}
