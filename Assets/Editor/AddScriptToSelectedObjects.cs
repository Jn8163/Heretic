using UnityEditor;
using UnityEngine;

public class AddScriptToSelectedObjects : EditorWindow
{
    // Reference to the script we want to add to selected objects
    public MonoScript scriptToAdd;

    [MenuItem("Tools/Add Script to Selected Objects")]
    static void Init()
    {
        // Open the window where the user can select a script
        AddScriptToSelectedObjects window = (AddScriptToSelectedObjects)EditorWindow.GetWindow(typeof(AddScriptToSelectedObjects));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Script to Selected Objects", EditorStyles.boldLabel);

        // Let the user choose which script to add
        scriptToAdd = (MonoScript)EditorGUILayout.ObjectField("Script to Add", scriptToAdd, typeof(MonoScript), false);

        if (GUILayout.Button("Add or Replace Script"))
        {
            // Ensure a script is selected
            if (scriptToAdd != null)
            {
                AddOrReplaceScriptOnSelected();
            }
            else
            {
                Debug.LogError("Please select a script to add.");
            }
        }
    }

    private void AddOrReplaceScriptOnSelected()
    {
        // Loop through all selected objects
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            // Get the class type of the script we want to add
            System.Type scriptType = scriptToAdd.GetClass();

            // Check if the script is already attached to the object
            var existingComponent = selectedObject.GetComponent(scriptType);

            if (existingComponent != null)
            {
                // If the component already exists, remove it first
                DestroyImmediate(existingComponent);
                Debug.Log($"{selectedObject.name} already had {scriptToAdd.name}, replacing it.");
            }

            // Add the script component to the object
            selectedObject.AddComponent(scriptType);
            Debug.Log($"Added {scriptToAdd.name} to {selectedObject.name}");
        }
    }
}