using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks transformed meshes that have been saved to a project folder, so that they can automatically
/// be reused in a scene. Used in combination with RacetrackMeshManager.
/// </summary>
[CreateAssetMenu(fileName = "RacetrackMeshes", menuName = "Scriptable Objects/Racetrack Saved Meshes", order = 20)]
public class RacetrackSavedMeshes : ScriptableObject
{
    // Parameters
    public string SaveFolder = "Assets/Meshes/Racetrack Builder";

    public int IndexGenerator;

    // Saved meshes
    public List<RacetrackMeshReferenceSaved> Meshes = new List<RacetrackMeshReferenceSaved>();

    public string GetNewAssetFilename()
    {
        var filename = string.Format("{0:00000000}.asset", this.IndexGenerator++);
        string folder = this.SaveFolder.Trim();
        if (!string.IsNullOrEmpty(folder) && !folder.EndsWith("/"))
            folder += "/";
        return folder + filename;
    }
}
