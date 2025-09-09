using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckListTEST : MonoBehaviour
{
    // Start is called before the first frame update
    public E_Effect tetstEffectType;
    public Effect testEffect;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            testEffect = EffectsFactory.instance.SearchInList(tetstEffectType);
            Debug.Log(testEffect);
        }
    }
}
