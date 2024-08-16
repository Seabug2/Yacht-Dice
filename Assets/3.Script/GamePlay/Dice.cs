using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public bool isKept; // Is dice kept or not
    public int dice_CurrentNum; // Current dice num
    public int dice_num() // Randomize number
    {
        if (!isKept)
        {
            dice_CurrentNum = (int)Random.Range(1, 7);
        }
        return dice_CurrentNum;
    }
    [SerializeField] private Button dice_btn; // Dice button
    [SerializeField] private Image dice_currentFace; // Current dice image
    [SerializeField] private Sprite[] dice_faceArr; // Sprite array of all die face
    [SerializeField] private GameObject dice_particle; // Name : "Dice_Particle"
    [SerializeField] private GameObject dice_lock; // Name : "Dice_Lock"

    private void Awake()
    {
        dice_particle = transform.Find("Dice_Particle").gameObject;
        dice_particle.SetActive(false);
        dice_lock = transform.Find("Dice_Lock").gameObject;
        dice_lock.SetActive(false);
        TryGetComponent(out dice_btn);
        dice_btn.interactable = false;
        UpdateDice(dice_num());
    }

    public void UpdateDice(int pips)
    {
        dice_currentFace.sprite = dice_faceArr[pips - 1];
        dice_CurrentNum = pips;
    }

    public void RollDice()
    {
        dice_btn.interactable = true;
    }

    public void KeepDice()
    {
        if (!isKept)
        {
            isKept = true;
            dice_lock.SetActive(true);
        }
        else
        {
            isKept = false;
            dice_lock.SetActive(false);
        }
    }

    public void InitDice()
    {
        dice_btn.interactable = true;
    }
}
