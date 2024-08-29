using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InforType {Exp, Level, kill, Time, Health};//열거형 데이터 선언
    public InforType type;

    Text myText;
    Slider mySlider;

    private void Awake() {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate() {
        switch(type){
            case InforType.Exp :
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / maxExp;
                break;
            case InforType.Level :
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);//중괄호 안쪽 숫자는 뒤에 오는 데이터의 순번. 뒤쪽 F0은 소수점 버린다는 뜻.
                break;
            case InforType.kill :
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InforType.Time :
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);//D2는 두 자리 숫자라는 뜻.
                break;
            case InforType.Health :
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
