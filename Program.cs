using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

//TODO - make linked list generic to accept all types

namespace LinkedLists
{    
    class Program
    {
        static void Main()
        {
            CustLinkedList linkyBoy = new(1);
            linkyBoy.PrintAllNodes();
            linkyBoy.Append(2);
            linkyBoy.Append(3);
            linkyBoy.PrintAllNodes();
            Console.WriteLine();
            linkyBoy.Prepend(0);
            linkyBoy.PrintAllNodes();
            Console.WriteLine();
            linkyBoy.Insert(10, 5);
            linkyBoy.PrintAllNodes();
            Console.WriteLine();
            Console.WriteLine($"Count is {linkyBoy.count}");
            linkyBoy.DataAt(4);
            Console.WriteLine();
            linkyBoy.Delete(4);
            linkyBoy.PrintAllNodes();
        }
    }

    public class Node(int? num)
    {
        public int? _num = num;
        public Node? next;
    }

    /// <summary>
    /// Optional parameter sets data of first node, expects <c>int</c>
    /// </summary>
    public class CustLinkedList(int? data = null)
    {
        private Node head = new(data);
        public int count = 1;

        /// <summary>
        /// Prints value stored in all nodes
        /// </summary>
        public void PrintAllNodes()
        {
            if (head == null)
            {
                Console.WriteLine("No list exists");
                return;
            }

            Node current = head;
            while (current.next != null)
            {
                Console.WriteLine(current._num);
                current = current.next;
            }
            Console.WriteLine(current._num);
        }

        /// <summary>
        /// Appends a value to the end of the linked list<br/>
        /// <paramref name="data"/> expects <c>int</c>.
        /// </summary>
        /// <param name="data">data to be inserted</param>
        public void Append(int data)
        {
            count++;
            if (head == null)
            {
                Node head = new(data);
            }
            else
            {
                Node current = head;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = new Node(data);
            }
        }

        /// <summary>
        /// Adds a value to the front of the linked list<br/>
        /// <paramref name="data"/> expects <c>int</c>.
        /// </summary>
        /// <param name="data">data to be inserted</param>
        public void Prepend(int data)
        {
            count++;
            if (head._num == null)
            {
                head._num = data;
                return;
            }
            Node temp = new(data);
            temp.next = head;
            head = temp;
        }

        /// <summary>
        /// Checks if <paramref name="index"/> is in range.
        /// </summary>
        /// <param name="index">Expected to be <c>int</c></param>
        /// <returns></returns>
        private bool InRange(int index)
        {
            if (index >= count || index < 0)
            {
                Console.WriteLine($"Index {index} out of range");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Inserts <paramref name="data"/> at <paramref name="index"/>
        /// </summary>
        /// <param name="data">value to be inserted</param>
        /// <param name="index">index to insert new node</param>
        public void Insert(int data, int index)
        {
            if (!InRange(index))
            {
                return;
            }
            count++;
            Node current = head;
            for (int i = 0; i < index -1; i++)
            {
                current = current.next!;
            }

            Node temp = new(data)
            {
                next = current.next
            };
            current.next = temp;

            // This could be re-written to eliminate need for 'count' like the following
            // although, count is nice to have in the case that I want a 'Length()' type record
            /*
            for (int i = 0; i < index; i++)
            {
                if (current == null && i < index)
                {
                    Console.WriteLine($"Index {index} out of range");
                    return;
                }
                current = current.next;
            }

            Node temp = new(data){
                next = current.next
            };
            current.next = temp;
            */
            //But this would have lower performance because we are checking the 
            //inRange condition each loop instead of once at the beginning

        }

        public void Delete(int index)
        {
            if (!InRange(index))
            {
                return;
            }

            Node current = head;
            for (int i = 0; i < index - 1; i++)
            {
                current = current.next!;
            }
            if (current.next!.next == null)
            {
                current.next = null;
            }
            else{
                current.next = current.next.next;
            }
            count--;

        }

        /// <summary>
        /// Prints the data stored at <paramref name="index"/>
        /// </summary>
        /// <param name="index">expects an integer</param>
        public void DataAt(int index)
        {
            if (!InRange(index))
            {
                return;
            }
            Node current = head;
            for (int i = 0; i < index; i++)
            {
                if (current.next != null)
                {
                    current = current.next;
                }
            }
            if (current != null)
            {
                Console.WriteLine($"The value at position {index} is {current._num}");
            }
        }
    }
}