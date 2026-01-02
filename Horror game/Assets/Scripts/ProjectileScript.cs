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
        StartCoroutine(DestroyProjectile(projectileLife));
    }

    public IEnumerator DestroyProjectile(float f)
    {
        yield return new WaitForSeconds(f);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        StopCoroutine(DestroyProjectile(projectileLife));
    }

}
