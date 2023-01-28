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
    private MyAmmoController _myAmmo = default;
    [SerializeField, Header("壊せる敵の弾のプレファブを設定")]
    private EnemyAmmo1 _enemyAmmo1 = default;
    [SerializeField, Header("壊せない敵の弾のプレファブを設定")]
    private EnemyAmmo2 _enemyAmmo2 = default;
    [SerializeField, Header("対応するスクリプトを設定")]
    private PlayerController _chara = default;

    [SerializeField, Header("自分の弾を生成する数")]
    private int _myAmmoMaxCount = default;
    [SerializeField, Header("敵の弾を生成する数")]
    private int _enemyAmmoMaxCount = default;

    //生成した弾を格納する場所
    private Queue<MyAmmoController> _myAmmoQueue;
    private Queue<EnemyAmmo1> _enemyAmmo1Queue;
    private Queue<EnemyAmmo2> _enemyAmmo2Queue;

    //初回生成時の位置
    private Vector3 _firstPos = new Vector3(100, 100, 0);
    #endregion

    /* Queueの初期化
     * 弾の生成＆Queueへの追加
     */
    private void Awake()
    {
        _myAmmoQueue = new Queue<MyAmmoController>();
        _enemyAmmo1Queue = new Queue<EnemyAmmo1>();
        _enemyAmmo2Queue = new Queue<EnemyAmmo2>();

        //プレイヤーの弾を生成してQueueに追加する
        for (int i = 0; i < _myAmmoMaxCount; i++)
        {
            MyAmmoController _tmpMyAmmo = Instantiate(_myAmmo, _firstPos, Quaternion.identity, this.transform);
            MyAmmoEnabledFalse(_tmpMyAmmo);
            _myAmmoQueue.Enqueue(_tmpMyAmmo);
        }

        //敵の弾を生成してQueueに追加する
        for (int i = 0; i < _enemyAmmoMaxCount; i++)
        {
            //壊せる弾
            EnemyAmmo1 _tmpEnemyAmmo1 = Instantiate(_enemyAmmo1, _firstPos, Quaternion.identity, this.transform);
            EnemyAmmo1EnabledFalse(_tmpEnemyAmmo1);
            _enemyAmmo1Queue.Enqueue(_tmpEnemyAmmo1);

            //壊せない弾
            EnemyAmmo2 _tmpEnemyAmmo2 = Instantiate(_enemyAmmo2, _firstPos, Quaternion.identity, this.transform);
            EnemyAmmo2EnabledFalse(_tmpEnemyAmmo2);
            _enemyAmmo2Queue.Enqueue(_tmpEnemyAmmo2);
        }
    }



    // プレイヤーの弾の貸し出し
    public MyAmmoController MyAmmoLaunch(Vector3 tmpPos, Quaternion tmpRot)
    {
        //Queueに何もなければ空を返す
        if (_myAmmoQueue.Count <= 0) return null;

        //Queueから弾を1つ取り出す
        MyAmmoController _tmpMyAmmo = _myAmmoQueue.Dequeue();

        //弾を表示する
        MyAmmoEnabledTrue(_tmpMyAmmo);

        //渡された座標に弾を移動する
        _tmpMyAmmo.Init(tmpPos, tmpRot);
        return _tmpMyAmmo;
    }

    //壊せる敵の弾の貸し出し
    public EnemyAmmo1 EnemyAmmoLaunch1(Vector3 tmpPos, Quaternion tmpRot)
    {
        //Queueに何もなければ空を返す
        if (_enemyAmmo1Queue.Count <= 0) return null;

        //Queueから弾を1つ取り出す
        EnemyAmmo1 _tmpEnemyAmmo1 = _enemyAmmo1Queue.Dequeue();

        //弾を表示する
        EnemyAmmo1EnabledTrue(_tmpEnemyAmmo1);

        //渡された座標に弾を移動する
        _tmpEnemyAmmo1.Init(tmpPos, tmpRot);
        return _tmpEnemyAmmo1;
    }

    //壊せない敵の弾の貸し出し
    public EnemyAmmo2 EnemyAmmoLaunch2(Vector3 tmpPos, Quaternion tmpRot)
    {
        //Queueに何もなければ空を返す
        if (_enemyAmmo2Queue.Count <= 0) return null;

        //Queueから弾を1つ取り出す
        EnemyAmmo2 _tmpEnemyAmmo2 = _enemyAmmo2Queue.Dequeue();

        //弾を表示する
        EnemyAmmo2EnabledTrue(_tmpEnemyAmmo2);

        //渡された座標に弾を移動する
        _tmpEnemyAmmo2.Init(tmpPos, tmpRot);
        return _tmpEnemyAmmo2;
    }



    //プレイヤーの弾の回収処理
    public void MACollect(MyAmmoController tmpMyAmmo)
    {
        MyAmmoEnabledFalse(tmpMyAmmo);
        tmpMyAmmo.transform.position = _firstPos;
        _myAmmoQueue.Enqueue(tmpMyAmmo);
    }

    //壊せる敵の弾の回収処理
    public void EACollect1(EnemyAmmo1 tmpEnemyAmmo1)
    {
        EnemyAmmo1EnabledFalse(tmpEnemyAmmo1);
        tmpEnemyAmmo1.transform.position = _firstPos;
        _enemyAmmo1Queue.Enqueue(tmpEnemyAmmo1);
    }

    //壊せない敵の弾の回収処理
    public void EACollect2(EnemyAmmo2 tmpEnemyAmmo2)
    {
        EnemyAmmo2EnabledFalse(tmpEnemyAmmo2);
        tmpEnemyAmmo2.transform.position = _firstPos;
        _enemyAmmo2Queue.Enqueue(tmpEnemyAmmo2);
    }



    //プレイヤーの弾を表示する処理
    public void MyAmmoEnabledTrue(MyAmmoController myAmmo)
    {
        //メッシュとスクリプトのコンポーネントを取得
        MeshRenderer _mesh = myAmmo.GetComponent<MeshRenderer>();
        MyAmmoController _ammoController = myAmmo.GetComponent<MyAmmoController>();

        //取得したコンポーネントを有効化
        _mesh.enabled = true;
        _ammoController.enabled = true;
    }

    //壊せる敵の弾を表示する処理
    public void EnemyAmmo1EnabledTrue(EnemyAmmo1 enemyAmmo1)
    {
        //メッシュとスクリプトのコンポーネントを取得
        MeshRenderer _mesh = enemyAmmo1.GetComponent<MeshRenderer>();
        EnemyAmmo1 _ammoController = enemyAmmo1.GetComponent<EnemyAmmo1>();

        //取得したコンポーネントを有効化
        _mesh.enabled = true;
        _ammoController.enabled = true;
    }

    //壊せない敵の弾を表示する処理
    public void EnemyAmmo2EnabledTrue(EnemyAmmo2 enemyAmmo2)
    {
        //メッシュとスクリプトのコンポーネントを取得
        MeshRenderer _mesh = enemyAmmo2.GetComponent<MeshRenderer>();
        EnemyAmmo2 _ammoController = enemyAmmo2.GetComponent<EnemyAmmo2>();

        //取得したコンポーネントを有効化
        _mesh.enabled = true;
        _ammoController.enabled = true;
    }



    //プレイヤーの弾を非表示にする処理
    public void MyAmmoEnabledFalse(MyAmmoController myAmmo)
    {
        //メッシュとスクリプトのコンポーネントを取得
        MeshRenderer _mesh = myAmmo.GetComponent<MeshRenderer>();
        MyAmmoController _ammoController = myAmmo.GetComponent<MyAmmoController>();

        //取得したコンポーネントを無効化
        _mesh.enabled = false;
        _ammoController.enabled = false;
    }

    //壊せる敵の弾を非表示にする処理
    public void EnemyAmmo1EnabledFalse(EnemyAmmo1 enemyAmmo1)
    {
        //メッシュとスクリプトのコンポーネントを取得
        MeshRenderer _mesh = enemyAmmo1.GetComponent<MeshRenderer>();
        EnemyAmmo1 _ammoController = enemyAmmo1.GetComponent<EnemyAmmo1>();

        //取得したコンポーネントを無効化
        _mesh.enabled = false;
        _ammoController.enabled = false;
    }

    //壊せない敵の弾を非表示にする処理
    public void EnemyAmmo2EnabledFalse(EnemyAmmo2 enemyAmmo2)
    {
        //メッシュとスクリプトのコンポーネントを取得
        MeshRenderer _mesh = enemyAmmo2.GetComponent<MeshRenderer>();
        EnemyAmmo2 _ammoController = enemyAmmo2.GetComponent<EnemyAmmo2>();

        //取得したコンポーネントを無効化
        _mesh.enabled = false;
        _ammoController.enabled = false;
    }
}
