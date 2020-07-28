﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private float lifetime = 5.0f;      //生存時間
    //public  bool  isDead   = false;   //生存フラグ

    private GameObject effect;          // エフェクト
    private bool       isBomb = false;  // 爆発したか

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        var v = new Vector2(0, -1);
        rb.AddForce(Quaternion.Euler(0, 0, Random.Range(-45.0f, 45.0f)) * v * 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isBomb) { return; }
        lifetime -= Time.deltaTime;     //生存時間を減らしている
        if(lifetime < 0)
        {
            isBomb = true;

            Destroy(gameObject, 0.1f);  //タイムオーバーの時の消去

            // エフェクトを表示
            effect = this.transform.GetChild(0).gameObject;
            GameObject p = (GameObject)Instantiate(effect);
            p.SetActive(true);
            p.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.1f);
        }        
    }

    public void Kill()
    {
        if (isBomb) { return; }
        isBomb = true;

        // エフェクトを表示
        effect = this.transform.GetChild(1).gameObject;
        GameObject p = (GameObject)Instantiate(effect);
        p.SetActive(true);
        p.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.1f);


        Destroy(gameObject, 0.1f);//プレイヤーがぶつかったときの削除

    }


}
