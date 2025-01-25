using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorSpawn : MonoBehaviour
{
    public List<GameObject> Floor_Placeholders = new List<GameObject>();
    public List<GameObject> Walls_Placeholders = new List<GameObject>();
    // public List<GameObject> L_Placeholders = new List<GameObject>();

    public List<GameObject> Floors = new List<GameObject>();
    public List<GameObject> Walls = new List<GameObject>();
    public List<GameObject> Guests = new List<GameObject>();
    // public List<GameObject> L_Buildings = new List<GameObject>();
    void Start()
    {
        SpawnAllSectorInteriors();

        ShowInterior(false, Floor_Placeholders);
        ShowInterior(false, Walls_Placeholders);
    }

    void Update()
    {
        if (Input.GetKey("r"))
        {
            SpawnAllSectorInteriors();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Guests.Add(other.gameObject);
            ShowInterior(true, Floor_Placeholders);
            ShowInterior(true, Walls_Placeholders);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
            Guests.Remove(other.gameObject);
        if (Guests.Count == 0)
        {
            ShowInterior(false, Floor_Placeholders);
            ShowInterior(false, Walls_Placeholders);
        }
    }



    public void SpawnAllSectorInteriors()
    {
        SpawnRandomBuildings(Floor_Placeholders, Floors);
        SpawnRandomBuildings(Walls_Placeholders, Walls);
        // SpawnRandomBuildings(L_Placeholders, L_Buildings);
    }

    public void SpawnRandomBuildings(List<GameObject> placeholders, List<GameObject> Parts)
    {
        if (placeholders.Count > 0)
        {
            for (int i = 0; i < placeholders.Count; i++)
            {
                GameObject randomBuilding = Parts[Random.Range(0, Parts.Count)];
                var instantiatedBuilding = Instantiate(randomBuilding, placeholders[i].transform.position, placeholders[i].transform.rotation);
                instantiatedBuilding.transform.localScale = placeholders[i].transform.localScale;


                if (placeholders[i] != null) Destroy(placeholders[i].gameObject);

                placeholders[i] = instantiatedBuilding;
            }
        }
        else
        {
            Debug.Log("No more placeholders in" + nameof(placeholders));
        }
    }

    public void ShowInterior(bool show, List<GameObject> placeholders)
    {
        for (int i = 0; i < placeholders.Count; i++)
        {
            placeholders[i].gameObject.SetActive(show);
        }
    }
}
