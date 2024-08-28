using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    int level;
    float timer;

    private void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();//이러면 0번에는 spawner 자체의 transform이 들어감.
    }
    

    void Spawn(){
        GameObject enemy = GameManager.instance.poolManager.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;//spawner가 아니라 points의 transform만 사용하기 위해 1번 인덱스부터. 
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);
        if(timer>(spawnData[level].spawnTime)){
            Spawn();
            timer = 0f;
        }
    }
}

//직렬화 : 개체를 저장 혹은 전송하기 위해 변환하는 것.
[System.Serializable]//이걸하면 아래 클래스가 직렬화되며 인스펙터에 보임.
public class SpawnData
{
    public float spawnTime;
    public int spriteType;

    public int health;
    public float speed;
}
