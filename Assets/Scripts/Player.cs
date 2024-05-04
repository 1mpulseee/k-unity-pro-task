using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instanse;
    public Joystick joystick;
    Animator animator;
    [SerializeField] float speed = 1f;
    [SerializeField] Transform RifleStart;
    [SerializeField] ParticleSystem Fire;
    [SerializeField] GameObject Impact;
    [SerializeField] Slider HpBar;
    [SerializeField] float MaxHp;
    float Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if (value <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            HpBar.value = Hp / MaxHp;
        }
    }
    float _hp;
    public Vector2 MapSize;
    private void Awake()
    {
        instanse = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerPrefs.GetFloat("Hp");
        Hp = MaxHp;
    }
    void Update()
    {
        //float X = Input.GetAxis("Horizontal");
        //float Y = Input.GetAxis("Vertical");

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Shoot();
        //}

        float X = joystick.Direction.x;
        float Y = joystick.Direction.y;

        UpdateAnim(new Vector2(X, Y));
        Move(new Vector2(X, Y), Time.deltaTime);
    }
    void UpdateAnim(Vector2 dir)
    {
        animator.SetFloat("SpeedX", dir.x);
        animator.SetFloat("SpeedY", dir.y);
    }
    void Move(Vector3 dir, float d)
    {
        dir = dir.normalized * dir.magnitude;
        transform.position += new Vector3(dir.x, 0, dir.y) * d * speed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -MapSize.x / 2, MapSize.x / 2), 0, Mathf.Clamp(transform.position.z, -MapSize.y / 2, MapSize.y / 2));
    }
    public void Shoot()
    {
        Fire.Play();
        RaycastHit hit;
        if (Physics.Raycast(RifleStart.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Enemy")
            {
                GameObject impact = Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));
                Manager.instance.AddMoney(10);
                Destroy(impact, 3f);
                Destroy(hit.transform.gameObject);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        Hp -= damage;
    }
    public void UpgradeHp()
    {
        MaxHp += 25f;
        Hp += 25f;
        PlayerPrefs.SetFloat("Hp", MaxHp);
    }
}