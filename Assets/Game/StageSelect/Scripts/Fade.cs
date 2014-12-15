using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    private const float FadeSpeed = 0.02f;       // フェードする速度
    private float LoadLevelTime;                 // シーン移動に移るまでの時間(フェード完了+1秒くらい)

    public float GetLoadLevelTime() { return LoadLevelTime; }

    void Awake()
    {
        // 透過しておく
        renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);

        // シーンへ移る時間を計算
        float time = 0f;
        int count = 0;
        while (time <= 1.0f) { time += FadeSpeed; count++; }
        LoadLevelTime = ((float)count / 60.0f) + 1.0f;
    }

    public void FadeIn()
    {
        // フィードしていく
        Color color = new Color(0f, 0f, 0f, FadeSpeed);
        renderer.material.color += color;

        // 最大値を越したら最大値を維持する
        if (renderer.material.color.a >= 1.0f)
            renderer.material.color = Color.white;
    }
}
