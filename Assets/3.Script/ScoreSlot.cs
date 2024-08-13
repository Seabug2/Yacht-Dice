using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreSlot : MonoBehaviour
{
    Text pointTxt;
    Button button;
    [SerializeField]
    string initial = "0";

    public int settedPoint { get; private set; }

    private void Awake()
    {
        pointTxt = GetComponentInChildren<Text>();
        pointTxt.text = initial;

        button = GetComponent<Button>();
        button.interactable = false;
    }

    /// <summary>
    /// 주사위 다섯개를 주면 점수를 계산
    /// </summary>
    /// <param name="_points"></param>
    public virtual void UpdatePoint(int[] _points){}

    public void UpdatePoint(int _point)
    {
        pointTxt.text = _point.ToString();
    }
}
