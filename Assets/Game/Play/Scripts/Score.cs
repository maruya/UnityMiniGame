using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    // GUIの表示
    [ExecuteInEditMode()]

    //private GameObject camera;
    public GameObject text; // 表示させる数値を持つテキスト
    public int score = 0;   // スコア本体

	void Start () {

        // カメラをキャプチャ
        //camera = GameObject.Find("Main Camera") as GameObject;
	}

    // ポイントを更新する関数
    // このスクリプトを呼び出す他のスクリプトから参照する関数
    // ToDO 位置の算出がゴリ押し
    public void Point(Vector3 pos, int point)
    {
        Vector3 text_pos = pos;
        float X = 9.5f;
        float Y = 4.8f;

        // 負の値であれば正の値へ、正の値であれば2倍する
        if (text_pos.x <= 0f) text_pos.x = Mathf.Abs(text_pos.x);
        else text_pos.x *= 2;
        if (text_pos.y <= 0f) text_pos.y = Mathf.Abs(text_pos.y);
        else text_pos.y *= 2;

        // 比率を合わせる
        text_pos.x = text_pos.x / X;
        text_pos.y = text_pos.y / Y;
        //text_pos.x = X / transform.position.x;
        //text_pos.y = Y / transform.position.y;

        // スコアの更新
        score += point;

        // ここで書き換え
        text.guiText.text = "" + score;

        // ToDO 全体的に書き方がよくわからない
        // インスタンスの生成
        GameObject draw = GameObject.Instantiate(text) as GameObject;

        // ToDO とりあえずzero
        draw.transform.position = text_pos;
    }
}
