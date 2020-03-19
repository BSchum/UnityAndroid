using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialProvider", menuName ="Material/MaterialProvider")]
public class MaterialsProvider : ScriptableObject
{
    [SerializeField] List<Material> materials;
    public int MaterialCount { 
        get {
            return materials.Count;
        }
    }

    public Material GetMaterial(int index)
    {
        return materials[index];
    }


}
