using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconMove : MonoBehaviour
{

    // 四季のどれかとIconの座標を持つ構造体
    struct SelectInformation
    {
        public string Season;
        public Vector3 Pos;

        // コンストラクタ
        public SelectInformation(string name, Vector3 pos)
        {
            Season = name;
            Pos = pos;
        }
    }

    private Fade FadeImage;                    // フェードイメージ
    private SelectInformation SelectInfo;            // 四季とIcon座標
    private List<SelectInformation> InformationList = new List<SelectInformation>();    // 四季と座標の情報コンテナ
    private const int UpdataSpeed = 5;               // Iconの更新速度
    private int Timer = 0;                           // タイマー
    private bool IsPushButton = false;               // 選択されたかのフラグ


    void Start()
    {
        // 子ノードからFadeクスリプトをキャプチャ
        FadeImage = GameObject.Find("FadeImage").GetComponent<Fade>();

        // 四季の情報を作成
        CreateInformation();
    }

    void CreateInformation()
    {
        InformationList.Add(new SelectInformation("Spring", new Vector3(-4.27f, 2.4f, 0f)));    // 春
        InformationList.Add(new SelectInformation("Summer", new Vector3(0f, 2.4f, 0f)));        // 夏
        InformationList.Add(new SelectInformation("Fall", new Vector3(-4.27f, 0f, 0f)));        // 秋
        InformationList.Add(new SelectInformation("Winter", new Vector3(0f, 0f, 0f)));          // 冬
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(FadeImage.GetLoadLevelTime());
        Application.LoadLevel("Play");
    }

    void Update()
    {
        if (!IsPushButton)
        {
            Timer++;
            if (Timer % UpdataSpeed == 0)
            {
                // ランダムでコンテナを取り出し、渡す
                int num = Random.Range(0, InformationList.Count);
                SelectInfo = InformationList[num];

                // アイコンの座標更新と次の更新時間の更新
                transform.position = InformationList[num].Pos;
            }
        }
        else
            FadeImage.FadeIn();

    }

    void OnGUI()
    {
        if (!IsPushButton)
        {
            // 押されたら選択されている四季を記録してフラグを立てる
            if (GUI.Button(new Rect(100, 80, 120, 25), "Push"))
            {
                PlayerPrefs.SetString(SelectInfo.Season, "Season");
                IsPushButton = true;
                StartCoroutine(NextScene());
            }
        }
    }
}
