using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public GameObject _player;
    public static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    GameManagerEx _game = new GameManagerEx();

    public static GameManagerEx Game { get { return s_instance._game; } }
    #endregion

    #region core
    DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    SoundManager _sound = new SoundManager();

    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SoundManager Sound { get { return Instance._sound; } }

    #endregion

    public static float GameTime { get; set; } = 0;


    void Awake()
    {
        Init();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    static void Init()
    {
        if (s_instance == null)
        {

            //�Ŵ��� �ʱ�ȭ
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<Managers>();
            }
            //�������� �ʰԲ� ���� -> Scene �̵��� �ϴ��� �ı����� ����
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._sound.Init();
            s_instance._pool.Init();
            s_instance._data.Init();
        }
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }
    public static void Clear()
    {
        Sound.Clear();
        UI.Clear();
        Pool.Clear();
    }



}