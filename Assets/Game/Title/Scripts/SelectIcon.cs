using UnityEngine;
using System.Collections;

public class SelectIcon : MonoBehaviour {
 
    private GameObject FadeImage;                // フェードイメージ
    private float LoadLevelTime;                 // シーン移動に移るまでの時間(フェード完了+1秒くらい)
    private const float FadeSpeed = 0.02f;       // フェード速度
    private bool IsFade = false;                 // フェードするフラグ

    void Awake()
    {
        // 子ノードのFadeImage(Texture)をキャプチャ
        FadeImage = GameObject.Find("FadeImage");

        // 透過しておく
        FadeImage.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);

        // シーンへ移る時間を計算
        float time = 0f;
        int count = 0;
        while (time <= 1.0f) { time += FadeSpeed; count++; }
        LoadLevelTime = ((float)count / 60.0f) + 1.0f;
    }

    void FadeIn()
    {
        // フィードしていく
        Color color = new Color(0f, 0f, 0f, FadeSpeed);
        FadeImage.renderer.material.color += color ;

        // 最大値を越したら最大値を維持する
        if (FadeImage.renderer.material.color.a >= 1.0f)
            FadeImage.renderer.material.color = Color.white;
    }

    IEnumerator Fade()
    {
        IsFade = true;

        yield return new WaitForSeconds(LoadLevelTime);
        Application.LoadLevel("StageSelect"); // シーン移動
    }

    void OnGUI()
    {
        if (!IsFade)
        {
            //HW = 120,300
            if (GUI.Button(new Rect(100, 120, 150, 25), "Start")) StartCoroutine(Fade());   // セレクトシーンへ移動
            if (GUI.Button(new Rect(100, 150, 150, 25), "Exit")) Application.Quit();        // 終了
        }
        else
            FadeIn();
    }
}
