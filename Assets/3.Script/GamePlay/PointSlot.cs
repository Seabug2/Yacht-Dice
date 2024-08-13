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
    /// �ֻ��� �ټ����� �ָ� ������ ���
    /// </summary>
    /// <param name="_pips"></param>
    public virtual void UpdateSlot(int[] _pips){}

    public void TextUpdate(int _point)
    {
        pointTxt.text = _point.ToString();
    }

    /// <summary>
    /// ��ư �̺�Ʈ
    /// </summary>
    public void FixedSlot_bt()
    {
        IsFixed = true;

        //����ĭ�� Ȯ���ϴ� ���� ������ ���ʰ� �ȴ�.
    }

    public void ResetSlot()
    {
        //������ Ȯ���� ����ĭ�� �ʱ�ȭ �۾��� ���� �ʴ´�.
        if (IsFixed)
        {
            return;
        }

        point = 0;
        pointTxt.text = initial;
    }
}
