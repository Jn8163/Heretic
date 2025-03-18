#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[ExecuteInEditMode]
#endif
public class GameObjectID : MonoBehaviour
{
    [SerializeField] private string id;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Skip if object is part of prefab mode or prefab asset
        if (PrefabStageUtility.GetCurrentPrefabStage() != null || PrefabUtility.IsPartOfPrefabAsset(this))
            return;

        // Skip if loading from scene (serialization not complete yet)
        if (string.IsNullOrEmpty(id) && gameObject.scene.IsValid())
        {
            // Delay the assignment until Unity has finished deserializing the scene
            EditorApplication.delayCall += () =>
            {
                if (string.IsNullOrEmpty(id))
                {
                    id = System.Guid.NewGuid().ToString();

                    // Make log clickable
                    Debug.Log($"Generated new ID in OnValidate: {id} for {gameObject.name}", gameObject);

                    // Ensure Unity saves the change
                    UnityEditor.EditorUtility.SetDirty(this);
                    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
                }
            };
        }
    }
#endif

    public string GetID()
    {
        return id;
    }
}