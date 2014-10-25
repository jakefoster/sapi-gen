using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace org.ncore.ServicedApi.CodeGenerator
{
    [DebuggerDisplay( "DisplayText = {DisplayText}, Key = {Key}, Checked = {Checked}" )]
    public class SelectionTreeNode : IList<SelectionTreeNode>
    {
        private List<SelectionTreeNode> _children = new List<SelectionTreeNode>();

        public string Key { get; set; }
        public string DisplayText { get; set; }
        public bool Checked { get; set; }
        public SelectionTreeNode Parent = null;

        public SelectionTreeNode( string key, string displayText = "", bool @checked = true )
        {
            this.Key = key;
            if( string.IsNullOrEmpty( displayText ) )
            {
                this.DisplayText = key;
            }
            else
            {
                this.DisplayText = displayText;
            }
            this.Checked = @checked;
        }

        public bool IsPathChecked()
        {
            if( this.Parent != null )
            {
                bool ancestorChecked = this.Parent.IsPathChecked();
                return ancestorChecked && this.Checked;                
            }
            else
            {
                return this.Checked;
            }
        }

        public SelectionTreeNode FindItem( string key )
        {
            SelectionTreeNode target = null;
            if( this.Key == key )
            {
                target = this;
            }
            else
            {
                foreach( SelectionTreeNode child in this )
                {
                    target = child.FindItem( key );
                    if( target != null )
                    {
                        break;
                    }
                }
            }
            return target;
        }

        public TreeNode ToTreeViewNode()
        {
            TreeNode treeViewNode = new TreeNode( this.DisplayText )
                                    {
                                        Name = this.Key,
                                        Checked = this.Checked
                                    };

            foreach( SelectionTreeNode selectionTreeNode in this )
            {
                treeViewNode.Nodes.Add( selectionTreeNode.ToTreeViewNode() );
            }
            return treeViewNode;
        }

        #region IList<SelectionTreeNode> Members

        public int IndexOf( SelectionTreeNode item )
        {
            return _children.IndexOf( item );
        }

        public void Insert( int index, SelectionTreeNode item )
        {
            item.Parent = this;
            _children.Insert( index, item );
        }

        public void RemoveAt( int index )
        {
            _children.RemoveAt( index );
        }

        public SelectionTreeNode this[ int index ]
        {
            get { return _children[ index ]; }
            set
            {
                value.Parent = this;
                _children[ index ] = value;
            }
        }

        #endregion

        #region ICollection<SelectionTreeNode> Members

        public void Add( SelectionTreeNode item )
        {
            item.Parent = this;
            _children.Add( item );
        }

        public void Clear()
        {
            _children.Clear();
        }

        public bool Contains( SelectionTreeNode item )
        {
            return _children.Contains( item );
        }

        public void CopyTo( SelectionTreeNode[] array, int arrayIndex )
        {
            _children.CopyTo( array, arrayIndex );
        }

        public int Count
        {
            get { return _children.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove( SelectionTreeNode item )
        {
            return _children.Remove( item );
        }

        #endregion

        #region IEnumerable<SelectionTreeNode> Members

        public IEnumerator<SelectionTreeNode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        #endregion
    }
}
