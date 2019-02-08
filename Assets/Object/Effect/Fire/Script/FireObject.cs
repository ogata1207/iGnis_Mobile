using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour {
    static  private FireManager fireManager;
    public SpriteRenderer spriteRenderer;

    void Start () {

        if (fireManager == null) fireManager = FindObjectOfType<FireManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

	}

    
}
