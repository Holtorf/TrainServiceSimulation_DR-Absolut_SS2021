using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TrainServiceSimulation.FTS;

[CustomEditor(typeof(SearchGraphManager))]
/// <summary>
/// The editor Script for the SearchGraphManager
/// its create the Button for the other class in the editor 
/// </summary>
public class SearchGraphManagerEditor : Editor
{
    private SearchGraphManager _searchGraphManager;

    public override void OnInspectorGUI()
    {
        _searchGraphManager = (SearchGraphManager)target;

        if (GUILayout.Button("Calculate Path Costs"))
        {
            _searchGraphManager.CalculatePathCost();
        }

        base.OnInspectorGUI();
    }

    
}
