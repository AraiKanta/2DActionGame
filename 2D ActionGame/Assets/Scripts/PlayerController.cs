using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_speed = 5f;
    /// <summary>ジャンプの強さ</summary>
    [SerializeField] float m_jumpPower = 5f;
    /// <summary>ジャンプできる回数</summary>
    [SerializeField] int m_jumpMaxCount = 2;
    /// <summary>ジャンプした回数</summary>
    int m_jumpCount = 0;
    /// <summary>接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ</summary>
    [SerializeField] float m_isGroundedLength = 1.1f;
    Rigidbody2D m_rb2d = null;
    [SerializeField]LayerMask groundLayer;
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_jumpCount = 0;
    }

    void Update()
    {
        // 水平方向の入力を検出する
        float h = Input.GetAxisRaw("Horizontal");
        //// 入力に応じてプレイヤーを水平方向に動かす
        //m_rb2d.velocity = h * Vector2.right * m_speed;
        // 入力方向のベクトルを組み立てる
        Vector2 dir = Vector2.right * h;
        if (dir == Vector2.zero)
        {
            // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            m_rb2d.velocity = new Vector2(0f, m_rb2d.velocity.y);
        }
        Vector2 velo = dir.normalized * m_speed; // 入力した方向に移動する
        velo.y = m_rb2d.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
        m_rb2d.velocity = velo;   // 計算した速度ベクトルをセットする

        if (Input.GetButtonDown("Jump"))
        {
            if (m_jumpCount < m_jumpMaxCount)
            {
                m_rb2d.AddForce(Vector2.up * m_jumpPower,ForceMode2D.Impulse);
                m_jumpCount++;
            }
        }
        if (IsGrounded())
        {
            Debug.Log("接地した");
            m_jumpCount = 0;
        }
    }
    public bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに地面が衝突していたら true とする
        Vector2 start = this.transform.position;   // start: オブジェクトの中心
        Vector2 end = start + Vector2.down * m_isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics2D.Linecast(start,end,groundLayer); // 引いたラインに何かがぶつかっていたら true とする(8はステージ)
        return isGrounded;
    }
}
