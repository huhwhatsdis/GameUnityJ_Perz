using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour, Interactable
{
    [SerializeField] Text text; 

    public void Interact() {
        StartCoroutine(TextManager.Instance.ShowText(text));
    }
}
