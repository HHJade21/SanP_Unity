using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Object")]
    public PoolManager poolManager;
    public Player player;   

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {3, 5, 10, 100, 150, 210, 280, 360, 450, 600};

    void Awake()
    {
        instance = this;
    }
    private void Update() {
        gameTime += Time.deltaTime;

        if(gameTime>maxGameTime){
            gameTime = maxGameTime;
        }
    }
    private void Start() {
        health = maxHealth;
    }

    public void GetExp(){
        exp++;
        if(exp == nextExp[level]){
            level++;
            exp = 0;
        }
    }
}
