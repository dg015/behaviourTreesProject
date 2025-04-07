using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementControls 
{


    public static void AirControl(Transform agent, Transform targetPosition, float ascendSpeed)
    {
        
        float speed = Mathf.Clamp(Vector3.Distance(agent.transform.position, targetPosition.transform.position) / ascendSpeed, .2f, 1f);
        // if player is not being chased stay at default, if go down to player level
        float newY = Mathf.Lerp(agent.transform.position.y, targetPosition.transform.position.y, Time.deltaTime * speed);
        agent.transform.position = new Vector3(agent.transform.position.x, newY, agent.transform.position.z);

    }

    public static void ChangeColour(Renderer ImageRender, Color colour)
    {
        //just change colour
        ImageRender.material.color = colour;
    }
}
