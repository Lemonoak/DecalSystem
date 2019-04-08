using UnityEngine;

public class DecalHandler : MonoBehaviour
{
    static DecalHandler Instance;
    //The Decal to spawn
    public GameObject Decal;
    public DecalMesh Mesh;

    DecalMesh[] MeshPool = new DecalMesh[20];
    int DecalPoolFreeIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < MeshPool.Length; i++)
        {
            MeshPool[i] = Instantiate(Decal, transform.position, Quaternion.identity).GetComponent<DecalMesh>();
            MeshPool[i].name = "Decal (pooled) " + i.ToString();
            MeshPool[i].PoolOwner = this;
            MeshPool[i].gameObject.SetActive(false);
        }
    }

    public void PlaceDecal(Vector3 Position, Quaternion Rot)
    {
        if (DecalPoolFreeIndex < MeshPool.Length)
        {
            MeshPool[DecalPoolFreeIndex].transform.position = Position;
            MeshPool[DecalPoolFreeIndex].transform.rotation = Rot;
            MeshPool[DecalPoolFreeIndex].PoolIndex = DecalPoolFreeIndex;
            MeshPool[DecalPoolFreeIndex].gameObject.SetActive(true);
            MeshPool[DecalPoolFreeIndex].PlacedDecal();
            DecalPoolFreeIndex++;
            if (DecalPoolFreeIndex >= MeshPool.Length)
            {
                DecalPoolFreeIndex = 0;
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

    public static DecalHandler GetInstance()
    {
        if(Instance == null)
        {
            Instance = new GameObject("DecalHandler").AddComponent<DecalHandler>();
        }
        return Instance;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
