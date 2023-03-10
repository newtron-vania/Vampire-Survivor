using UnityEngine;

public class Knife : MonoBehaviour
{
    public Vector3 dir = new Vector3(1, 0, 0);
    public int damage = 10;
    public float speed = 10f;
    public float force = 0f;
    public int panatrate = 1;
    private int piercing = 0;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float createTime = 0f;


    private void OnEnable()
    {
        createTime = Managers.GameTime;
    }

    void FixedUpdate()
    {
        if (Managers.GameTime - createTime > lifeTime)
        {
            Managers.Resource.Destroy(gameObject);
        }
        OnMove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            
            go.GetComponent<BaseController>().OnDamaged(damage, force);
            piercing++;
            if (piercing >= panatrate)
                Managers.Resource.Destroy(gameObject);
        }
    }


    void OnMove()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
        transform.position += dir * (speed * Time.fixedDeltaTime);
    }
}