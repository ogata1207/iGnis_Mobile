using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour {

    static public TileManager tileManager;
    static public FireManager fireManager;
    public StateManager state;
    public bool isActive;
    public int tileId;

    //燃えるまでの時間
    public float burnTime;

    void Start()
    {
        if (tileManager == null) tileManager = FindObjectOfType<TileManager>();
        if (fireManager == null) fireManager = FindObjectOfType<FireManager>();

        state = new StateManager();
        state.RequestState(Idle);
    }

    public void Idle(bool stateInitialize)
    {
        if (isActive) state.RequestState(Burn);
    }

    public void Burn(bool stateInitialize)
    {
        if (stateInitialize) tileId = 98;
        if(state.elapsedTime > burnTime)
        {
            tileId = 99;
            Debug.Log("燃えた");
            var x = (int)transform.position.x;
            var y = (int)transform.position.y;

            if(x + 1 <= TileManager.STAGE_MAX_SIZE)
            if (tileManager.tileObject[x + 1, y].tileId == 1 && tileManager.tileObject[x + 1, y].isActive == false) tileManager.tileObject[x + 1, y].SetTime(1.0f);

            if (x - 1 >= 0)
                if (tileManager.tileObject[x - 1, y].tileId == 1 && tileManager.tileObject[x - 1, y].isActive == false) tileManager.tileObject[x - 1, y].SetTime(1.0f);

            if(y - 1 >= 0)
                if (tileManager.tileObject[x, y - 1].tileId == 1 && tileManager.tileObject[x, y - 1].isActive == false) tileManager.tileObject[x, y - 1].SetTime(1.0f);

            if (y + 1 <= TileManager.STAGE_MAX_SIZE)
                if (tileManager.tileObject[x, y + 1].tileId == 1 && tileManager.tileObject[x, y + 1].isActive == false) tileManager.tileObject[x, y + 1].SetTime(1.0f);


            //炎のエフェクトを生成
            fireManager.GetObject(transform.position, transform.rotation);
            //更新をしなくする
            isActive = false;
            
        }
    }

    public void SetTime(float time)
    {
        if (isActive == true && tileId == 99) return;
        burnTime = time;
        isActive = true;
    }
}
