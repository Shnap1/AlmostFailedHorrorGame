using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float projectileLife;
    void Start()
    {
        StartCoroutine(DestrouProjectile(projectileLife));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestrouProjectile(float f)
    {
        yield return new WaitForSeconds(f);
        Destroy(gameObject);
    }

}
