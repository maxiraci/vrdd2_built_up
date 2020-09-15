﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR;

public class AttachableObject : MonoBehaviour
{
    public List<Attachment> m_attachments;
    float m_breakForce = 600f;
    List<Vector3> m_forceVectors = new List<Vector3>();
    Rigidbody rigidBody;

    public List<GameObject> m_connectedObjects;

    //private void OnCollisionStay(Collision collision)
    //{
    //    Vector3 v = collision.impulse / Time.fixedDeltaTime;
    //    if(!m_forceVectors.Contains(v)) m_forceVectors.Add(v);
    //    print("adding " + v);
    //}

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void LateUpdate()
    {
      
    }

    private void FixedUpdate()
    { 
        //Vector3 totalForce = Vector3.zero;// (Physics.gravity * rigidBody.mass);
        //print("totalForce begin: " + totalForce);
        //foreach (Vector3 v in m_forceVectors)
        //{
        //    totalForce += v;
        //}
        //if (totalForce.magnitude > m_breakForce)
        //{
        //    print("total: " + totalForce + ", breaking");
        //    gameObject.SetActive(false);
        //}

        //m_forceVectors.Clear();
    }

    public List<GameObject> GetConnectedObjects(List<GameObject> _o)
    {
        if(_o == null) _o = new List<GameObject> { gameObject };

        foreach (Attachment a in m_attachments)
        {
            if (!_o.Contains(a.gameObject)) _o.Add(a.gameObject);
            foreach (AttachableObject _ao in a.m_connectedObjects.Keys)
            {
                if (!_o.Contains(_ao.gameObject))
                    _o.AddRange(_ao.GetConnectedObjects(_o));
            }
        }

        return _o;
    }
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_connectedObjects = GetConnectedObjects(null);
        }
    }
}
