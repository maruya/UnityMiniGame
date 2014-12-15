using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasketMove : MonoBehaviour {

    private List<GameObject> FruitsList = new List<GameObject>();   // フルーツコンテナ
    public Score score = null;

    void Awake()
    {
        // プレイシーンからリザルトシーンに引継ぎ
        DontDestroyOnLoad(this);
    }

	void Start () {

        // ToDO 自身が持つ"スコア"スクリプトを取得
        score = GameObject.Find("basket").GetComponent<Score>();
	
	}
	
	void Update () {
	
	}


    void DrawScore()
    {
        // テスト、ポイントの更新
        score.Point(transform.position, 10);
    }


    void OnCollisionEnter2D(Collision2D collision2d)
    {
        // アイテムタグにヒットしたら格納し、ヒットしたオブジェクトを削除
        if (collision2d.gameObject.tag == "Item")
        {
            FruitsList.Add(collision2d.gameObject);
            Destroy(collision2d.gameObject);
            // test
            DrawScore();
        }
    }
}
