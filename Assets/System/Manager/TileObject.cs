using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour {
    public StateManager state;
    public bool isActive;
    public int tileId;

    //燃えるまでの時間
    public float burnTime;

    void Start()
    {
        state = new StateManager();
        state.RequestState(Idle);
    }

    public void Idle(bool stateInitialize)
    {
        if (isActive) state.RequestState(Burn);
    }

    public void Burn(bool stateInitialize)
    {
        if(state.elapsedTime > burnTime)
        {
            tileId = 99;
            Debug.Log("燃えた");
            isActive = false;
        }
    }

    public void SetTime(float time)
    {
        burnTime = time;
        isActive = true;
    }
}
