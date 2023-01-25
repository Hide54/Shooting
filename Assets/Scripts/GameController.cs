using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("カメラ制御スクリプトが付いたオブジェクトを設定")]
    private CameraController _cameraController = default;

    private void FixedUpdate()
    {
        //カメラをプレイヤーに追従させる
        _cameraController.gameObject.transform.position = Vector3.Lerp(
            _cameraController.transform.position, 
            _cameraController._followTarget.transform.position + _cameraController._offset, 
            Time.deltaTime * _cameraController._smoothTime);
    }
}
