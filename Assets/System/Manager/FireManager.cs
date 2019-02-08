using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FireManager : MonoBehaviour
{

    public Sprite[] fireSprite;
    public int currentNumber;
    public float startTime;         //タイマー
    public float nextTime;          //スプライトまでの時間
    public ObjectPool pool;
    public GameObject originObject; //炎のオブジェクト

    public FireObject[] fireObject;

    static public Sprite currentSprite;


    // Use this for initialization
    void Start()
    {
        var tileNum = FindObjectOfType<TileManager>().tileNum;
        pool = new ObjectPool(transform, originObject, tileNum);
        fireObject = FindObjectsOfType<FireObject>();

        foreach(var obj in fireObject)
        {
            obj.gameObject.SetActive(false);
        }
        StartCoroutine(fireAnimation());
    }

    // Update is called once per frame
    public GameObject GetObject(Vector3 pos, Quaternion rotate)
    {
        return pool.GetInstance(pos,rotate);
    }

    IEnumerator fireAnimation()
    {
        while (true)
        {
            //スプライトの切り替え
            if (Time.time - startTime > nextTime)
            {
                //タイマー初期化
                startTime = Time.time;

                //次のスプライトがない場合、最初のスプライトに戻る
                if (++currentNumber < fireSprite.Length)
                {
                    currentSprite = fireSprite[currentNumber];
                }
                else
                {
                    currentNumber = 0;
                    currentSprite = fireSprite[currentNumber];
                }

            }

            foreach(var obj in fireObject.Where(x => x.gameObject.activeSelf))
            {
                obj.spriteRenderer.sprite = currentSprite;
            }


            yield return null;
        }
    }
}
