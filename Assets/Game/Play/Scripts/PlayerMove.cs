using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

// AddForce変数
#if(false)
    private bool IsRight = true;            // キャラクターの向き
    private float Accelerate = 0f;          // 加速
    private const float MaxSpeed = 10.0f;    // 限界速度
    private const float Speed = 2.0f;       // 移動する力
    private const float MaxAccele = 1.0f;   // 限界加速度
    private const float Acceleration = 0.1f;// 加速度
#endif

    private float speed = 0f;               // 実際に代入する速度
    private const float Speed = 2.0f;       // 移動する力
    private bool IsRight = true;            // キャラクターの向き
    private int groundlLayer;               // 接地判定に使用するレイヤー
    private const float JumpPower = 200.0f; // ジャンプ力

	void Start () 
    {
        groundlLayer = 1 << LayerMask.NameToLayer("Ground");
	}


    void Update()
    {
        // 移動
        if (Input.GetKey("left")) speed = -Speed;
        else if (Input.GetKey("right")) speed = Speed;
        else speed = 0f;
    }

    void FixedUpdate()
    {
        // 接地しているときにジャンプ可能
        if (Physics2D.Linecast(transform.position, transform.position - transform.up / 2, groundlLayer))
        {
            if( Input.GetButtonDown("Jump")) rigidbody2D.AddForce(new Vector2(0f, JumpPower));
        }

        // 速度制限
        float h = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);

        // キャラクターの向きを修正
        if ((h > 0 && !IsRight) || (h < 0 && IsRight))
        {
            IsRight = (h > 0);
            float scaleX = Mathf.Abs(transform.localScale.x);
            this.transform.localScale = new Vector3((IsRight ? scaleX : -scaleX), transform.localScale.y, transform.localScale.z);
        }
    }

/// 以下、AddForceでの挙動
#if(false)

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

#endif
}
