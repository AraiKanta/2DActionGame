using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    /// <summary>生成する敵のプレハブ</summary>
    [SerializeField] GameObject m_enemyPrefab = null;
    /// <summary>敵を生成する位置として設定するオブジェクト</summary>
    [SerializeField] Transform m_spawnPoint = null;
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "GameZone")
        {
            Debug.Log("生成器とゲームゾーンの接触");
            GameObject go = Instantiate(m_enemyPrefab);
            go.transform.position = m_spawnPoint.position;
        }
    }
}
