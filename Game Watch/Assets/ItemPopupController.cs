using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPopupController : MonoBehaviour
{
    public GameObject popupPanel;

    [Header("3D Model")]
    public Transform modelSpawnPoint;
    private GameObject currentModel;

    [Header("Logo Quad")]
    public Renderer logoQuadRenderer;

    public void OpenPopup(GameObject modelPrefab, Texture logoTexture)
    {
        popupPanel.SetActive(true);

        // Remove old model
        if (currentModel != null)
            Destroy(currentModel);

        // Spawn new model
        currentModel = Instantiate(modelPrefab, modelSpawnPoint.position, modelSpawnPoint.rotation);

        // Set logo texture on Quad
        logoQuadRenderer.material.mainTexture = logoTexture;

        // No scroll stopping
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);

        if (currentModel != null)
            Destroy(currentModel);
    }
}
