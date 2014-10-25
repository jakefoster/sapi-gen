using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace org.ncore.ServicedApi.CodeGenerator
{
    public class SelectionTree : List<SelectionTreeNode>
    {
        public SelectionTree()
        {
        }

        public SelectionTree( TreeView treeView )
        {
            // TODO: Should add some logic to make sure that every node in the tree has a unique key.  JF
            foreach( TreeNode childTreeViewNode in treeView.Nodes )
            {
                SelectionTreeNode selectionTreeNode = _buildSelectionTreeNode( childTreeViewNode );
                this.Add( selectionTreeNode );
            }
        }

        public void PopulateTreeView( TreeView treeView )
        {
            treeView.Nodes.Clear();

            foreach( SelectionTreeNode selectionTreeNode in this )
            {
                treeView.Nodes.Add( selectionTreeNode.ToTreeViewNode() );
            }
        }

        public bool IsPathChecked( string key )
        {
            SelectionTreeNode node = FindItem( key );

            return false;
        }

        public SelectionTreeNode FindItem( string key )
        {
            SelectionTreeNode target = null;
            foreach( SelectionTreeNode node in this )
            {
                target = node.FindItem( key );
                if( target != null )
                {
                    break;
                }
            }
            return target;
        }

        private SelectionTreeNode _buildSelectionTreeNode( TreeNode treeViewNode )
        {
            SelectionTreeNode selectionTreeNode = new SelectionTreeNode( treeViewNode.Name, treeViewNode.Text, treeViewNode.Checked );
            foreach( TreeNode childTreeViewNode in treeViewNode.Nodes )
            {
                SelectionTreeNode childSelectionTreeNode = _buildSelectionTreeNode( childTreeViewNode );
                selectionTreeNode.Add( childSelectionTreeNode );
            }
            return selectionTreeNode;
        }
    }
}
