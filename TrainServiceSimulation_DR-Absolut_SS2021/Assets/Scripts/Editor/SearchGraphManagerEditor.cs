using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TrainServiceSimulation.FTS;

[CustomEditor(typeof(SearchGraphManager))]
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
