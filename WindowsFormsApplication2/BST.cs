using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//code modified from https://msdn.microsoft.com/en-us/library/ms379572%28v=vs.80%29.aspx

namespace RecursiveSearchCS
{
    
    public class DuplicateInfo
    {
        private List<duplicate> duplicates;
        private double totalDuplicateSize;
        private int duplicateCount;
        public enum duplicateType { copy, duplicate };
        
        public struct duplicate
        {
            public HashInfo duplicateA;
            public HashInfo duplicateB;
            public String message;
            public string hashCode;
            public duplicateType type;
        }

        public DuplicateInfo()
        {
            duplicates = new List<duplicate>();
            totalDuplicateSize = 0.0;
            duplicateCount = 0;
        }

        public double getTotalSize ()
        {
            return totalDuplicateSize;
        }

        public int getCount()
        {
            return duplicateCount;
        }
        
        public void clear ()
        {
            duplicates.Clear();
            totalDuplicateSize = 0;
            duplicateCount = 0;
        }

        public List<duplicate> getDuplicateList()
        {
            return duplicates;
        }

        public void addDuplicate(HashInfo data, BinaryTreeNode current)
        {
            duplicate aDuplicate = new duplicate();

            if (Regex.IsMatch(current.Value.GetFileName(), @".\(\d\)."))
            {
                aDuplicate.duplicateA = data;
                aDuplicate.duplicateB = current.Value;
                aDuplicate.message = data.GetFileName() + " has a copy: " + current.Value.GetFileName();
                aDuplicate.type = duplicateType.copy;

                //swap node value so the orig file is the one in the bst not the copy "filename (#).ext"
                current.swapNodeValue(data);
            }
            else if ((Regex.IsMatch(data.GetFileName(), @".\(\d\).")))
            {
                aDuplicate.duplicateA = current.Value;
                aDuplicate.duplicateB = data;
                aDuplicate.message = current.Value.GetFileName() + " has a copy: " + data.GetFileName();
                aDuplicate.type = duplicateType.copy;
            }
            else
            {
                aDuplicate.duplicateA = current.Value;
                aDuplicate.duplicateB = data;
                aDuplicate.message = current.Value.GetFileName() + " has a duplicate: " + data.GetFileName();
                aDuplicate.type = duplicateType.duplicate;
            }

            aDuplicate.hashCode = data.GetHashString();
            duplicates.Add(aDuplicate);
            duplicateCount++;
            totalDuplicateSize += new FileInfo(data.GetFileName()).Length / 1024.0;
        }

    }
    
    public class HashInfo 
    {
        public string hash;
        public string filename;
        public string GetFileName() {
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
        private DuplicateInfo duplicates = new DuplicateInfo();

        public DuplicateInfo getDuplicates()
        {
            return duplicates;
        }
        public BinaryTree()
        {
            root = null;
        }

        public virtual void Clear()
        {
            root = null;
            duplicates.clear();
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
                    duplicates.addDuplicate(data, current);
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
        
        public void swapNodeValue(HashInfo data){
            this.Value = data;
        }

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

