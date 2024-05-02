using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{
    public enum EnemyType
    {
        EnemyA,
        EnemyB,
        EnemyC,
    }
    public enum EnemyBoss
    {
        EnemyD
    }


    public int _monsterCount = 0;
    public int _reserveCount = 0;
    public int _keepMonsterCount = 0;
    public int _totalCount = 0;
    Vector3 _spawnPosition = Vector3.zero;
    float _spawnRadius = 15.0f;
    public float _spawnTime = 5.0f;

    public void AddMonsterCount(int value)
    {
        _monsterCount += value;
        if(_monsterCount<0)
        {
            _monsterCount = 0;
        }
        Debug.Log($"EnemySpawn : {_monsterCount}");
    }
    public void SetKeepMonsterCount(int count)
    {
        _keepMonsterCount = count;  
    }
    private void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;   
        Managers.Game.OnSpawnEvent += AddMonsterCount;   
    }

    private void Update()
    {
        while(_reserveCount+Managers.Game._monsters.Count<_keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
        if(_totalCount%15==0)
        {
            StartCoroutine("BossSpawn");
        }
    }


    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        _totalCount++;
        yield return new WaitForSeconds(Random.Range(0,_spawnTime));
        string EnemyPath = GetRandomEnumValue<EnemyType>().ToString();
        GameObject obj = Managers.Game.SpawnEnemy(null, EnemyPath);

        NavMeshAgent nwa = obj.GetOrAddComponent<NavMeshAgent>();  

        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0 ,_spawnRadius);
            randDir.y = 3;
            randPos = gameObject.transform.position + randDir;

            NavMeshPath path = new NavMeshPath();
            if (nwa.CalculatePath(randPos, path))
                break;
        }
        obj.GetComponent<Enemy>()._target = Managers.Game.GetPlayer().transform;
        obj.transform.position = randPos;
        _reserveCount--;
       
    }

    IEnumerator BossSpawn()
    {
        _totalCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));
        string EnemyPath = EnemyBoss.EnemyD.ToString();
        GameObject obj = Managers.Game.SpawnEnemy(null, EnemyPath);

        NavMeshAgent nwa = obj.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randDir.y = 3;
            randPos = gameObject.transform.position + randDir;

            NavMeshPath path = new NavMeshPath();
            if (nwa.CalculatePath(randPos, path))
                break;
        }
        obj.GetComponent<Enemy>()._target = Managers.Game.GetPlayer().transform;
        obj.transform.position = randPos;
    }

    public T GetRandomEnumValue<T>()
    {
        var enumVal = System.Enum.GetValues(enumType: typeof(T));
        return (T)enumVal.GetValue(Random.Range(0, enumVal.Length));
    }

}
