using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {

    private const float deleteTime = 2.0f;       // 削除するまでの時間
    private const float UpSpeed = 0.005f;         // テキストの上昇速度


    private IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(deleteTime);
        Destroy(gameObject);
    }
	
	void Update () {

        // 上昇しつつ、指定時間に自身を削除
        transform.position += Vector3.up * UpSpeed;
        StartCoroutine(DeleteText());

	}
}
