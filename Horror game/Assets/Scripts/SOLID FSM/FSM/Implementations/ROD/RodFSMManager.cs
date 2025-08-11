using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
public class RodFSMManager : MonoBehaviour, IFSMManager
{
    IFSMBuilder[] Builders = new IFSMBuilder[1];

    public FSMContext _context;
    public bool _launchOnStart;


    void Start()
    {
        StartFSMManager(_launchOnStart);
    }
    public void StartFSMManager(bool launch)
    {
        if (launch)
        {
            SetContext(_context);
            InitializeBuilders();
        }
    }
    public void SetContext(FSMContext context)
    {
        if (_context != null)
        {
            _context = context;
        }
    }

    public void InitializeBuilders()
    {
        Builders[0] = new RodFSMBuilderStandard();
        AddIFSMBuilders(Builders);

    }
    public void AddIFSMBuilders(IFSMBuilder[] buildersArray)
    {
        if (buildersArray == null)
        {
            Debug.Log("builder is null");
            return;
        }
        else if (buildersArray.Length > 0)
        {
            foreach (var builder in buildersArray)
            {
                builder.SetContext(_context);
                Debug.Log("builder got context set");
            }
        }
        else
        {
            Debug.Log("builder already exists");
        }


    }




    public void StopFSMManager()
    {
        throw new System.NotImplementedException();
    }



}
