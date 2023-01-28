using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("カメラ制御スクリプトが付いたオブジェクトを設定")]
    private CameraController _cameraController = default;
    [SerializeField,Header("プレイヤー制御スクリプトが付いたオブジェクトを設定")]
    private PlayerController _chara = default;

    //private void Update()
    //{
    //    _chara.MoveInput();
    //}

    private void FixedUpdate()
    {
        //プレイヤーの移動
        _chara.PlayerMove();
        ////プレイヤーの向きの変更
        //_chara.PlayerRotation();

        //カメラをプレイヤーに追従させる
        _cameraController.CameraFollow();
    }
}
