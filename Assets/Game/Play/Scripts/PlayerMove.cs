using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    private bool IsRight = true;            // キャラクターの向き
    private float Accelerate = 0f;          // 加速
    private const float MaxSpeed = 10.0f;    // 限界速度
    private const float Speed = 8.0f;       // 移動する力
    private const float MaxAccele = 1.0f;   // 限界加速度
    private const float Acceleration = 0.1f;// 加速度

	void Start () 
    {
	}
	
	void Update () 
    {
        // 加速と制限
        if (Input.GetKey("left") || Input.GetKey("right"))
        {
            Accelerate += Acceleration;
            if (Accelerate >= MaxAccele) Accelerate = MaxAccele;
        }
        else Accelerate = 0f;
	}

    void FixedUpdate()
    {
        // 速度制限
        float h = Input.GetAxis("Horizontal");
        if (rigidbody2D.velocity.x < MaxSpeed) rigidbody2D.AddForce(Vector2.right * h * (Speed + Accelerate));
        
        // キャラクターの向きを修正
        if ((h > 0 && !IsRight) || (h < 0 && IsRight))
        {
            IsRight = (h > 0);
            float scaleX = Mathf.Abs(transform.localScale.x);
            this.transform.localScale = new Vector3((IsRight ? scaleX : -scaleX), transform.localScale.y, transform.localScale.z);
        }
    }
}
