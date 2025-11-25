using UnityEngine;
using Oculus.Interaction.HandGrab;

public class CheckGrabPoses : MonoBehaviour
{
    void Start()
    {
        var interactables = Object.FindObjectsByType<HandGrabInteractable>(FindObjectsSortMode.None);
        foreach (var i in interactables)
        {
            if (i.HandGrabPoses == null || i.HandGrabPoses.Count == 0)
                Debug.LogError("❌ Missing poses on: " + i.gameObject.name);
        }
    }
}