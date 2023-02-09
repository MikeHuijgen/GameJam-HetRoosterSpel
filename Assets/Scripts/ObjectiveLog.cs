using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveLog : MonoBehaviour
{
    public TMP_Text ObjectiveText;
    public Transform RoosterParent;
    private BoxCollider test;
    [SerializeField] private ExitDoor exitDoor;

    [SerializeField] private objective currentObjective;

    private int roosters;
    private int totalRoosters;
    public static ObjectiveLog Instance { get; private set; }

    private enum objective
    {
        sneakPastTheGuards,
        PickUpRoosters,
        Escape
    }

    private void Awake()
    {
        if (Instance != null)
        {
            // Dit is een check voor als er meer dan 1 UnitActionSystem is. 
            Debug.LogError("There's more than one ObjectiveLog!" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        foreach(Transform roosters in RoosterParent)
        {
            totalRoosters++;
        }
    }

    private void Update()
    {
        CheckObjectives();
    }

    private void CheckObjectives()
    {
        switch (currentObjective)
        {
            case objective.sneakPastTheGuards:
                ObjectiveText.text = "Sneak pas the guards";
                
                break;
            case objective.PickUpRoosters:
                ObjectiveText.text = $"Pick up all the roosters : {roosters} / {totalRoosters}";
                if (roosters == totalRoosters)
                {
                    currentObjective = objective.Escape;
                    exitDoor.EnableCollider();
                }
                break;
            case objective.Escape:
                ObjectiveText.text = "Go to the exit";
                break;
        }           
    }

    public void PickedUpARooster()
    {
        roosters++;
    }

    public void StartPickUpObective()
    {
        currentObjective = objective.PickUpRoosters;
    }
}
