using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointSlot : MonoBehaviour
{
    public bool isSelected;
    public int slot_currentScore;

    public Button slot_btn;
    public Text slot_txt;

    private void Awake()
    {
        TryGetComponent(out slot_btn);
        slot_txt = GetComponentInChildren<Text>();
        slot_currentScore = 0;
        slot_txt.text = slot_currentScore.ToString();
        isSelected = false;
    }

    virtual public int CalculateScore(int[] pips)
    {
        return slot_currentScore;
    }

    virtual public void UpdateScore(int score)
    {
        if (score > 0)
        {
            slot_txt.color = new Color(255, 0, 0);
        }
    }

    public void scoreSelect_btn()
    {
        isSelected = true;
        slot_btn.interactable = false;
        slot_txt.color = new Color(0, 0, 0);
        slot_txt.transform.DOPunchScale(Vector3.up, 1f, 10, 1);
    }

    public void InitSlot()
    {
        slot_currentScore = 0;
        isSelected = false;
        slot_txt.color = new Color(0, 0, 0);
    }
}
