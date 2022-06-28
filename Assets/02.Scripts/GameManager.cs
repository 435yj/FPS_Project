using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int barrelCount = -1;

    private float time = 0;

    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text pazeText;

    [SerializeField]
    private GameObject monsterPrefab;
    // ������ ���� ����

    private bool isGameOver;
    // ���� ���� ����

    public int monsterHP = 10;

    public float createTime = 3.0f;
    // ���� ���� ����
    public int maxMonsterCount = 50;
    // ���� �ִ� ���� ����

    public List<Transform> points = new List<Transform>();
    // ���� �⿬ ��ġ ����
    public List<GameObject> monsterPool = new List<GameObject>();
    // ���͸� �̸� ����-����

    public float turnSpeed = 80.0f;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value;
            if (isGameOver)
                CancelInvoke("CreateMonster");
        }
    }

    private static GameManager instance;

    public static GameManager Instance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<GameManager>();
            
            if (instance == null)
            {
                GameObject container = new GameObject("GameManager");

                instance = container.AddComponent<GameManager>();
            }
        }
     
        return instance;
    }

    private void Awake()
    {
        time = 0;

        if (instance == null)
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // ���� ������Ʈ Ǯ ����
        CreateMonsterPool();

        Transform MonsterSpawns = GameObject.Find("Spawn")?.transform;

        foreach (Transform item in MonsterSpawns)
        {
            points.Add(item);
        }

        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    private void Update()
    {
        DisplayTime(time += Time.deltaTime); 
        
        if (time <= 70)
        {
            monsterHP = 10;
            pazeText.text = "PAZE - 1";
        }
        else if (time > 70 && time <= 140)
        {
            monsterHP = 30;
            pazeText.text = "PAZE - 2";
        }
        else
        {
            monsterHP = 50;
            pazeText.text = "PAZE - 3";
        }

        if (isGameOver)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);

        GameObject monster = GetMonsterInPool();
        // ������Ʈ Ǯ���� ���� ����

        monster.transform.parent = points[idx].transform;
        monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
        // ���� ��ġ-ȸ�� ����
        monster?.SetActive(true);
    }
    
    void CreateMonsterPool()
    {
        for(int i = 0; i < maxMonsterCount; i++)
        {
            var monster = Instantiate<GameObject>(monsterPrefab);

            monster.name = $"Monster_{i:00}";
            // ���� �̸� ����
            monster.SetActive(false);
            // ���� ���� ����
            monsterPool.Add(monster);
            // ������Ʈ Ǯ�� �߰�
        }
    }

    public GameObject GetMonsterInPool()
    {
        foreach(var monster in monsterPool)
        {
            if (monster.activeSelf == false)
                return monster;
        }
        return null;
    }

    public void DisplayTime(float time)
    {
        timeText.text = Mathf.Ceil(time).ToString();
    }
}
