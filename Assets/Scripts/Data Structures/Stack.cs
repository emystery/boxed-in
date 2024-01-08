using System;

public class StackNode<T>
{
    private StackNode<T> next;
    private T data;

    public StackNode(T t)
    {
        next = null;
        data = t;
    }

    public StackNode<T> Next
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

public class GameStack<T>
{
    private StackNode<T> top;

    public GameStack()
    {
        top = null;
    }

    public void Push(T data)
    {
        StackNode<T> node = new StackNode<T>(data);
        node.Next = top;
        top = node;
    }

    public StackNode<T> Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack is empty. Cannot pop.");
            return null;
        }
        else
        {
            StackNode<T> node = top;
            top = top.Next;
            return node;
        }
    }

    public StackNode<T> Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack is empty. Cannot peek.");
            return null;
        }
        else
        {
            return top;
        }
    }

    public bool IsEmpty()
    {
        return top == null;
    }

    public void PrintStack()
    {
        StackNode<T> current = top;
        while (current != null)
        {
            Console.Write($"{current.Data} ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    public int Count()
    {
        int count = 0;
        StackNode<T> current = top;
        while (current != null)
        {
            count++;
            current = current.Next;
        }
        return count;
    }

    public void Clear()
    {
        top = null;
    }
}
