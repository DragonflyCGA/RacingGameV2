using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Caches information about mesh templates and their meshes.
/// Measures the Z length of mesh templates, so that it is available for layout and alignment calculations.
/// Also caches mesh vertices, normals etc so that they don't have to be fetched from the GPU each time
/// (which is expensive).
/// </summary>
public class RacetrackMeshInfoCache
{
    private Dictionary<RacetrackMeshTemplate, TemplateInfo> templateInfo = new Dictionary<RacetrackMeshTemplate, TemplateInfo>();

    private Dictionary<Mesh, VertexInfo> vertexInfo = new Dictionary<Mesh, VertexInfo>();

    public static RacetrackMeshInfoCache Instance { get; } = new RacetrackMeshInfoCache();

    public void Clear()
    {
        templateInfo.Clear();
        vertexInfo.Clear();
    }

    public TemplateInfo GetTemplateInfo(RacetrackMeshTemplate template)
    {
        if (template == null)
            return null;

        // Try cache first
        TemplateInfo result;
        if (!templateInfo.TryGetValue(template, out result))
        {
            // Otherwise measure and add to cache
            result = new TemplateInfo(template);
            templateInfo.Add(template, result);
        }
        return result;
    }

    public VertexInfo GetMeshVertexInfo(Mesh mesh)
    {
        if (mesh == null)
            return null;

        // Try cache first
        VertexInfo result;
        if (!vertexInfo.TryGetValue(mesh, out result))
        {
            result = new VertexInfo(mesh);
            vertexInfo.Add(mesh, result);
        }
        return result;
    }

    public class VertexInfo
    {
        public Vector3[] Vertices;
        public Vector3[] Normals;
        public Vector2[] UV;

        public VertexInfo(Mesh mesh)
        {
            this.Vertices = mesh.vertices;
            this.Normals = mesh.normals;
            this.UV = mesh.uv;
        }
    }

    public class TemplateInfo
    {
        public float MinZ;
        public float MaxZ;
        public float Length;

        public TemplateInfo(RacetrackMeshTemplate template)
        {
            // The length of the mesh template is the length of the main driving surface mesh.
            // This is the first mesh within a subtree marked with the RacetrackContinuous component.
            var continuous = template.FindSubtrees<RacetrackContinuous>();
            var mainMesh = continuous.Select(c => c.GetComponentsInChildren<MeshFilter>().FirstOrDefault())
                                     .FirstOrDefault(m => m != null);
            if (mainMesh == null)
            {
                Debug.LogWarningFormat("RacetrackMeshTemplate '%s' has no continuous meshes. Cannot measure length.", template.gameObject.name);
                return;
            }

            // Calculate the maximum and minimum Z values in template space.
            Matrix4x4 templateFromMesh = template.GetTemplateFromSubtreeMatrix(mainMesh);
            var mesh = mainMesh.sharedMesh;
            MinZ = mesh.vertices.Min(v => templateFromMesh.MultiplyPoint(v).z);
            MaxZ = mesh.vertices.Max(v => templateFromMesh.MultiplyPoint(v).z);

            // Difference gives the length of the mesh template.
            Length = MaxZ - MinZ;
        }
    }
}
