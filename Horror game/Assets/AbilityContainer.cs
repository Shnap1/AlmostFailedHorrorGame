using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AbilityContainer : MonoBehaviour
{
    public List<Ability> abilities = new List<Ability>();
    // public GameObject Player;
    // public bool isWaitingForInput = false;
    public bool isAbilityGiven = false;
    public UnityEvent<string> onTextUpdate;

    void Start()
    {
        abilities.Add(new Ability());

    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Player = collision.gameObject;
    //     isWaitingForInput = true;

    // }
    void OnTriggerStay(Collider collision)
    {
        if (!isAbilityGiven && collision.gameObject.tag == "Player")
        {
            // Debug.Log("OnCollisionStay");

            if (Input.GetKey(KeyCode.I))
            {
                AddAbilityToList(collision.gameObject);
                onTextUpdate?.Invoke("Ability given!");
                isAbilityGiven = true;
                Debug.Log("Ability given!");
            }
            else if (Input.GetKey(KeyCode.U))
            {
                AddAbilityToSelf(collision.gameObject);
                onTextUpdate?.Invoke("Ability used!");
                isAbilityGiven = true;
                Debug.Log("Ability used!");
            }
        }
    }


    // void OnCollisionExit(Collision collision)
    // {
    //     collision = null;
    //     isWaitingForInput = false;
    // }

    // IEnumerator WaitingForInput()
    // {
    //     while (isWaitingForInput)
    //     {
    //         yield return null;
    //     }
    //     // yield return new WaitForSeconds(1);
    // }

    public void AddAbilityToList(GameObject playerobj)
    {
        if (playerobj)
        {
            foreach (var ability in abilities)
            {
                playerobj.TryGetComponent(out AbilityAdder abilityAdder);
                abilityAdder.abilities.Add(ability);
            }
        }
    }
    public void AddAbilityToSelf(GameObject playerobj)
    {
        if (playerobj)
        {
            foreach (var ability in abilities)
            {
                playerobj.AddComponent(ability.GetType());
            }
        }
    }



    public void AddToList(GameObject playerObj)
    {
        if (playerObj.GetComponent<AbilityAdder>() != null)
        {
            foreach (var ability in abilities)
            {
                playerObj.GetComponent<AbilityAdder>().abilities.Add(ability);
            }
        }
    }
}
