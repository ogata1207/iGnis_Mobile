using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour {
    static  private FireController fireController;
    public SpriteRenderer spriteRenderer;

    void Start () {

        if (fireController == null) fireController = FindObjectOfType<FireController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

	}

    
}
