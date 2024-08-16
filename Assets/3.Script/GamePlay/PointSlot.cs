using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointSlot : MonoBehaviour
{
    public bool isSelected;
    public int slot_currentScore;

    [SerializeField] private Button slot_btn;
    [SerializeField] private Text slot_txt;





    virtual public int slotScore()
    {
        return slot_currentScore;
    }
}
