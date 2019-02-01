using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour {

    [SerializeField, Range(0.0f, 2.0f)]
    public float scrollSpeed;
    public Vector2 scrollPower;
    private Renderer renderer;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {

        float scroll = Mathf.Repeat(Time.time * scrollSpeed,1);
        Vector2 offset = new Vector2(scrollPower.x * scroll, scrollPower.y * scroll);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
