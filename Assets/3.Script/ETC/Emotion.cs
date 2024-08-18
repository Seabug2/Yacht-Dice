using UnityEngine;
using UnityEngine.UI;

public class Emotion : MonoBehaviour
{
    [SerializeField] Image[] images;
    Animator ani;

    private void Awake()
    {
        images = GetComponents<Image>();
        ani = GetComponent<Animator>();
    }

    public void Setup(int iNum)
    {
        switch (iNum)
        {
            case 1:
                SetForm(1);
                return;
            case 2:
                SetForm(2);
                return;
            case 3:
                SetForm(3);
                return;
            case 4:
                SetForm(4);
                return;
            case 5:
                SetForm(5);
                return;
            case 6:
                SetForm(6);
                return;
            case 7:
                SetForm(7);
                return;
            case 8:
                SetForm(8);
                return;
            case 9:
                SetForm(9);
                return;
            case 10:
                SetForm(10);
                return;
            case 11:
                SetForm(11);
                return;
        }
    }

    public void SetForm(int iNum)
    {
        images[iNum].gameObject.SetActive(true);
        ani.SetTrigger("isPop");
    }
}
