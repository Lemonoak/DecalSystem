using UnityEngine;

public class DecalHandler : MonoBehaviour
{
    //The Decal to spawn
    public GameObject Decal;
    public DecalMesh Mesh;

    DecalMesh[] MeshPool = new DecalMesh[20];
    int DecalPoolFreeIndex = 0;

    private void Start()
    {
        for(int i = 0; i < MeshPool.Length; i++)
        {
            MeshPool[i] = Instantiate(Decal, transform.position, Quaternion.identity).GetComponent<DecalMesh>();
            MeshPool[i].name = "Decal (pooled) " + i.ToString();
            MeshPool[i].Owner = this;
            MeshPool[i].PoolOwner = this;
            MeshPool[i].gameObject.SetActive(false);
        }
    }

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
            if(DecalPoolFreeIndex < MeshPool.Length)
            {
                Quaternion Rot = Quaternion.FromToRotation(Vector3.up, RayHit.normal);
                MeshPool[DecalPoolFreeIndex].transform.position = RayHit.point + (RayHit.normal * 0.001f);
                MeshPool[DecalPoolFreeIndex].transform.rotation = Rot;
                MeshPool[DecalPoolFreeIndex].PoolIndex = DecalPoolFreeIndex;
                MeshPool[DecalPoolFreeIndex].gameObject.SetActive(true);
                MeshPool[DecalPoolFreeIndex].PlacedDecal();
                DecalPoolFreeIndex++;
                if(DecalPoolFreeIndex >= MeshPool.Length)
                {
                    DecalPoolFreeIndex = 0;
                }
            }
        }
    }

    public void ReturnDecal(int Index)
    {
        DecalPoolFreeIndex--;
        DecalMesh ReturningDecal = MeshPool[Index];
        MeshPool[Index] = MeshPool[DecalPoolFreeIndex];
        MeshPool[Index].PoolIndex = Index;
        MeshPool[DecalPoolFreeIndex] = ReturningDecal;
        ReturningDecal.gameObject.SetActive(false);
    }
}
