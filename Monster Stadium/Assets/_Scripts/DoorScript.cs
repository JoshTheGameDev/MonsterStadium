using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right, Forwards, Backwards}

    /// <summary>
    /// The direcction to move the object in
    /// </summary>
    public Direction moveDirection;

    /// <summary>
    /// How far will the object move
    /// </summary>
    public float distance;
  
    /// <summary>
    /// How fast the object will reach it's desination
    /// </summary>
    public float duration;

    /// <summary>
    /// How fast the object will close
    /// </summary>
    public AnimationCurve scaleOverTime;



    /// <summary>
    /// The initial position of the object
    /// </summary>
    private Vector3 m_initialPos;

    /// <summary>
    /// The end position of the door
    /// </summary>
    private Vector3 m_endPos;

    //Bool to test the function while in editor
    public bool debugTest = false;

    void Start()
    {
        Vector3 direction = Vector3.zero;

        // Get the initial postition
        m_initialPos = transform.position;

        // Determine which value to modify vased on the direction selected
        switch (moveDirection)
        {
            case Direction.Up:
            {
                direction = Vector3.up;
                break;
            }

            case Direction.Down:
            {
                direction = Vector3.down;
                break;
            }

            case Direction.Left:
            {
                direction = Vector3.left;
                break;
            }

            case Direction.Right:
            {
                direction = Vector3.right;
                break;
            }

            case Direction.Forwards:
            {
                direction = Vector3.forward;
                break;
            }

            case Direction.Backwards:
            {
                direction = Vector3.back;
                break;
            }
        }

        // Calculate the end position
        m_endPos = m_initialPos + (direction * distance);
    }

    public void Update()
    {
        if (debugTest == true)
        {
            TriggerDoor();
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        TriggerDoor();
    //    }

    //    if(Input.GetKeyUp(KeyCode.F))
    //    {
    //        TriggerDoorReverse();
    //    }
    //}

    /// <summary>
    /// Triggers the door
    /// </summary>
    public void TriggerDoor()
    {
        // Begin the door moving sequence
        StartCoroutine(DoorSequence());
    }


    /// <summary>
    /// Sequence to trigger the door
    /// </summary>
    /// <returns></returns>
    IEnumerator DoorSequence()
    {
        float timer = 0;

        // Move the object towards the end postiion
        while(timer <= duration)
        {
            float scale = scaleOverTime.Evaluate(Mathf.Abs((timer / duration) - 1));

            this.transform.position = Vector3.Lerp(m_initialPos, m_endPos, (timer / duration) * scale);
            timer += Time.deltaTime;

            yield return null;
        }

        this.transform.position = m_endPos;

        // We should be done now
        yield return null;
    }

    /// <summary>
    /// Triggers the door (in reverse)
    /// </summary>
    public void TriggerDoorReverse()
    {
        StartCoroutine(DoorSequenceReverse());
    }

    /// <summary>
    /// Sequence to trigger the door
    /// </summary>
    /// <returns></returns>
    IEnumerator DoorSequenceReverse()
    {
        float timer = 0;

        // Move the object towards the end postiion
        while(timer <= duration)
        {
            float scale = scaleOverTime.Evaluate(Mathf.Abs((timer / duration) - 1));

            this.transform.position = Vector3.Lerp(m_endPos, m_initialPos, (timer / duration) * scale);
            timer += Time.deltaTime;

            yield return null;
        }

        this.transform.position = m_initialPos;

        // We should be done now
        yield return null;
    }
}
