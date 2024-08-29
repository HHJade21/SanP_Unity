using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public Rigidbody2D target;
    public RuntimeAnimatorController[] animCon;

    bool isLive;

    WaitForFixedUpdate wait;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;


        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; //얘는 정체가 뭐지???
    }

    void LateUpdate() {
        if(!isLive) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }


    private void OnEnable() {//start, awake, onenable 각각 무슨 차이?
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
    public void Init(SpawnData data){
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Bullet")) return;

        health -= other.GetComponent<Bullet>().damage;
        //넉백함수 호출
        StartCoroutine(knockBack());

        //피격 혹은 사망 애니메이션
        if(health > 0){
            anim.SetTrigger("Hit");
        }
        else{
            Dead();
        }
    }

    void Dead(){
        gameObject.SetActive(false);
    }

    IEnumerator knockBack(){
        yield return wait; //다음 하나의 물리 프레임 딜레이까지 기다린다.
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);//얜 또 뭐지???
    }
}
