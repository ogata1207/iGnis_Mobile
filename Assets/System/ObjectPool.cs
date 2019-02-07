using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public List<GameObject> pool = new List<GameObject>();
    public string effectName;
    public int maxCount;
    public bool[] isUsed;

    public ObjectPool(Transform parent,GameObject originalObject, int num)
    {
        maxCount = num;
        isUsed = new bool[num];
        effectName = originalObject.name;

        for (int i = 0; i < num; i++)
        {
            isUsed[i] = false;
            var obj = GameObject.Instantiate(originalObject);
            obj.name = originalObject.name + i.ToString();
            obj.SetActive(false);
            obj.transform.parent = parent;
            pool.Add(obj);
        }
    }

    public GameObject GetInstance(Vector3 position, Quaternion rotation)
    {
        for(int i = 0; i < maxCount; i++)
        {
            if(!isUsed[i])
            {
                isUsed[i] = true;
                pool[i].transform.position = position;
                pool[i].transform.rotation = rotation;
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }
}
