using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate() {
        //rect.position = GameManager.instance.player.transform.position;//이러면 안됨. 씬 내 캐릭터의 좌표와 카메라 스크린의 좌표가 서로 달라서. 
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
