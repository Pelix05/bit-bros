using UnityEngine;

public class ForceFogOn : MonoBehaviour
{
    void Start()
    {
        // Mengaktifkan fog
        RenderSettings.fog = true;
        
        // Mode Exponential (kabut natural)
        RenderSettings.fogMode = FogMode.Exponential;
        
        // Warna kabut (biru keunguan untuk nuansa mimpi)
        RenderSettings.fogColor = new Color(0.5f, 0.4f, 0.8f);
        
        // Ketebalan kabut (0.03 = sedang, tidak terlalu tebal)
        RenderSettings.fogDensity = 0.03f;
        
        Debug.Log("Fog telah diaktifkan!");
    }
}