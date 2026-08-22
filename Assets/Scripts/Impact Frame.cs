using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ImpactFrame : MonoBehaviour
{
    public ScriptableRendererData rendererData; // drag your URP Renderer asset here
    private const string FeatureName = "Impact Black";
    [SerializeField] private PlayerParticleManager PlayerParticleManager;


    public Camera mainCamera;
    public LayerMask selectedLayerOnly;

    private int originalMask;
    private Color originalBg;
    private CameraClearFlags originalClearFlags;

    private void OnEnable()
    {
        foreach (var feature in rendererData.rendererFeatures)
        {
            if (feature.name == FeatureName)
            {
                feature.SetActive(false);
                break;
            }
        }
    }
    public void SetImpact(float time)
    {
        StartCoroutine("Impact", time);
    }

    private IEnumerator Impact(float time)
    {

        originalMask = mainCamera.cullingMask;
        originalBg = mainCamera.backgroundColor;
        originalClearFlags = mainCamera.clearFlags;

        mainCamera.clearFlags = CameraClearFlags.SolidColor;
        mainCamera.backgroundColor = Color.white;
        mainCamera.cullingMask = selectedLayerOnly;

        PlayerParticleManager.Impact(time); 
        foreach (var feature in rendererData.rendererFeatures)
        {
            if (feature.name == FeatureName)
            {
                feature.SetActive(true);
                break;
            }
        }
        
        yield return new WaitForSeconds(time);

        foreach (var feature in rendererData.rendererFeatures)
        {
            if (feature.name == FeatureName)
            {
                feature.SetActive(false);
                break;
            }
        }

        mainCamera.cullingMask = originalMask;
        mainCamera.backgroundColor = originalBg;
        mainCamera.clearFlags = originalClearFlags;
    }
}
