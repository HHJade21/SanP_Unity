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
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
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
        //Dead상태에서 바꿔줬던 애들 초기화
        coll.enabled=true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;//order in layer 다시 증가
        anim.SetBool("Dead", false);
        //
        health = maxHealth;
    }
    public void Init(SpawnData data){
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Bullet") || !isLive) return;//살아있을때만 충돌이 일어나도록 islive조건도 걸었음.

        health -= other.GetComponent<Bullet>().damage;
        //넉백함수 호출
        StartCoroutine(knockBack());

        //피격 혹은 사망 애니메이션
        if(health > 0){
            anim.SetTrigger("Hit");
        }
        else{//재활용할때 이거 다 되살려야함.
            isLive = false;
            coll.enabled=false;//죽은 애의 콜라이더가 남아있으면 맵에서 불필요한 충돌이 생김.
            rigid.simulated = false;
            spriter.sortingOrder = 1;//order in layer 감소시키기(죽은 애가 플레이어나 다른 몬스터 등을 가리면 안 되니까.)
            anim.SetBool("Dead", true);
            //Dead를 바로 실행시키지 않음.
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    void Dead(){//?????
        gameObject.SetActive(false);
    }

    IEnumerator knockBack(){
        yield return wait; //다음 하나의 물리 프레임 딜레이까지 기다린다.
        Vector3 playerPos = GameManager.instance.player.transform.position;//플레이어의 위치
        Vector3 dirVec = transform.position - playerPos;//내 위치 - 플레이어 위치 : 플레이어의 반대 방향 벡터
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);//
    }
}
