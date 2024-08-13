using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointSlot : MonoBehaviour
{
    Text pointTxt;
    Button button;
    [SerializeField]
    string initial = "0";
    
    public bool IsFixed { get; protected set; }
    public int point { get; protected set; }

    public int settedPoint { get; private set; }

    private void Awake()
    {
        pointTxt = GetComponentInChildren<Text>();
        pointTxt.text = initial;
        IsFixed = false;
        button = GetComponent<Button>();
        button.interactable = false;
    }

    /// <summary>
    /// 주사위 다섯개를 주면 점수를 계산
    /// </summary>
    /// <param name="_pips"></param>
    public virtual void UpdateSlot(int[] _pips){}

    public void TextUpdate(int _point)
    {
        pointTxt.text = _point.ToString();
    }

    /// <summary>
    /// 버튼 이벤트
    /// </summary>
    public void FixedSlot_bt()
    {
        IsFixed = true;

        //점수칸을 확정하는 순간 상대방의 차례가 된다.
    }

    public void ResetSlot()
    {
        //점수가 확정된 점수칸은 초기화 작업을 하지 않는다.
        if (IsFixed)
        {
            return;
        }

        point = 0;
        pointTxt.text = initial;
    }
}
