using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraTargetSetter : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private string targetSceneName = "Imagination"; // Ganti dengan nama scene mimpi kamu
    
    void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }
    
    void Start()
    {
        // Cek apakah kita sedang di Scene 2
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            // Hanya set target jika di Scene 2
            SetCameraTarget();
        }
    }
    
    void SetCameraTarget()
    {
        // Cari parent dengan tag "Player"
        Transform parent = transform.parent;
        Transform preferredTarget = null;
        while (parent != null)
        {
            if (parent.CompareTag("Player"))
            {
                // Prefer a ViewController.playerViewPoint if available
                var vc = parent.GetComponentInChildren<ViewController>();
                if (vc != null && vc.playerViewPoint != null)
                    preferredTarget = vc.playerViewPoint;
                else
                    preferredTarget = parent;

                if (vcam != null && preferredTarget != null)
                {
                    vcam.Follow = preferredTarget;
                    // Do not set LookAt to avoid rotation feedback loops with local view controller
                    vcam.LookAt = null;
                    vcam.Priority = 1000;
                    Debug.Log("Camera target set to: " + preferredTarget.name);
                }
                return;
            }
            parent = parent.parent;
        }
        
        Debug.LogWarning("Player not found as parent, trying FindWithTag...");
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && vcam != null)
        {
            // Try to use player's ViewController viewpoint if present
            var vc = player.GetComponentInChildren<ViewController>();
            if (vc != null && vc.playerViewPoint != null)
                preferredTarget = vc.playerViewPoint;
            else
                preferredTarget = player.transform;

            vcam.Follow = preferredTarget;
            vcam.LookAt = null;
            vcam.Priority = 1000;
            Debug.Log("Camera target set to: " + preferredTarget.name);
        }
    }
}