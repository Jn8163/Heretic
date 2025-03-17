#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

[ExecuteInEditMode]
#endif
public class GameObjectID : MonoBehaviour
{
    public string id;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Only generate a new ID if it's part of the scene and not in prefab editing mode
        if (string.IsNullOrEmpty(id) &&
            gameObject.scene.IsValid() && // Ensure it's in a scene
            PrefabStageUtility.GetCurrentPrefabStage() == null) // Not in prefab mode
        {
            id = System.Guid.NewGuid().ToString();
            Debug.Log($"Generated new ID in OnValidate: {id} for {gameObject.name}");
            UnityEditor.EditorUtility.SetDirty(this); // Mark as dirty to save changes
        }
    }
#endif

    public string GetID()
    {
        return id;
    }
}
