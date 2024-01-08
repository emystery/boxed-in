using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    private Node<T> next;
    private T data;

    public Node(T value)
    {
        next = null;
        data = value;
    }

    public Node<T> Next
    {
        get { return next; }
        set { next = value; }
    }

    public T Data
    {
        get { return data; }
        set { data = value; }
    }
}

public class LinkedList<T>
{
    private Node<T> head;
    public int count = 0;

    public LinkedList()
    {
        head = null;
    }

    public int Length()
    {
        return count;
    }

    public void InsertAtBegin(T data)
    {
        Node<T> node = new Node<T>(data);
        node.Next = head;
        head = node;
        count++;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T> current = head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    public bool IsEmpty()
    {
        if (head == null)
        {
            return true;
        }
        else { return false; }
    }


    public Node<T> Search(T data)
    {
        Node<T> current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
            {
                return current;
            }
            current = current.Next;
        }
        return null;
    }


    public void InsertAtEnd(T data)
    {
        Node<T> node = new Node<T>(data);

        if (head == null)
        {
            node.Next = head;
            head = node;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
                current = current.Next;
            current.Next = node;
        }
    }

    public void InsertAfter(T after, T data)
    {
        Node<T> afterNode = Search(after);

        if (afterNode != null)
        {
            Node<T> newNode = new Node<T>(data);
            Node<T> temp = afterNode.Next;
            afterNode.Next = newNode;
            newNode.Next = temp;
        }
    }

    public void Remove(T data)
    {
        Node<T> current = head;
        Node<T> previous = null;
        //search and keep both current and previous pointers
        while ((current != null) && (!current.Data.Equals(data)))
        {
            previous = current;
            current = current.Next;
        }
        if (current != null)
        {
            if (previous == null)           //remove from beginning
            {
                head = current.Next;
            }
            else                             //remove from anny other place
            {
                previous.Next = current.Next;
            }

        }
    }

    public LinkedList<T> TopThree()
    {
        LinkedList<T> topThree = new LinkedList<T>();

        if (head == null)
        {
            return topThree;
        }

        Node<T> current = head;
        int counter = 0;

        while (current != null && counter < 3)
        {
            Node<T> topOne = current;
            Node<T> temp = current.Next;

            while (temp != null)
            {
                if (Comparer<T>.Default.Compare(temp.Data, topOne.Data) > 0)
                {
                    topOne = temp;
                }
                temp = temp.Next;
            }

            topThree.InsertAtEnd(topOne.Data);
            Remove(topOne.Data);

            counter++;

            current = current.Next;
        }

        return topThree;
    }



}