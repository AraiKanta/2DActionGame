using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>自機のプレハブを指定する</summary>
    [SerializeField] GameObject m_playerPrefab = null;
    /// <summary>ゲームの状態</summary>
    GameState m_gameState = GameState.NonInitialized;
    [SerializeField] Transform m_playerSpawnPoint;
    public GameObject panel;


    // Update is called once per frame
    void Update()
    {
        switch (m_gameState) //ゲームの状態によって処理を分ける
        {
            case GameState.NonInitialized:
                Debug.Log("状態遷移：NonInitialized");
                GameObject go = Instantiate(m_playerPrefab);    // 指定の位置にプレイヤーを生成する
                go.transform.position = m_playerSpawnPoint.position;
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
            case GameState.Finished:
                Debug.Log("状態遷移：Finished");
                SceneManager.LoadScene("Title");
                break;
        }
    }
    public void PlayerDead()
    {
        Debug.Log("Player Dead.");
        m_gameState = GameState.PlayerDead;   // ステータスをプレイヤーがやられた状態に更新する
    }
    public void Finished()
    {
        Debug.Log("Goal!");
        m_gameState = GameState.Finished;   // ステータスをゴールした状態に更新する

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
        /// <summary>ゴールした</summary>
        Finished,
    }
}
