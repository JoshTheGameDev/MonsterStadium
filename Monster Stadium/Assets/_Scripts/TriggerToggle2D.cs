using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.InteropServices;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
/// =============================================================================================
/// Created by Joshua D'Agostino
/// Created on 15/02/2019
/// Last Edited on 29/05/2019 by Joshua D'Agostino
/// =============================================================================================
 
public class TriggerToggle2D : MonoBehaviour
{
    [Header("--- Simple ---")]
    [Space]
    [Header("OnTrigger Event Commands")]
    [Tooltip("Use this trigger toggle once?")]
    [SerializeField]
    private bool useOnce;
    public UnityEvent onTrigger;

    public enum ToggleType { turnOn, turnOff, toggle }

    [Space]
    [Header("Variables")]
    [Space]
    [Header("--- Advanced ---")]
    [Tooltip("The tag of the object to check against")]
    [SerializeField]
   // private string tagOfObject = "default";
    [TagSelector]
    public string tagOfObject = "";

    [Space]
    [Tooltip("What do you want it to do?")]
    [SerializeField]
    private ToggleType toggleType = ToggleType.turnOn;

    [Space]
    [Tooltip("Do you want to wait for the set amount of time between obj toggles?")]
    [SerializeField]
    private bool wait;
    
    [Tooltip("Time to wait between toggeling each object")]
    [SerializeField]
    private float timeBetweenToggles = 0.01f;

    [Space]
    [Tooltip("Game Objects to Enable or Disable")]
    [SerializeField]
    private List<GameObject> toggleObject = new List<GameObject>();

    [Space]
    [Tooltip("Show Debug?")]
    [SerializeField]
    private bool debug;

    /// <summary>
    /// Checks if player enters Trigger Volume, then enables or disables all game objects listed in the inspector and waits for the delay time if any, before repeating through the list.
    /// </summary>
    /// 

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (debug == true) 
        { Debug.Log(other.gameObject.name); }

        if (other.gameObject.tag == tagOfObject)
        {
            if (toggleType == ToggleType.turnOn)
            {
                foreach (GameObject Go in toggleObject)
                {
                    
                  Go.SetActive(true);

                    if (debug == true)
                    { Debug.Log("Enabling " + Go); }
                    
                    if (wait == true) {
                        yield return new WaitForSeconds(timeBetweenToggles);
                    }
                }
            }
            else if (toggleType == ToggleType.turnOff)
            {
                foreach (GameObject Go in toggleObject)
                {
                    Go.SetActive(false);
                    if (debug == true)
                    { Debug.Log("Disabling " + Go); }
                        
                    if (wait == true)
                    {
                        yield return new WaitForSeconds(timeBetweenToggles);
                    }
                }
            }
            else if (toggleType == ToggleType.toggle)
            {
                foreach (GameObject Go in toggleObject)
                {
                    Go.SetActive(!Go);
                    if (debug == true) 
                    { Debug.Log("Toggling " + Go); }
                    
                    if (wait == true)
                    {
                        yield return new WaitForSeconds(timeBetweenToggles);
                    }
                }
            }

            // Using the Event System
            if (onTrigger != null)
            {
                onTrigger.Invoke();
            }

            if (useOnce == true)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
