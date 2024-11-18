using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBuildingPartsSpown : MonoBehaviour
{
    public GameObject downWall;
    public GameObject rightWall;
    public GameObject leftWall;

    public GameObject replaceDownWall;
    public GameObject replaceRightWall;
    public GameObject replaceLeftWall;
    void Start()
    {
        Instantiate(replaceDownWall, downWall.transform.position, downWall.transform.rotation);
        Instantiate(replaceRightWall, rightWall.transform.position, rightWall.transform.rotation);
        Instantiate(replaceLeftWall, leftWall.transform.position, leftWall.transform.rotation);

    }

    void Update()
    {

    }
}
