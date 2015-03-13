using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//code modified from https://msdn.microsoft.com/en-us/library/ms379572%28v=vs.80%29.aspx

namespace RecursiveSearchCS
{
    public class HashInfo 
    {
        public string hash;
        public string filename;
        public override string ToString() {
            return filename;
        }
        public string GetHashString()
        {
            return hash;
        }
    }
    
    
    public class BinaryTree
    {
        private BinaryTreeNode root;
        public ArrayList duplicates = new ArrayList();
        public double totalDuplicateSize;
        public int duplicateCount = 0;

        public BinaryTree()
        {
            root = null;
        }

        public virtual void Clear()
        {
            root = null;
        }

        public BinaryTreeNode Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }

    
        public virtual void Add(HashInfo data)
        {
            BinaryTreeNode n = new BinaryTreeNode(data);
            int result;
            BinaryTreeNode current = root, parent = null;

            while (current != null)
            {
                result = String.Compare(current.Value.GetHashString(), data.GetHashString());
                if (result == 0)
                {
                    duplicateCount++;
                    duplicates.Add(current.Value.ToString() + " copy of: " + data.ToString());
                    totalDuplicateSize += new FileInfo(data.ToString()).Length / 1024.0;
                    return;
                }
                else if (result > 0)
                {

                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
            }

            if (parent == null)
                root = n;
            else
            {
                result = String.Compare(parent.Value.GetHashString(), data.GetHashString());
                if (result > 0)
                    parent.Left = n;
                else
                    parent.Right = n;
            }
        }
    }
    
    public class BinaryTreeNode : Node
    {
        public BinaryTreeNode(HashInfo data) : base(data, null) { }

        public BinaryTreeNode Left
        {
            get
            {
                if (base.Neighbors == null)
                    return null;
                else
                    return (BinaryTreeNode)base.Neighbors[0];
            }
            set
            {
                if (base.Neighbors == null)
                    base.Neighbors = new NodeList(2);

                base.Neighbors[0] = value;
            }
        }

        public BinaryTreeNode Right
        {
            get
            {
                if (base.Neighbors == null)
                    return null;
                else
                    return (BinaryTreeNode)base.Neighbors[1];
            }
            set
            {
                if (base.Neighbors == null)
                    base.Neighbors = new NodeList(2);

                base.Neighbors[1] = value;
            }
        }
    }
    
    public class NodeList : Collection<Node>
{
 
    public NodeList(int initialSize)
    {
        // Add the specified number of items
        for (int i = 0; i < initialSize; i++)
            base.Items.Add(default(Node));
    }
}
    
    public class Node
{
        // Private member-variables
        private HashInfo data;
        private NodeList neighbors = null;

        public Node() {}
        public Node(HashInfo data) : this(data, null) {}
        public Node(HashInfo data, NodeList neighbors)
        {
            this.data = data;
            this.neighbors = neighbors;
        }

        public HashInfo Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        protected NodeList Neighbors
        {
            get
            {
                return neighbors;
            }
            set
            {
                neighbors = value;
            }
        }
    }
}

