using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;//범위
    public LayerMask targetLayer;//레이어
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void FixedUpdate() {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest(){
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in targets){
            Vector3 myPos = transform.position;
            Vector3 tragetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, tragetPos);

            if(curDiff < diff){
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }

}
