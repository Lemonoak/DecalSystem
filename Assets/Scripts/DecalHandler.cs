using UnityEngine;

public class DecalHandler : MonoBehaviour
{
    //The Decal to spawn
    public GameObject Decal;
    public DecalMesh Mesh;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GetPosition();
        }
    }

    void GetPosition()
    {
        LayerMask LayersToIgnore = LayerMask.GetMask("Decal");
        RaycastHit RayHit;
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(Ray, out RayHit, LayersToIgnore))
        {
            //Debug.Log(RayHit.point);
            //Debug.Log(RayHit.normal);
            Quaternion Rot = Quaternion.FromToRotation(Vector3.up, RayHit.normal);
            //Debug.Log(Rot);
            //Debug.Log(LayersToIgnore);
            Instantiate(Decal, RayHit.point + (RayHit.normal * 0.001f), Rot);
            //Graphics.DrawMeshInstanced();
        }
    }
}
