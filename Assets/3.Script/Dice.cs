using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dice : MonoBehaviour, IPointerClickHandler
{
    bool isKept;
    Image image;
    [SerializeField]
    Sprite[] pips;
    /// <summary>
    /// �ֻ����� keep �ϸ� ���������� �̹���
    /// </summary>
    GameObject lockImage;

    public int myNum { get; private set; }

    private void Awake()
    {
        image = GetComponent<Image>();
        isKept = false;
        lockImage = transform.Find("Lock").gameObject;
        lockImage.SetActive(isKept);
    }

    private void Start()
    {
        Reroll();
    }

    public void Reroll()
    {
        if (!isKept)
        {
            int rand = Random.Range(0, 6);
            image.sprite = pips[rand];
            myNum = rand + 1;
        }
    }

    /// <summary>
    /// RPC�� ����ȭ�� ��û���� �ʿ��� �����ϴ� �޼���
    /// </summary>
    /// <param name="_num">myNum - 1</param>
    public void DiceUpdate(int _num)
    {
        myNum = _num;
        image.sprite = pips[_num-1];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isKept = !isKept;
        Debug.Log("�ֻ��� ����");
        lockImage.SetActive(isKept);
    }
}
