using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARFilteredPlaneCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject wholePanel;
    ARFilteredPlanes filteredPlanes;

    [SerializeField]
    Toggle horizontalPlaneToggle = null,
        bigPlaneToggle = null;

    public bool HorizontalPlaneToggle
    {
        get => horizontalPlaneToggle.isOn;
        set
        {
            horizontalPlaneToggle.isOn = value;
            CheckAllAreTrue();
        }
    }
    public bool BigPlaneToggle
    {
        get => bigPlaneToggle.isOn;
        set
        {
            bigPlaneToggle.isOn = value;
            CheckAllAreTrue();
        }
    }

    private void OnEnable()
    {
        filteredPlanes = FindObjectOfType<ARFilteredPlanes>();

        filteredPlanes.OnHorizontalPlaneFound += () => HorizontalPlaneToggle = true;

        filteredPlanes.OnBigPlaneFound += () => BigPlaneToggle = true;
    }

    private void OnDisable()
    {
        filteredPlanes.OnHorizontalPlaneFound -= () => HorizontalPlaneToggle = true;

        filteredPlanes.OnBigPlaneFound -= () => BigPlaneToggle = true;
    }

    private void CheckAllAreTrue()
    {
        if (HorizontalPlaneToggle && BigPlaneToggle)
        {
            wholePanel.SetActive(false);
        }
    }
}
