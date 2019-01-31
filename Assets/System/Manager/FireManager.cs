using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        pool = new ObjectPool(originObject, TileManager.STAGE_MAX_SIZE * TileManager.STAGE_MAX_SIZE);
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
            foreach(var sprite in fireObject)
            {
                sprite.spriteRenderer.sprite = currentSprite;
            }
            yield return null;
        }
    }
}
