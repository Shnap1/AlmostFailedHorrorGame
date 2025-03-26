using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SkyboxSpawn : MonoBehaviour
{
    [SerializeField] private List<Texture> cubeMapList;

    private void Start()
    {
        // RenderSettings.skybox = skyboxMaterialOne;
        SetRandomSkybox();
    }


    private void SetRandomSkybox()
    {
        int randomIndex = Random.Range(0, cubeMapList.Count);
        ChangeSkybox(cubeMapList[randomIndex]);
    }

    private void ChangeSkybox(Texture newTexture)
    {
        ((Material)RenderSettings.skybox).mainTexture = newTexture;

        Material skyboxMaterial = new Material(Shader.Find("Skybox/Cubemap"));
        skyboxMaterial.SetTexture("_Tex", newTexture);

        // Assign the material to the scene's skybox
        RenderSettings.skybox = skyboxMaterial;

        // Update the environment lighting if using dynamic GI
        DynamicGI.UpdateEnvironment();
        RenderSettings.customReflectionTexture = newTexture;
    }



}


