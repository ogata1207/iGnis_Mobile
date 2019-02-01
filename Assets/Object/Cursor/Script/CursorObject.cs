using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour {

    private Vector2 moveDirection;
    private float moveDelay = 0.1f;
    private bool isDelay = false;
    private float startTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > moveDelay)
        {
            isDelay = false;
        }

        if (isDelay) return;
        
        if (moveDirection != Vector2.zero)
        {
            var pos = transform.position;
            transform.position = pos + (Vector3)moveDirection;
            isDelay = true;
            startTime = Time.time;
        }
        
	}
 
    public void MoveUp()
    {
        moveDirection = Vector2.up;
    }
    public void MoveDown()
    {
        moveDirection = Vector2.down;
    }
    public void MoveLeft()
    {
        moveDirection = Vector2.left;
    }
    public void MoveRight()
    {
        moveDirection = Vector2.right;
    }
    public void MoveStop()
    {
        moveDirection = Vector2.zero;
    }
}
