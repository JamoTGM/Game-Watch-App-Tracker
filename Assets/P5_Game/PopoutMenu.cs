using UnityEngine;

public class PopoutMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public RectTransform menuButton;

    // Position when menu is closed
    public Vector2 closedPosition = new Vector2(1200, 95);

    // Position when menu is open
    public Vector2 openPosition = new Vector2(900, 95);

    public void ToggleMenu()
    {
        bool isOpen = !menuPanel.activeSelf;
        menuPanel.SetActive(isOpen);

        if (isOpen)
            menuButton.anchoredPosition = openPosition;
        else
            menuButton.anchoredPosition = closedPosition;
    }
}