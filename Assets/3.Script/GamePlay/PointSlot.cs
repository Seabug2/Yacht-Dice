using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointSlot : MonoBehaviour
{
    public bool isSelected;
    public int slot_currentScore;

    public int[] dice_count;

    [SerializeField] private Button slot_btn;
    [SerializeField] private Text slot_txt;

    private void Awake()
    {
        TryGetComponent(out slot_btn);
        slot_txt = GetComponentInChildren<Text>();
        slot_currentScore = 0;
        slot_txt.text = slot_currentScore.ToString();
        isSelected = false;
        dice_count = new int[6];
    }

    virtual public int UpdateScore(int[] pips)
    {
        return slot_currentScore;
    }

    public void scoreSelect_btn()
    {
        isSelected = true;
        slot_btn.interactable = false;
        slot_txt.color = new Color(255, 0, 0);
    }
}
