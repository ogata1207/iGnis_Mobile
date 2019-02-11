using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilController : MonoBehaviour {

    [Header("オイルの最大数")]
    public int oilMax;
    public GameObject origin;

    private TileManager tileManager;
    private ObjectPool pool;

	// Use this for initialization
	void Start () {
        
        tileManager = FindObjectOfType<TileManager>();


        pool = new ObjectPool(transform, origin, oilMax);
        foreach(var obj in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            obj.gameObject.SetActive(false);
        }
    }

	public GameObject GetInstance(Vector3 pos, Quaternion rotate)
    {
        return pool.GetInstance(pos, rotate);
    }
}
