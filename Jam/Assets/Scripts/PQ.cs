//https://stackoverflow.com/questions/19396346/how-to-iterate-through-linked-list
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PQ<DataType> 
{

    // An individual element in our Priority Queue
    public class PQElement
    {
        public DataType data;
        public float priority;
    }

    // Using a linked list to store all of our data internally
    public LinkedList<PQElement> linkedList = new LinkedList<PQElement>();

    // Add a new value into the priority queue, with the given priority
    public void Enqueue(DataType aData, float aPriority)
    {
        
        if (linkedList.Count > 0)
        {
            //bool keeps track if the item actually gets inserted
            bool inserted = false;
            for (LinkedListNode<PQElement> node = linkedList.First; node != null; node = node.Next)
            {
                
                if (node.Value.priority >= aPriority)
                {
                    PQElement newelement = new PQElement();
                    newelement.data = aData;
                    newelement.priority = aPriority;
                    linkedList.AddBefore(node, newelement);
                    inserted = true;
                    break;
                }
            }
            //if object not inserted, add it to the end
            if (!inserted)
            {
                PQElement newelement = new PQElement();
                newelement.data = aData;
                newelement.priority = aPriority;
                linkedList.AddLast(newelement);
            }
            
        }
        //first element in the list with throw errors, since there is no itmes in the list to iterate through
        //so te first in is added here
        else
        {
            PQElement newelement = new PQElement();
            newelement.data = aData;
            newelement.priority = aPriority;
            linkedList.AddFirst(newelement);
        }
    }

    //reads out the elements in the order of the queue
    public void DebugElements()
    {
        for (LinkedListNode<PQElement> node = linkedList.First; node != null; node = node.Next)
        {       
            // Debug.Log(node.Value.priority);
        }
    }

    // Return the element with the lowest priority
    public DataType Dequeue()
    {
        PQElement firstElement = linkedList.First.Value;
        linkedList.RemoveFirst();
        return firstElement.data;
    }
}
