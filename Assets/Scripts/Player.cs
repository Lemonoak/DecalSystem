using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetPosition();
        }
    }

    void GetPosition()
    {
        LayerMask LayersToIgnore = LayerMask.GetMask("Decal");
        RaycastHit RayHit;
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Ray, out RayHit, LayersToIgnore))
        {
            Quaternion Rot = Quaternion.FromToRotation(Vector3.up, RayHit.normal);
            Vector3 Position = RayHit.point + (RayHit.normal * 0.001f);
            DecalHandler.GetInstance().PlaceDecal(Position, Rot);
        }
    }
}
