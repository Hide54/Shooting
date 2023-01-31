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

    // �Q�[�����I�����鏈��
    public void End()
    {
#if UNITY_EDITOR
        //�f�o�b�O���[�h���I��
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        //�A�v�����I��
        Application.Quit();
#endif
    }
}
