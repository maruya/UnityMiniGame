using UnityEngine;
using System.Collections;

public class FruitsMove : MonoBehaviour {

    private float interval = 0f;            // 初動に移るまでのインターバル
    private const float min = 2.0f;         // 最小値毎秒
    private const float max = 4.0f;         // 最大値毎秒
    private const float MAXSCALE = 2.0f;    // 最大スケール値
    private const float GrowUpSpeed = 0.05f;// 成長速度
    private float GrowUpSize = 0f;          // 成長値
    private bool IsGrowUp = false;          // 成長完了を判断するフラグ
    private bool Isfall = false ;           // 地面に落下したフラグ
    private const float fadeTime = 0.2f;    // フェード間隔
    private const float DestroyTime = 1.0f; // 削除する時間



	void Start () {
	
        // 各種初期化
        interval = Random.Range(min,max);
        GrowUpSize = Random.Range((MAXSCALE / 3.0f), MAXSCALE);
        rigidbody2D.Sleep();

	}

    private void GrowUpFruit()
    {
        // 拡大し、合計値を計算
        // スケールを変更(なにかしら値をいぢる)とスリープが解除されるので
        // 成長後はスリープを呼ぶ
        transform.localScale += new Vector3(GrowUpSpeed, GrowUpSpeed, GrowUpSpeed);
        float sum = transform.localScale.x + transform.localScale.y + transform.localScale.z ;
        rigidbody2D.Sleep();

        // 3はx,y,zの積を出す数値
        if (sum >= GrowUpSize * 3) IsGrowUp = true;
    }

    private IEnumerator UpdateInterval()
    {
        // 時間になったらスリープ解除
        yield return new WaitForSeconds(interval);
        rigidbody2D.WakeUp();
    }

    void FadeOut()
    {
        // チカチカさせる
        StartCoroutine(Fade());

        // 指定時間になったら自身を削除
        StartCoroutine(Delete());
    }

    private IEnumerator Fade()
    {
        // 一定間隔で呼ぶ
        while (true)
        {
            yield return new WaitForSeconds(fadeTime);
            if (renderer.material.color.a == 1.0f) renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            else renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }

	void Update () {

        // 成長したか判断
        if (IsGrowUp) StartCoroutine(UpdateInterval()); // インターバルの更新
        else GrowUpFruit(); // 成長を続ける

        // 地面に落ちたら数秒後削除
        if (Isfall) FadeOut();
	}

    void OnCollisionEnter2D(Collision2D collision2d)
    {
        // 落下判定
        if (collision2d.gameObject.tag == "Stage") Isfall = true;
    }
}
