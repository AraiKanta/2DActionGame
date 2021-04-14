using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>エネミーの移動速度</summary>
    [SerializeField] float m_enemySpeed = 5f;
    /// <summary>接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ</summary>
    [SerializeField] float m_isGroundedLength = 1.1f;
    Rigidbody2D m_rb2d = null;
    [SerializeField] LayerMask groundLayer;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        m_rb2d.velocity = Vector2.right * m_enemySpeed;

        if (!IsGrounded())
        {
            Debug.Log("エネミー非接地");
            m_enemySpeed *= -1;
            m_isGroundedLength *= -1;
        }
    }
    public bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに地面が衝突していたら true とする
        Vector2 start = this.transform.position;   // start: オブジェクトの中心
        Vector2 end = start + Vector2.down + Vector2.right * m_isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics2D.Linecast(start, end, groundLayer); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }
}
