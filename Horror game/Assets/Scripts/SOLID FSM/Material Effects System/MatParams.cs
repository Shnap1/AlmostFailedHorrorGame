using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a universal class for holding all possible material and effect parameters. Should be passed as a patameter for methods instead of specific values so other classes will take specific values for themselves
/// </summary>
[CreateAssetMenu(fileName = "MatParams", menuName = "MatParams")]
public class MatParams : ScriptableObject
{
    public float temperatureIn;
    public float temperatureOut;

    public float waterIn;
    public float waterOut;

    public float pressureIn;
    public float pressureOut;

    public float oxygenIn;
    public float oxygenOut;


}
