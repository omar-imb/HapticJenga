using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("Cube")]
    public RawImage cubePreview;
    public TMP_Text cubeName;
    public CustomizationOption[] cubeOptions;

    [Header("Floor")]
    public RawImage floorPreview;
    public TMP_Text floorName;
    public CustomizationOption[] floorOptions;

    [Header("Sky")]
    public RawImage skyPreview;
    public TMP_Text skyName;
    public CustomizationOption[] skyOptions;

    [Header("Scene References")]
    public Transform jengaTower;
    public Renderer floorRenderer;

    private int cubeIndex = 0;
    private int floorIndex = 0;
    private int skyIndex = 0;

    private int defaultCubeIndex;
    private int defaultFloorIndex;
    private int defaultSkyIndex;

    private void Start()
    {
        defaultCubeIndex = cubeIndex;
        defaultFloorIndex = floorIndex;
        defaultSkyIndex = skyIndex;

        UpdateCubeUI();
        UpdateFloorUI();
        UpdateSkyUI();
    }

    // ==================================================
    // CUBE
    // ==================================================

    public void NextCube()
    {
        cubeIndex = (cubeIndex + 1) % cubeOptions.Length;
        UpdateCubeUI();
    }

    public void PreviousCube()
    {
        cubeIndex--;

        if (cubeIndex < 0)
            cubeIndex = cubeOptions.Length - 1;

        UpdateCubeUI();
    }

    private void UpdateCubeUI()
    {
        if (cubeOptions == null || cubeOptions.Length == 0)
            return;

        cubePreview.texture =
            cubeOptions[cubeIndex].previewTexture;

        cubeName.text =
            cubeOptions[cubeIndex].optionName;
    }

    // ==================================================
    // FLOOR
    // ==================================================

    public void NextFloor()
    {
        floorIndex = (floorIndex + 1) % floorOptions.Length;
        UpdateFloorUI();
    }

    public void PreviousFloor()
    {
        floorIndex--;

        if (floorIndex < 0)
            floorIndex = floorOptions.Length - 1;

        UpdateFloorUI();
    }

    private void UpdateFloorUI()
    {
        if (floorOptions == null || floorOptions.Length == 0)
            return;
        floorPreview.texture =
            floorOptions[floorIndex].previewTexture;

        floorName.text =
            floorOptions[floorIndex].optionName;
    }

    // ==================================================
    // SKY
    // ==================================================

    public void NextSky()
    {
        skyIndex = (skyIndex + 1) % skyOptions.Length;
        UpdateSkyUI();
    }

    public void PreviousSky()
    {
        skyIndex--;

        if (skyIndex < 0)
            skyIndex = skyOptions.Length - 1;

        UpdateSkyUI();
    }

    private void UpdateSkyUI()
    {
        if (skyOptions == null || skyOptions.Length == 0)
            return;

        skyPreview.texture =
            skyOptions[skyIndex].previewTexture;

        skyName.text =
            skyOptions[skyIndex].optionName;
    }

    // ==================================================
    // APPLY
    // ==================================================

    public void ApplyChanges()
    {
        // Cambiar material de toda la torre Jenga
        if (jengaTower != null && cubeOptions.Length > 0)
        {
            Material selectedCubeMaterial =
                cubeOptions[cubeIndex].material;

            Renderer[] renderers =
                jengaTower.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                renderer.sharedMaterial = selectedCubeMaterial;
            }
        }

        // Cambiar material del piso
        if (floorRenderer != null && floorOptions.Length > 0)
        {
            floorRenderer.sharedMaterial =
                floorOptions[floorIndex].material;
        }

        // Cambiar skybox
        if (skyOptions.Length > 0)
        {
            RenderSettings.skybox =
                skyOptions[skyIndex].material;

            DynamicGI.UpdateEnvironment();
        }
    }

    // ==================================================
    // RESET
    // ==================================================

    public void ResetChanges()
    {
        cubeIndex = defaultCubeIndex;
        floorIndex = defaultFloorIndex;
        skyIndex = defaultSkyIndex;

        UpdateCubeUI();
        UpdateFloorUI();
        UpdateSkyUI();

        ApplyChanges();
    }
}