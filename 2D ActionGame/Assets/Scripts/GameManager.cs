using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>自機のプレハブを指定する</summary>
    [SerializeField] GameObject m_playerPrefab = null;
    /// <summary>ゲームの状態</summary>
    GameState m_gameState = GameState.NonInitialized;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_gameState) //ゲームの状態によって処理を分ける
        {
            case GameState.NonInitialized:
                Debug.Log("状態遷移：NonInitialized");
                Instantiate(m_playerPrefab);    // プレイヤーを生成する
                m_gameState = GameState.Initialized;   // ステータスを初期化済みにする
                break;
            case GameState.Initialized:
                Debug.Log("状態遷移：Initialized");
                m_gameState = GameState.InGame;   // ステータスをゲーム中にする
                break;
            case GameState.PlayerDead:
                Debug.Log("状態遷移：PlayerDead");
                m_gameState = GameState.NonInitialized;
                break;
        }
    }
    public void PlayerDead()
    {
        Debug.Log("Player Dead.");
        m_gameState = GameState.PlayerDead;   // ステータスをプレイヤーがやられた状態に更新する
    }

    enum GameState
    {
        /// <summary>ゲーム初期化前</summary>
        NonInitialized,
        /// <summary>ゲーム初期化済み、ゲーム開始前</summary>
        Initialized,
        /// <summary>ゲーム中</summary>
        InGame,
        /// <summary>プレイヤーがやられた</summary>
        PlayerDead,
    }
}
