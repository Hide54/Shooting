using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("カメラ制御スクリプトが付いたオブジェクトを設定")]
    private CameraController _cameraController = default;
    [SerializeField, Header("プレイヤー制御スクリプトが付いたオブジェクトを設定")]
    private PlayerController _chara = default;
    [SerializeField, Header("コア制御スクリプトが付いたオブジェクトを設定")]
    private CoreController _boss = default;
    [SerializeField, Header("オーディオソースを設定")]
    private AudioSource _audio = default;
    [SerializeField, Header("ゲームクリア、オーバー時に鳴らすSEを設定")]
    private AudioClip[] _clips = default;
    [SerializeField, Header("ゲームクリア時のUI")]
    private GameObject _clearUI = default;
    [SerializeField,Header("ゲームクリア時のUI")]
    private GameObject _deathUI = default;

    [Header("ーーーーーいじらないーーーーー")]
    [SerializeField, Header("プレイヤーを設定")]
    private GameObject _player = default;
    [SerializeField, Header("ボスを設定")]
    private GameObject _core = default;

    private float _endTime = 2f;


    private void Awake()
    {
        Time.timeScale = 1;
        _clearUI.SetActive(false);
        _deathUI.SetActive(false);
        _player = GameObject.FindGameObjectWithTag("Player");
        _core = GameObject.FindGameObjectWithTag("Core");
    }

    private void Update()
    {
        _chara.MoveInput();
        if (_player.activeSelf == false)
        {
            _audio.PlayOneShot(_clips[1]);
            _deathUI.SetActive(true);
            StartCoroutine(Death());
        }
        if (_core.activeSelf == false)
        {
            _audio.PlayOneShot(_clips[0]);
            _clearUI.SetActive(true);
            StartCoroutine(Clear());
        }
    }

    private void FixedUpdate()
    {
        //プレイヤーの移動
        _chara.PlayerMove();
        //プレイヤーの向きの変更
        _chara.PlayerRotation();

        //カメラをプレイヤーに追従させる
        _cameraController.CameraFollow();

        //ボスの移動
        _boss.Move(_player);
    }

    public IEnumerator Clear()
    {
        yield return null;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(_endTime);
        SceneManager.LoadSceneAsync("Title");
    }

    public IEnumerator Death()
    {
        yield return null;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(_endTime);
        SceneManager.LoadSceneAsync("Title");
    }
}
