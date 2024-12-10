using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] int onField;
    [SerializeField] int maxEnemy;
    [SerializeField] Player playerPrefab;
    Player player;
    public Player Player => player;

    List<Enemy> ActiveEnemies = new();
    public int remainEnemyCount { get; private set; }

    int spawnCount;

    [SerializeField] UILevel UILevel;

    Booster booster;
    [SerializeField] Booster boosterPref;

    public bool isRevive { get; private set; }

    public void OnGameStateChanged(GameState state)
    {
        if(player != null)
        {
            switch (state)
            {
                case GameState.Weapon:
                    player.gameObject.SetActive(false);
                    break;
                default:
                    player.gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void OnInit()
    {
        GameManager.Ins._OnStateChanged += OnGameStateChanged;

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
            OnEnemySpawn(GetValidPos());
        }

        AddBooster();
    }

    public void OnPlay()
    {
        player.OnPlay();

        for (int i = 0; i < ActiveEnemies.Count; i++)
        {
            Enemy e = ActiveEnemies[i];
            e.OnPlay();
        }
    }

    public void RevivePlayer()
    {
        isRevive = true;

        Vector3 pos = GetValidPos();
        player.OnRevive(pos);

    }

    public void OnDespawn()
    {
        player.OnDespawn();
        player = null;

        MiniPool.CollectAll();

        GameManager.Ins._OnStateChanged -= OnGameStateChanged;
    }

    public void AddBooster()
    {
        booster = Instantiate(boosterPref, GetRandomPos() + Vector3.up * 5f, Quaternion.identity);
        booster.OnInit();
    }

    public void OnEnemyDeath(Enemy e)
    {
        //dieu kien pool thang moi
        //dieu kien end game

        OnEnemyDeSpawn(e);

        if (spawnCount < maxEnemy)
        {
            OnEnemySpawn(GetValidPos());
        }

        if (remainEnemyCount <= 0 && GameManager.IsState(GameState.GamePlay))
        {
            player.OnVictory();
        }
    }

    private void OnEnemySpawn(Vector3 pos)
    {
        //Enemy e = Pool;
        //add Action
        //add list

        Enemy e = MiniPool.Spawn<Enemy>(PoolType.Enemy, pos, Quaternion.identity);
        e.OnInit();
        e.OnDeadEvent += OnEnemyDeath;
        ActiveEnemies.Add(e);

        int rnd = Random.Range(0, player.Level - 2);
        if (rnd < 0) { rnd = 0; }

        e.InitLevel(rnd);
        if (e != null) { UILevel.AddTargetIndicator(e); }

        if (GameManager.IsState(GameState.GamePlay)) { e.OnPlay(); }

        spawnCount++;
    }

    private void OnEnemyDeSpawn(Enemy e)
    {
        remainEnemyCount--;

        UILevel.RemoveTargetIndicator(e);

        e.OnDeadEvent -= OnEnemyDeath;
        MiniPool.Despawn(e);
        ActiveEnemies.Remove(e);
    }

    private Vector3 GetValidPos()
    {
        bool isValid;
        int count = 0;
        Vector3 pos;
        do
        {
            count++;
            isValid = true;
            pos = GetRandomPos();
            foreach (GameUnit unit in ActiveEnemies)
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
                break;
            }
        }
        while (isValid == false && count < 100000);

        return pos;
    }
    private Vector3 GetRandomPos()
    {
        float x1 = -14, x2 = 14, z1 = 11, z2 = -11;

        float x = Random.Range(x1, x2);
        float z = Random.Range(z1, z2);

        return new Vector3(x, 0, z);
    }

    
}
