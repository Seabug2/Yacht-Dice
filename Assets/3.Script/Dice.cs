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
    /// 주사위를 keep 하면 덧씌워지는 이미지
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
    /// RPC로 동기화를 요청받은 쪽에서 수행하는 메서드
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
        Debug.Log("주사위 눌림");
        lockImage.SetActive(isKept);
    }
}
