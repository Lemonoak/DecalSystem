using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalHandler : MonoBehaviour
{

    public GameObject Decal;

    void Start()
    {
        
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
        //LayerMask LayersToIgnore;
        //LayersToIgnore =~ LayerMask.GetMask("Decal");
        RaycastHit RayHit;
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(Ray, out RayHit/*, LayersToIgnore*/))
        {
            //Debug.Log(RayHit.point);
            //Debug.Log(RayHit.normal);
            Quaternion Rot = Quaternion.FromToRotation(Vector3.up, RayHit.normal);
            //Debug.Log(Rot);
            Instantiate(Decal, RayHit.point + (RayHit.normal * 0.001f), Rot);
        }
    }
}
