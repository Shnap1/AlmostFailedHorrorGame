using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IFSMManager controls several Final State Machines to manage different states of the object on parralel. 
/// </summary>
public interface IFSMManager
{

    /// List<IFSMBuilder> FSMs;
    /// <summary>
    /// StartFSMManagertakes checks bool launch and if true calls AddsFSMs to (T context); that  Then 
    /// </summary>
    public void StartFSMManager(bool launch);
    public void StopFSMManager();

    /// <summary>
    ///loops  through all FSMs in a List (set through inspector) to call  SetContext<T>(T context)
    /// </summary>

    public void AddIFSMBuilder(IFSMBuilder states); // add states to a list

    /// <summary>
    ///loops  through all FSMs and put data about the object as context.
    /// </summary>
    public void SetContext<T>(T context);

    public void UpdateFSMs();
    public void FixedUpdateFSMs();
}
