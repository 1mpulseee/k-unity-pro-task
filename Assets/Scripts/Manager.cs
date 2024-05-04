using System.Collections;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public GameObject PauseUi;
    [SerializeField] GameObject Floor;
    [SerializeField] GameObject FloorEnemy;
    [SerializeField] GameObject Enemy;
    [SerializeField] TMP_Text MoneyText;
    bool isPause = false;
    float Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            MoneyText.text = "Money: " + value.ToString("0.00");
            PlayerPrefs.SetFloat("Money", value);
        }
    }
    float _money;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Floor.transform.localScale = new Vector3(Player.instanse.MapSize.x + 1, 1, Player.instanse.MapSize.y + 1);
        FloorEnemy.transform.position = Floor.transform.position + new Vector3(0, 0, Player.instanse.MapSize.y * 1.5f + 1);
        FloorEnemy.transform.localScale = new Vector3(Player.instanse.MapSize.x + 1, 1, Player.instanse.MapSize.y * 2 + 1);

        Money = PlayerPrefs.GetFloat("Money");

        PauseUi.SetActive(false);

        StartCoroutine(EnemySpawner());
    }
    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            Instantiate(Enemy, new Vector3(Random.Range(-Player.instanse.MapSize.x / 2, Player.instanse.MapSize.x / 2), 1.5f, Player.instanse.MapSize.y * 2.5f), Quaternion.identity);
        }
    }
    public void AddMoney(float value)
    {
        Money += value;
    }
    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0f;
            PauseUi.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            PauseUi.SetActive(false);
        }
    }
    public bool Buy(float cost)
    {
        if (Money >= cost)
        {
            Money -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }
}