using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateItem : MonoBehaviour {

    private float interval = 0f;            // インターバル
    private float counter = 0f;             // カウンター
    private const float min = 0.1f;         // 最小値
    private const float max = 3.0f;         // 最大値
    private bool IsCreate = false;          // 生成フラグ
    private List<Vector4> SpaceList = new List<Vector4>();                // 生成される範囲リスト
    private Vector4 LeftSpace;              // 生成される範囲(左)
    private Vector4 TopSpace;               // 生成される範囲(中)
    private Vector4 RightSpace;             // 生成される範囲(右)
    private List<GameObject> FruitsList = new List<GameObject>();   // 果物リスト
    public GameObject Apple;                // リンゴ
    public GameObject Blueberry;            // ブルーベリー
    public GameObject Cherry;               // さくらんぼ
    public GameObject Grape;                // ぶどう
    public GameObject Orange;               // みかん

	void Start () 
    {
        // 次の生成までの時間取得
        interval = Random.Range(min, max);

        // 範囲を指定,コンテナへ
        // **ここではXとY,ZとWをそれぞれのVector2として考えて使用する**
        LeftSpace = new Vector4(-3f, 2f, -1f, 0f);
        TopSpace  = new Vector4(-1f, 2f, 0.8f, 1.2f);
        RightSpace= new Vector4(0.5f, 2f, 2.8f, 0.5f);
        SpaceList.Add(LeftSpace);
        SpaceList.Add(TopSpace);
        SpaceList.Add(RightSpace);

        // フルーツをコンテナへ
        FruitsList.Add(Apple);
        FruitsList.Add(Blueberry);
        FruitsList.Add(Cherry);
        FruitsList.Add(Grape);
        FruitsList.Add(Orange);
	}

    bool CreateTime()
    {
        // カウンター更新
        counter += 0.1f;

        // 更新時間なら,次の時間を受け取りカウンターをリセット
        if (interval <= counter)
        {
            interval = Random.Range(min, max);
            counter = 0f;
            return true;
        }
        else
            return false;
    }

    GameObject SelectFruits()
    {
        // ランダムにフルーツを返す
        int num = Random.Range(0, FruitsList.Count);
        GameObject fruit = FruitsList[num] ;

        return fruit;
    }

    Vector2 SelectSpace()
    {
        // ランダムで指定範囲の情報を受け取り、計算し返す
        int num = Random.Range(0, SpaceList.Count);
        Vector4 space = SpaceList[num];

        Vector2 postion;
        postion.x = Random.Range(space.z, space.x);
        postion.y = Random.Range(space.w, space.y);

        return postion;
    }
	
	void Update () 
    {
        // 時間であればフルーツを生成
        IsCreate = CreateTime();
        if (IsCreate)
        {
            GameObject fruit = SelectFruits() ;
            Vector2 postion = SelectSpace();
            Instantiate(fruit, postion, transform.rotation);
        }
	}
}
