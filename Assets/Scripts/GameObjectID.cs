using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode] // This will only be applied in the Unity Editor
#endif
public class GameObjectID : MonoBehaviour
{
    public string id;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Generate a new ID in the editor if it's not set already
        if (string.IsNullOrEmpty(id))
        {
            id = System.Guid.NewGuid().ToString();
            Debug.Log($"Generated new ID in OnValidate: {id} for {gameObject.name}");
        }
    }
#endif

    public string GetID()
    {
        return id;
    }
}
