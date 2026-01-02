using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestCor());
    }
    IEnumerator TestCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("test");
        }
    }
}
