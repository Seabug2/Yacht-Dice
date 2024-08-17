using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Die : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// �ֻ����� Ŭ���� �� ����
    /// </summary>
    bool interactable;
    /// <summary>
    /// Is dice kept or not
    /// </summary>
    bool isKept;

    int currentNum; // Current dice num (�ʱ�ȭ�� �ʿ����)

    public int Roll() // Randomize number
    {
        if (!isKept)
        {
            currentNum = Random.Range(1, 7);
        }
        interactable = true;
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
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        lockRect = lockIcon.GetComponent<RectTransform>();
        lockIcon.SetActive(false);
        isKept = false;
    }

    private void Start()
    {
        //ó�� �����Ǿ��� �� ������ ������ �ʱ�ȭ
        UpdateDice(Roll());
        DontTouchDice();
    }

    //�ڽ��� ���ʰ� �Ǿ��� ��, ���濡�� ���ʸ� �Ѱ��� ��
    //�ֻ����� ������ �� �������
    public void DontTouchDice()
    {
        interactable = false;
        isKept = false;
        lockIcon.SetActive(false);
    }

    /// <summary>
    /// 1 ~ 6�� ���ڸ� ������ �ֻ��� �׸��� ���� �ڽ��� ���ڸ� �ٲ۴�.
    /// </summary>
    /// <param name="pip"></param>
    public void UpdateDice(int pip)
    {
        image.sprite = dice_faceArr[pip - 1];
        rect.DOPunchScale(Vector3.one * .3f, 1f, 8);
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

        //�ֻ����� Ŭ���ϸ�...
        KeepDice();
    }
}
