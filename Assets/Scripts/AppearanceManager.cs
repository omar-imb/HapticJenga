using UnityEngine;
using TMPro;

public class AppearanceManager : MonoBehaviour
{
    [Header("Scene References")]
    public Transform jengaParent;
    public Renderer floorRenderer;

    [Header("Dropdowns")]
    public TMP_Dropdown blockDropdown;
    public TMP_Dropdown floorDropdown;
    public TMP_Dropdown skyDropdown;

    [Header("Materials")]
    public Material[] blockMaterials;
    public Material[] floorMaterials;
    public Material[] skyboxMaterials;

    private Renderer[] blockRenderers;

    void Start()
    {
        if (jengaParent != null)
            blockRenderers = jengaParent.GetComponentsInChildren<Renderer>();

        // Fill dropdown text automatically
        SetupDropdown(blockDropdown, blockMaterials);
        SetupDropdown(floorDropdown, floorMaterials);
        SetupDropdown(skyDropdown, skyboxMaterials);

        // Add listeners
        if (blockDropdown != null)
            blockDropdown.onValueChanged.AddListener(SetBlockMaterial);

        if (floorDropdown != null)
            floorDropdown.onValueChanged.AddListener(SetFloorMaterial);

        if (skyDropdown != null)
            skyDropdown.onValueChanged.AddListener(SetSkybox);

        // Apply default selection at start
        if (blockDropdown != null) SetBlockMaterial(blockDropdown.value);
        if (floorDropdown != null) SetFloorMaterial(floorDropdown.value);
        if (skyDropdown != null) SetSkybox(skyDropdown.value);
    }

    void SetupDropdown(TMP_Dropdown dropdown, Material[] materials)
    {
        if (dropdown == null || materials == null) return;

        dropdown.ClearOptions();

        var options = new System.Collections.Generic.List<string>();
        foreach (Material mat in materials)
        {
            options.Add(mat != null ? mat.name : "None");
        }

        dropdown.AddOptions(options);
    }

    public void SetBlockMaterial(int index)
    {
        if (blockRenderers == null || index < 0 || index >= blockMaterials.Length) return;

        foreach (Renderer rend in blockRenderers)
        {
            if (rend != null)
                rend.material = blockMaterials[index];
        }
    }

    public void SetFloorMaterial(int index)
    {
        if (floorRenderer == null || index < 0 || index >= floorMaterials.Length) return;

        floorRenderer.material = floorMaterials[index];
    }

    public void SetSkybox(int index)
    {
        if (index < 0 || index >= skyboxMaterials.Length) return;

        RenderSettings.skybox = skyboxMaterials[index];
        DynamicGI.UpdateEnvironment();
    }
}