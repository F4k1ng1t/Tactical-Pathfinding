using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Graph
{
    List<Connection> mConnections;

    // an array of connections outgoing from the given node
    public List<Connection> getConnections(Node fromNode)
    {
        List<Connection> connections = new List<Connection>();
        foreach (Connection c in mConnections)
        {
            if (c.getFromNode() == fromNode)
            {
                connections.Add(c);
            }
        }
        return connections;
    }

    public void Build()
    {
        // find all nodes in scene
        // iterate over the nodes
        //   create connection objects,
        //   stuff them in mConnections
        //mConnections = new List<Connection>();

        //Node[] nodes = GameObject.FindObjectsOfType<Node>();
        //foreach (Node fromNode in nodes)
        //{
        //    foreach (Node toNode in fromNode.ConnectsTo)
        //    {
        //        //change this
        //        float cost = (toNode.transform.position - fromNode.transform.position).magnitude;
        //        Debug.Log($"{cost}");
        //        Connection c = new Connection(cost, fromNode, toNode);
        //        mConnections.Add(c);
        //    }
        //}
        AvoidTaxes();
    }
    public void AvoidTaxes()
    {
        
        mConnections = new List<Connection>();
        Node[] nodes = GameObject.FindObjectsOfType<Node>();
        foreach (Node fromNode in nodes)
        {
            foreach (Node toNode in fromNode.ConnectsTo)
            {
                float cost = 0;
                //change this
                Vector3 direction = toNode.transform.position - fromNode.transform.position;
                //RaycastHit hit;
                //if (Physics.Raycast(fromNode.gameObject.transform.position, direction.normalized, out hit, direction.magnitude) &&  hit.collider.gameObject.tag == "Tax Station")
                //{
                //    //increases cost to avoid tax station
                //    cost++;
                //}

                RaycastHit[] hits = Physics.RaycastAll(fromNode.gameObject.transform.position, direction.normalized, direction.magnitude);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Tax Station"))
                    {
                        cost++;
                    }
                }

                //float cost = (toNode.transform.position - fromNode.transform.position).magnitude;
                Connection c = new Connection(cost, fromNode, toNode);
                mConnections.Add(c);
            }
        }
    }
}


public class Connection
{
    float cost;
    Node fromNode;
    Node toNode;

    public Connection(float cost, Node fromNode, Node toNode)
    {
        this.cost = cost;
        this.fromNode = fromNode;
        this.toNode = toNode;
    }
    public float getCost()
    {
        return cost;
    }

    public Node getFromNode()
    {
        return fromNode;
    }

    public Node getToNode()
    {
        return toNode;
    }
}
