using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public GameObject modelPrefab;
    public Texture logoTexture;

    public ItemPopupController controller;

    public void OnClick()
    {
        controller.OpenPopup(modelPrefab, logoTexture);
    }
}
