using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject item, bomb;

    private float ItemInterval = 0.5f; // アイテムの出現時間
    private float itemTime = 0;        // アイテムの出現計算用

    private GameObject title, over;
    private AudioSource snd;           //音声
    public AudioClip se_start, se_over;//スタート時と終了時の音声を表示

    public int Mode = 0;               //現在のゲームモード
    public int Score;                  //スコアの関数

    private float PlayTime = 0;        //制限時間の関数
    private Text ScoreText;            //スコアのテキスト

    // Start is called before the first frame update
    void Start()
    {
        title = GameObject.Find("Title");
        over = GameObject.Find("Over");
        over.SetActive(false);         //ゲームオーバー画面をいったん非表示に
        snd = gameObject.AddComponent<AudioSource>();
        ScoreText = GameObject.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Mode)
        {
            case 0:
                Title();
                break;
            case 1:
                Game();
                break;
            case 2:
                Over();
                break;
        }

        ScoreText.text = "Time:" + Mathf.FloorToInt(PlayTime).ToString() + ", Score: " + Score.ToString();
    }

    private void Title()
    {
        if (Input.GetKeyDown(KeyCode.Space))//タイトルでスペースキーが押されたら
        {
            title.SetActive(false);         //タイトル画面を非表示
            snd.PlayOneShot(se_start);  
            Mode++;                         //Modeの値を一つ増やしてゲームの方へ

            Score = 0;
            PlayTime = 60.0f;               //制限時間を60秒に
        }
    }

    private void Over()
    {
        if (Input.GetKeyDown(KeyCode.Space))//ゲームオーバー画面でスペースキー押すとタイトルへ
        {
            Mode = 0;
            over.SetActive(false);
            title.SetActive(true);
        }
    }

    private void Game()
    {

        PlayTime -= Time.deltaTime;
        if (PlayTime <= 0)                  //時間が無くなったら…
        {
            snd.PlayOneShot(se_over);
            over.SetActive(true);
            PlayTime = 0;
            Mode++;
            return;
        }

        itemTime -= Time.deltaTime;
        if (itemTime <= 0)                  // アイテムの出現時間が来たか?
        {                                   // アイテムを生成
            GameObject a = item;

            int b = Random.Range(0, 3);     // 0～2
            if (b <= 1) { a = bomb; }

            var obj = Instantiate(a, new Vector3(Random.Range(-3.28f, 3.97f), 5.8f, 0), Quaternion.identity);

            ItemInterval -= 0.01f;
            if (ItemInterval <= 0.2f) { ItemInterval = 0.2f; }

            itemTime = ItemInterval;
        }

    }
}
