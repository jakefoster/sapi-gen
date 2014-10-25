using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace org.ncore.ServicedApi.CodeGenerator
{
    public partial class ExceptionTreeForm : Form
    {
        private XDocument _exceptionGraph;

        public ExceptionTreeForm( XDocument exceptionGraph )
        {
            _exceptionGraph = exceptionGraph;

            InitializeComponent();
        }

        private void ExceptionGraphForm_Load( object sender, EventArgs e )
        {
            _exceptionGraphRichTextBox.Text = _exceptionGraph.ToString().Replace( Environment.NewLine, "\n" );
        }
    }
}
