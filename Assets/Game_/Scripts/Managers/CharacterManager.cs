using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] int onField;
    [SerializeField] int maxEnemy;
    [SerializeField] Player playerPrefab;
    Player player;
    public Player Player => player;

    public int remainEnemyCount { get; private set; }

    int spawnCount;

    bool isWaiting = false;
    float delay = 3f;
    float startDelay;

    public UILevel UILevel;

    Pool enemyPool;

    Booster booster;
    [SerializeField] Booster boosterPref;

    public bool isRevive { get; private set; }

    private void Update()
    {
        if (isWaiting)
        {
            if (Time.time - startDelay >= delay)
            {
                isWaiting = false;
            }
        }

        if (player != null)
        {
            if (GameManager.IsState(GameState.Weapon))
            {
                player.gameObject.SetActive(false);
            }
            else
            {
                player.gameObject.SetActive(true);
            }
        }

        if (remainEnemyCount <= 0 && GameManager.IsState(GameState.GamePlay))
        {
            player.Victory();
        }

        if (booster == null)
        {
            booster = Instantiate(boosterPref, GetRandomPos() + Vector3.up * 5f, Quaternion.identity);
            booster.OnInit();
        }

    }

    public void OnInit()
    {
        isRevive = false;

        if (player == null)
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        player.OnInit();
        UILevel.AddTargetIndicator(player);

        spawnCount = 0;
        remainEnemyCount = maxEnemy;

        for (int i = 0; i < onField; i++)
        {
            SpawnFromPool();
        }
        Enemy.OnDeadEvent += SpawnFromPool;

        UILevel.UpdateAllIndicators();
        TargetIndicator.OnIndicatorChange += UILevel.OnIndicatorStateChanged;
    }

    public void OnPlay()
    {
        player.OnPlay();

        for (int i = 0; i < enemyPool.Active.Count; i++)
        {
            Enemy e = (Enemy)enemyPool.Active[i];
            e.OnPlay();
        }
    }

    public void RevivePlayer()
    {
        isRevive = true;

        bool isValid = true;
        Vector3 pos = Vector3.zero;
        do
        {
            isValid = true;
            pos = GetRandomPos();
            foreach (GameUnit unit in enemyPool.Active)
            {
                Enemy e = (Enemy)unit;
                if (Vector3.Distance(pos, e.transform.position) < e.AttackRange + 1)
                {
                    isValid = false;
                    break;
                }
            }
        }
        while (isValid == false);

        if (isValid)
        {
            player.Revive(pos);
        }
    }

    public void OnDespawn()
    {
        Destroy(player.gameObject);
        player = null;

        MiniPool.CollectAll();
        Enemy.OnDeadEvent -= SpawnFromPool;
        TargetIndicator.OnIndicatorChange -= UILevel.OnIndicatorStateChanged;
    }

    public void SetPool()
    {
        enemyPool = MiniPool.GetPool<Pool>(PoolType.Enemy);
        UILevel.SetPool();
    }

    public Vector3 GetRandomPos()
    {
        float x1 = -14, x2 = 14, z1 = 11, z2 = -11;

        float x = Random.Range(x1, x2);
        float z = Random.Range(z1, z2);

        return new Vector3(x, 0, z);
    }

    private void SpawnFromPool()
    {
        bool isValid = true;
        int count = 0;
        Vector3 pos = Vector3.zero;
        do
        {
            if (isWaiting)
            {
                if (Time.time - startDelay >= delay)
                {
                    isWaiting = false;
                    startDelay = Time.time;
                }
                continue;
            }

            count++;
            isValid = true;
            pos = GetRandomPos();
            foreach (GameUnit unit in enemyPool.Active)
            {
                Enemy e = (Enemy)unit;
                if (Vector3.Distance(pos, e.transform.position) < e.AttackRange + 1)
                {
                    isValid = false;
                    break;
                }
            }

            if (Vector3.Distance(pos, player.transform.position) < player.AttackRange + 3)
            {
                isValid = false;
            }

            if (count >= 10000)
            {
                startDelay = Time.time;
                isWaiting = true;
                return;
            }
        }
        while (isValid == false && count < 100000);

        if (isValid)
        {
            Enemy e = MiniPool.Spawn<Enemy>(PoolType.Enemy, pos, Quaternion.identity);

            int rnd = Random.Range(0, 3);
            e.OnInit();
            e.InitLevel(rnd);

            UILevel.AddTargetIndicator(e);

            spawnCount++;
        }
    }

    private void SpawnFromPool(Enemy enemy)
    {
        remainEnemyCount--;

        bool isValid = true;

        DespawnFromPool(enemy);

        if (remainEnemyCount <= 0) { return; }

        if (isWaiting) return;

        int count = 0;
        Vector3 pos = Vector3.zero;
        do
        {
            count++;
            isValid = true;
            pos = GetRandomPos();
            foreach (GameUnit unit in enemyPool.Active)
            {
                Enemy e = (Enemy)unit;
                if (Vector3.Distance(pos, e.transform.position) < e.AttackRange + 1)
                {
                    isValid = false;
                    break;
                }
            }

            if (Vector3.Distance(pos, player.transform.position) < player.AttackRange + 3)
            {
                isValid = false;
            }

            if (count >= 10000)
            {
                startDelay = Time.time;
                isWaiting = true;
                return;
            }
        }
        while (isValid == false && count < 100000);

        if (isValid)
        {
            Enemy e = MiniPool.Spawn<Enemy>(PoolType.Enemy, pos, Quaternion.identity);
            e.OnInit();

            if (spawnCount <= 30)
            {
                int rnd = Random.Range(0, 3);
                e.InitLevel(rnd);
            }
            else if (spawnCount <= 40)
            {
                int rnd = Random.Range(4, 7);
                e.InitLevel(rnd);
            }
            else if (spawnCount <= 70)
            {
                int rnd = Random.Range(8, 16);
                e.InitLevel(rnd);
            }
            else if (spawnCount <= maxEnemy)
            {
                int rnd = Random.Range(7, 30);
                e.InitLevel(rnd);
            }

            if (e.gameObject != null)
            {
                UILevel.AddTargetIndicator(e);
            }

            e.OnPlay();

            spawnCount++;
        }
    }

    public void DespawnFromPool(Enemy enemy)
    {
        MiniPool.Despawn(enemy);

        UILevel.RemoveTargetIndicator(enemy.gameObject);
    }

}
