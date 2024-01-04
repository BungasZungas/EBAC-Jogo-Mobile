using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUNGAS.Core.Singleton;

public class ColorManager : Singleton<ColorManager>
{
    public enum ArtType
    {
        TYPE_01,
        TYPE_02
    }

    public List<Material> materials;
    public List<ColorSetup> colorSetup;


    public void ChangeColorByType(ColorManager.ArtType artType)
    {
        var setup = colorSetup.Find(i => i.artType == artType);

        for(int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_Color", setup.colors[i]);
        }

    }
}


public class ColorSetup
{
    public ColorManager.ArtType artType;
    public List<Color> colors;
}