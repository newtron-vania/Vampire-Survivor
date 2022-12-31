using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    [SerializeField] Vector2 _inputVec;
    [SerializeField] public Vector2 _lastDirVec = new Vector2(1, 0);
    bool _isDamaged = false;
    float _invincibility_time = 1f;

    Slider _slider;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
        _type = Define.WorldObject.Player;
    }

    void Start()
    {
    }

    void Update()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = _inputVec.normalized * (_stat.MoveSpeed * Time.fixedDeltaTime);
        //��ġ �̵�
        _rigid.MovePosition(_rigid.position + nextVec);

        if (_inputVec.normalized.magnitude != 0)
        {
            _lastDirVec = _inputVec.normalized;
        }
    }

    private void LateUpdate()
    {
        _anime.SetFloat("speed", _inputVec.magnitude);
        if (_inputVec.x != 0)
        {
            _sprite.flipX = (_inputVec.x < 0) ? true : false;
        }
    }

    private void OnDamaged(Collision2D collision, float gameTime)
    {
        _isDamaged = true;
        Stat EnemyStat = collision.transform.GetComponent<EnemyStat>();

        Debug.Log(
            $"{collision.gameObject.name} attacked to the player. and enemyStat is {EnemyStat.Attack}");

        _stat.HP -= Mathf.Max(EnemyStat.Attack - _stat.Defense, 1);
        
        if (_stat.HP <= 0)
            OnDead();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log($"{collision.gameObject.name} was collided to the player");

            float currentTime = Managers.GameTime;
            if (!_isDamaged)
            {
                OnDamaged(collision, currentTime);
                StartCoroutine(OnDamagedColor());
            }
        }
    }

    IEnumerator OnDamagedColor()
    {
        _sprite.color = Color.red;

        yield return new WaitForSeconds(_invincibility_time);

        _isDamaged = false;
        _sprite.color = Color.white;
    }


    public override void OnDead()
    {
        _stat.HP = 0;
    }
}