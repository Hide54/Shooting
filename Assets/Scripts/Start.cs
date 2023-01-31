using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    private void Update()
    {
        if (Gamepad.current.buttonSouth.isPressed)
        {
            SceneManager.LoadSceneAsync("Stage1");
        }
        else if (Gamepad.current.buttonEast.isPressed)
        {
            End();
        }
    }

    // ゲームを終了する処理
    public void End()
    {
#if UNITY_EDITOR
        //デバッグモードを終了
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        //アプリを終了
        Application.Quit();
#endif
    }
}
