using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public GameObject target;
    private void Start()
    {
        target.SetActive(false);
    }

    public void Toggle()
    {
        target.SetActive(!target.activeSelf);
    }
}
