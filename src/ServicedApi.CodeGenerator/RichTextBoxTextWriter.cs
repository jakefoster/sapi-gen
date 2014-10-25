using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace org.ncore.ServicedApi.CodeGenerator
{
    public class RichTextBoxTextWriter : TextWriter
    {
        private readonly RichTextBox _target = null;
        private CharSendDelegate _invokeWrite;

        public RichTextBoxTextWriter( RichTextBox target )
        {
            _target = target;
            // NOTE: We have to explicitly declare our newline as \n or \r\n will be used and we'll 
            //  get *two* newlines in our RichTextBox for each Console.WriteLine.  JF
            base.NewLine = "\n";
            _invokeWrite = _sendChar;
        }

        public override void Write( char value )
        {
            lock( this )
            {
                base.Write( value );
                if( _target.InvokeRequired )
                {
                    _target.Invoke( _invokeWrite, new object[] { value } );
                }
                else
                {
                    _sendChar( value );
                }
            }
        }

        private delegate void CharSendDelegate( char value );
        private void _sendChar( char value )
        {
            _target.AppendText( value.ToString() );
            // NOTE: Non-obvious, but we want the RTB to scroll as text flows off the bottom of it.
            //  In order to do so we need to do the .Focus() and .SelectionStart bit below, however,
            //  we've found that we need to re-focus on the control that had focus previously otherwise
            //  other controls on the page (most notably the "Cancel" button will behave erratically.
            //  I believe this is because this method is being called so frequently (when each individual
            //  character is being writen to the RTB) and it's stealing focus from the other controls.
            //  In other words, the user *thinks* they clicked on "Cancel" but by the time the click event
            //  registered focus had been moved (by this method) back to the RTB.  So basically what we're
            //  doing here is finding the control on the form that currently has focus and setting focus
            //  back to that control after we've scrolled the RTB to show the newest text.  JF
            Control active = _target.FindForm().ActiveControl;
            _target.Focus();
            _target.SelectionStart = _target.Text.Length;
            active.Focus();
        }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }
}
