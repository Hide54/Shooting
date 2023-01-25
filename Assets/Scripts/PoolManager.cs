using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプールを管理するスクリプト
/// </summary>
public class PoolManager : MonoBehaviour
{
    /// <summary>
    /// 定数や変数
    /// </summary>
    #region
    [SerializeField, Header("自分の弾のプレファブを設定")]
    private MyAmoController _myAmo = default;
    [SerializeField, Header("壊せる敵の弾のプレファブを設定")]
    private EnemyAmoController _enemyAmo1 = default;
    [SerializeField, Header("壊せない敵の弾のプレファブを設定")]
    private EnemyAmoController _enemyAmo2 = default;

    [SerializeField, Header("弾を生成する数")]
    private int _amoMaxCount = default;

    //生成した弾を格納する場所
    private Queue<MyAmoController> _myAmoQueue;
    private Queue<EnemyAmoController> _enemyAmo1Queue;
    private Queue<EnemyAmoController> _enemyAmo2Queue;

    //初回生成時の位置
    private Vector3 _firstPos = new Vector3(100, 100, 0);
    #endregion

    /* Queueの初期化
     * 敵の生成＆Queueへの追加
     */
    private void Awake()
    {
        _myAmoQueue = new Queue<MyAmoController>();
        _enemyAmo1Queue = new Queue<EnemyAmoController>();
        _enemyAmo2Queue = new Queue<EnemyAmoController>();

        for (int i = 0; i < _amoMaxCount; i++)
        {
            MyAmoController _tmpMyAmo = Instantiate(_myAmo, _firstPos, Quaternion.identity, transform);
            _myAmoQueue.Enqueue(_tmpMyAmo);
            EnemyAmoController _tmpEnemyAmo1 = Instantiate(_enemyAmo1, _firstPos, Quaternion.identity, transform);
            _enemyAmo1Queue.Enqueue(_tmpEnemyAmo1);
            EnemyAmoController _tmpEnemyAmo2 = Instantiate(_enemyAmo2, _firstPos, Quaternion.identity, transform);
            _enemyAmo2Queue.Enqueue(_tmpEnemyAmo2);
        }
    }

    /* 敵の貸し出し
     * Queueから敵を1つ取り出す
     * 敵を表示
     * 渡された座標に敵を移動する
     * 呼び出し元に渡す
     */
    public MyAmoController Launch(Vector3 _pos)
    {
        if (_myAmoQueue.Count <= 0) return null;

        MyAmoController _tmpMyAmo = _myAmoQueue.Dequeue();
        _tmpMyAmo.gameObject.SetActive(true);
        _tmpMyAmo.ShowInStage(_pos);
        return _tmpMyAmo;
    }

    /* 敵の回収
     * 敵を非表示
     * Queueに格納
     */
    public void MACollect(MyAmoController myAmo)
    {
        myAmo.gameObject.SetActive(false);
        _myAmoQueue.Enqueue(myAmo);
    }
    public void EACollect1(EnemyAmoController enemyAmo)
    {
        enemyAmo.gameObject.SetActive(false);
        _enemyAmo1Queue.Enqueue(enemyAmo);
    }
    public void EACollect2(EnemyAmoController enemyAmo)
    {
        enemyAmo.gameObject.SetActive(false);
        _enemyAmo2Queue.Enqueue(enemyAmo);
    }
}
