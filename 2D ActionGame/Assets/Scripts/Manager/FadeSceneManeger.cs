using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーン遷移時のフェードイン、アウトを制御するためのクラス
/// </summary>
public class FadeSceneManeger : SingletonMonoBehaviour<FadeSceneManeger>
{


    private static FadeSceneManeger instance;

    public static FadeSceneManeger Instance 
    {
        get 
        {
            if (instance == null)
            {
                instance = (FadeSceneManeger)FindObjectOfType(typeof(FadeSceneManeger));

                if (instance == null)
                {
                    Debug.LogError(typeof(FadeSceneManeger) + "is noting");
                }
            }

            return instance;
        }
    }

    /// <summary> デバッグ </summary>
    public bool DebugMode = true;
    /// <summary> フェード中の透明度 </summary>
    private float fadeAlpha = 0;
    /// <summary> フェード中かどうか </summary>
    private bool isFading = false;
    /// <summary> フェード中かどうか </summary>
    public Color fadeColor = Color.black;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGUI()
    {
        //Fade
        if (this.isFading)
        {
            //色と透明度を更新して白テクスチャを描画
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),Texture2D.whiteTexture);
        }

        if (this.DebugMode)
        {
            if (!this.isFading)
            {
                List<string> scenes = new List<string>();
                scenes.Add("SampleSecne");

                if (scenes.Count == 0)
                {
                    GUI.Box(new Rect(10, 10, 200, 50), "Fade Manager(Debug Mode)");
                    GUI.Label(new Rect(20, 30, 280, 20), "Curreent not found.");
                    return;
                }

                GUI.Box(new Rect(10, 10, 300, 50 + scenes.Count * 25), "Fade Manager(Debug Mode)");
                GUI.Label(new Rect(20, 30, 280, 20), "Curreent not found.");

                int i = 0;
                foreach (var sceneName in scenes)
                {
                    if (GUI.Button(new Rect(20, 55 + i * 25, 100, 20), "Load Level"))
                    {
                        LoadScene(sceneName, 1.0f);
                    }
                    GUI.Label(new Rect(125, 55 + i * 25, 1000, 20), sceneName);
                    i++;
                }
            } 
        }
    }

    /// <summary>
    /// 画面遷移
    /// </summary>
    /// <param name="scene">　シーン名　</param>
    /// <param name="intarval">　暗転に掛かる時間　</param>
    public void LoadScene(string scene, float intarval)
    {
        StartCoroutine(TrancsScene(scene, intarval));
    }

    /// <summary>
    /// シーン遷移のコルーチン
    /// </summary>
    /// <param name="scene">　シーン名　</param>
    /// <param name="intarval">　暗転にかかる時間　</param>
    public IEnumerator TrancsScene(string scene,float intarval) 
    {
        this.isFading = true;
        float time = 0;
        while (time <= intarval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / intarval);
            time += Time.deltaTime;
            yield return 0;
        }

        SceneManager.LoadScene(scene);

        time = 0;
        while (time <= intarval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / intarval);
            time += Time.deltaTime;
            yield return 0;
        }
        this.isFading = false;
    }
}
