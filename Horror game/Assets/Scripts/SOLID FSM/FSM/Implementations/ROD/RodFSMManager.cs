using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
public class RodFSMManager : MonoBehaviour, IFSMManager
{
    [SerializeField] public List<IFSMBuilder> builders = new List<IFSMBuilder>();
    [SerializeField] public IFSMBuilder[] buildersArray;
    [SerializeField] public List<string> testStrings = new List<string>();
    public FSMContext _context;
    public bool _launchOnStart;


    public void InitializeBuilders()
    {
        AddIFSMBuilder(new RodFSMBuilderStandard());
    }
    public void AddIFSMBuilder(IFSMBuilder builder)
    {
        if (builder != null && !builders.Contains(builder))
        {
            builder.SetContext(_context);
            builders.Add(builder);
        }
        else
        {
            Debug.Log("builder is null or already exists");
        }
    }

    public void SetContext(FSMContext context)
    {
        if (_context != null)
        {
            _context = context;
        }
    }

    public void StartFSMManager(bool launch)
    {
        if (launch)
        {
            SetContext(_context);
            InitializeBuilders();
        }
    }

    public void StopFSMManager()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        StartFSMManager(_launchOnStart);
    }

}
