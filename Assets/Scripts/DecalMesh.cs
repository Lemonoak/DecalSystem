using UnityEngine;

public class DecalMesh : MonoBehaviour
{

    public Material MeshMaterial;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

        }
    }

    public void SpawnMesh()
    {
        //The Gameobject
        GameObject MyPlane = new GameObject("Plane");
        //Adds a meshfilter to the gameobject
        MeshFilter MeshFilter = (MeshFilter)MyPlane.AddComponent(typeof(MeshFilter));
        //Size of the gameobject mesh
        MeshFilter.mesh = MyMesh(0.1f, 0.1f);
        //Adds a mesh renderer to the object
        MeshRenderer MyMeshRenderer = MyPlane.AddComponent<MeshRenderer>();
        MyMeshRenderer.material.color = Color.red;
        MyMeshRenderer.material = MeshMaterial;
    }

    Mesh MyMesh(float Width, float Height)
    {
        Mesh ThisMesh = new Mesh();
        ThisMesh.name = "MeshByCode";
        ThisMesh.vertices = new Vector3[]
        {
            //Bottom Left Corner
            new Vector3(-Width, -Height, 0.01f),
            //Bottom, Right Corner
            new Vector3(Width, -Height, 0.01f),
            //Top Right Corner
            new Vector3(Width, Height, 0.01f),
            //Top Left Corner
            new Vector3(-Width, Height, 0.01f)
        };
        ThisMesh.uv = new Vector2[]
        {
            new Vector2 (0, 0),
            new Vector2 (0, 1),
            new Vector2(1, 1),
            new Vector2 (1, 0)
        };
        ThisMesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        ThisMesh.RecalculateNormals();

        return ThisMesh;
    }
}
