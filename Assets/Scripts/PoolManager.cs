using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //오브젝트 풀링? 찾아보기.
    //프리팹들을 보관할 변수
    public GameObject[] prefabs;

    //..을 담당하는 리스트들
    List<GameObject>[] pools;

    private void Awake() {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pools.Length; index++){
            pools[index] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //선택한 풀의 놀고 있는 게임오브젝트 접근. 발견하면 select에 할당.
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못 찾았으면 ? 새롭게 생성해서 select에 추가.
        if(!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }


}
