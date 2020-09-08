using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using UnityEngine;

public class Bird : Agent
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private PipeHandler pipeHandler = null;
    [SerializeField] private Transform bodyTransform = null;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxVelocityMagnitude = 5f;

    private Vector3 _startingPos;
    private Quaternion _startingRot;

    public override void Initialize()
    {
        _startingPos = transform.position;
        _startingRot = transform.rotation;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = _startingPos;
        transform.rotation = _startingRot;
        bodyTransform.position = _startingPos;
        bodyTransform.rotation = _startingRot;
        rb.velocity = Vector3.zero;

        pipeHandler.ResetPipes();
        pipeHandler.ResetPasses();
        
        if (GetComponent<BehaviorParameters>().BehaviorType != BehaviorType.HeuristicOnly) return;

        GameManager.Get.ResetScore();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(0.1f);
        
        if(Mathf.FloorToInt(vectorAction[0]) != 1) return;
        
        Jump();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        
        if(!Input.GetKeyDown(KeyCode.Space)) {return;}

        actionsOut[0] = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            return;
        }
        
        AddReward(-1.0f);
        EndEpisode();
    }

    private void OnTriggerExit(Collider other)
    {
        if (GetComponent<BehaviorParameters>().BehaviorType != BehaviorType.HeuristicOnly) return;
            
        if (other.gameObject.layer == 8)
        {
            GameManager.Get.UpdateScore();
        }
    }

    private void Update()
    {
        bodyTransform.rotation = Quaternion.LookRotation(rb.velocity + new Vector3(10f, 0, 0), -transform.up);
    }
}
