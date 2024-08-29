using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public PoolManager poolManager;
    public Player player;   

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
}
