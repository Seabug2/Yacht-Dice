using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Die : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 주사위를 클릭할 수 없음
    /// </summary>
    bool interactable;
    /// <summary>
    /// Is dice kept or not
    /// </summary>
    bool isKept;

    int currentNum; // Current dice num (초기화는 필요없음)

    public int Roll() // Randomize number
    {
        if (!isKept)
        {
            currentNum = UnityEngine.Random.Range(1, 7);
        }
        return currentNum;
    }

    //private Button button; // Dice button
    RectTransform rect;
    RectTransform lockRect;
    Image image; // Current dice image
    [SerializeField] Sprite[] dice_faceArr; // Sprite array of all die face
    //[SerializeField] GameObject ptcl;
    [SerializeField] GameObject lockIcon;

    private void Awake()
    {
        //button = GetComponent<Button>();
        //ptcl.SetActive(false);
        rect = GetComponent<RectTransform>();
        lockRect = lockIcon.GetComponent<RectTransform>();

        lockIcon.SetActive(false);

        isKept = false;

        //처음 생성되었을 때 무작위 면으로 초기화
        UpdateDice(Roll());
    }

    //자신의 차례가 되었을 때, 상대방에게 차례를 넘겼을 때
    //주사위를 조작할 수 없어야함
    public void DontTouchDice()
    {
        interactable = false;
        isKept = false;
        lockIcon.SetActive(false);
    }

    /// <summary>
    /// Reroll 버튼에 추가하여 주사위를 굴린 이후부터 주사위를 Keep 할 수 있게 한다
    /// </summary>
    public void IsKeepable()
    {
        interactable = true;
    }

    /// <summary>
    /// 1 ~ 6의 숫자를 받으면 주사위 그림과 현재 자신의 숫자를 바꾼다.
    /// </summary>
    /// <param name="pip"></param>
    public void UpdateDice(int pip)
    {
        currentNum = pip;
        image.sprite = dice_faceArr[pip - 1];
        rect.DOPunchScale(new Vector3(0, 1.2f, 0), .25f);
    }

    public void KeepDice()
    {
        isKept = !isKept;
        lockIcon.SetActive(isKept);
        lockRect.DOPunchScale(new Vector3(0, -.5f, 0), .25f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;

        //주사위를 클릭하면...
        KeepDice();
    }
}
