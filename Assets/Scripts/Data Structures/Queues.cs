using System;
public class QueueNode<T>
{
    private QueueNode<T> next;
    private T data;

    public QueueNode(T t)
    {
        next = null;
        data = t;
    }

    public QueueNode<T> Next
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

public class Queue<T>
{
    private QueueNode<T> front;
    private QueueNode<T> back;

    public Queue()
    {
        front = null;
        back = null;
    }

    public void Enqueue(T data)
    {
        QueueNode<T> node = new QueueNode<T>(data);
        if (back == null)
        {
            back = node;
            front = node;
        }
        else
        {
            back.Next = node;
            back = node;
        }
    }

    public QueueNode<T> Dequeue()
    {
        if (front == null)
        {
            return null;
        }
        else
        {
            QueueNode<T> node = front;
            front = front.Next;
            if (front == null)
            {
                back = null;
            }
            return node;
        }
    }

    public QueueNode<T> Front()
    {
        return front;
    }

    public QueueNode<T> Back()
    {
        return back;
    }

    public bool IsEmpty()
    {
        return front == null;
    }
}
