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

    private FireObject[] fireObject;

    static public Sprite currentSprite;


    // Use this for initialization
    void Start()
    {
        var tileNum = FindObjectOfType<TileManager>().tileNum;
        pool = new ObjectPool(originObject, tileNum);
        fireObject = FindObjectsOfType<FireObject>();

        StartCoroutine(fireAnimation());
    }

    // Update is called once per frame


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

            fireObject.Where(x => x.gameObject.activeSelf).
                       Select(x => x.spriteRenderer.sprite = currentSprite);
            

            yield return null;
        }
    }
}
