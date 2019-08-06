﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    public static bool superTime = false;
    public static bool unBeatTime = false;
    public bool inputUp = false;
    public bool inputDown = false;
    public int retryNum;
    public static float distanceY;
    Vector3 initPos;


    void Start(){
        initPos = transform.position;
        distanceY = 0;
    }

    void Update () {
        //키보드 조작용
        distanceY = Input.GetAxis("Vertical") * 0.8f;
        // 상하 이동
        if(inputUp)  distanceY += 0.5f;
        else if(inputDown) distanceY =- 0.5f;
        //else if (!inputUp && !inputDown) distanceY = 0;
        distanceY *= Time.deltaTime * 8;
        transform.Translate(0,distanceY,0);
        moveRange();
    }

    void moveRange(){
     Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
     if(pos.y < 0.2f) pos.y = 0.2f; // down OUT
     if(pos.y > 0.88f) pos.y = 0.88f; // up OUT
     transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(superTime == false) {
        lifeManager.lifeNum--;
        superTime = true;
        unBeatTime = true;
        StartCoroutine(UnBeatTime());
        }
        
    }


    IEnumerator UnBeatTime(){
        int countTime = 0;

        if(lifeManager.lifeNum != 0)
        while(countTime < 7){
            // Alpha effect
            if(countTime%2 == 0)
            GetComponent<SpriteRenderer>().color = new Color32(255,255,255,90);
            else
            GetComponent<SpriteRenderer>().color = new Color32(255,255,255,180);

            yield return new WaitForSeconds(0.1f);

            countTime++;
        }

        //effect end
        GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
        superTime = false;
        unBeatTime = false;

        yield return null;
    }


}
