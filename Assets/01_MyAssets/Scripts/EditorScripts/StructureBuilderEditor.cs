using UnityEngine;
using UnityEditor;

public class StructureBuilderEditor : EditorWindow
{
    // Define variables for the modular prefab and the number of prefabs to build
    private GameObject prefabToBuild;
    private int numberOfRows = 2;
    private int numberOfColumns = 5;
    private float prefabOffsetX = 1f;
    private float prefabOffsetZ = 1f;
    private string containerName = "Structure";

    [MenuItem("Window/Structure Builder")] // Add this script to the Window menu
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(StructureBuilderEditor));
    }

    private void OnGUI()
    {
        // Create a label and a field for the prefab selection
        prefabToBuild = EditorGUILayout.ObjectField("Prefab to Build", prefabToBuild, typeof(GameObject), true) as GameObject;

        // Create a grid layout for specifying the number of rows and columns
        numberOfRows = EditorGUILayout.IntField("Number of Rows", numberOfRows);
        numberOfColumns = EditorGUILayout.IntField("Number of Columns", numberOfColumns);

        prefabOffsetX = EditorGUILayout.FloatField("Prefab Offset X", prefabOffsetX);
        prefabOffsetZ = EditorGUILayout.FloatField("Prefab Offset Z", prefabOffsetZ);

        containerName = EditorGUILayout.TextField("Container Name", containerName);

        // Create a button for building the structure
        if (GUILayout.Button("Build Structure"))
        {
            BuildStructure();
        }
    }

    private void BuildStructure()
    {
        if (prefabToBuild == null)
        {
            Debug.LogError("No prefab selected!");
            return;
        }

        // Create an empty container object to parent the instantiated prefabs
        GameObject container = new GameObject(containerName);

        // Instantiate the prefabs and position them accordingly in a grid layout
        for (int row = 0; row < numberOfRows; row++)
        {
            for (int col = 0; col < numberOfColumns; col++)
            {
                GameObject prefabInstance = PrefabUtility.InstantiatePrefab(prefabToBuild) as GameObject;

                Vector3 position = new Vector3(col * prefabOffsetX, 0f, row * prefabOffsetZ);
                prefabInstance.transform.position = position;

                // Parent the instantiated prefabs to the container object
                prefabInstance.transform.parent = container.transform;
            }
        }
    }
}