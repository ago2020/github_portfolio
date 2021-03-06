using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OnTopReplica {

    /// <summary>
    /// Extension methods for windows forms.
    /// </summary>
    static class WindowsFormsExtensions {

        /// <summary>
        /// Internationalizes the text of a LinkLabel instance.
        /// </summary>
        /// <param name="text">Main text without link. Contains a '%' character which will be replaced by the link.</param>
        /// <param name="linkText">Linked text.</param>
        public static void Internationalize(this LinkLabel label, string text, string linkText) {
            int linkIndex = text.IndexOf('%');
            if (linkIndex == -1) {
                //Shouldn't happen, but try to fail with meaningful text
                label.Text = text;
                return;
            }

            label.Text = text.Substring(0, linkIndex) + linkText + text.Substring(linkIndex + 1);
            label.LinkArea = new LinkArea(linkIndex, linkText.Length);
        }

        /// <summary>
        /// Makes a safe GUI invoke on a form's GUI thread.
        /// </summary>
        /// <param name="action">The action to be executed on the GUI's thread.</param>
        /// <remarks>
        /// If the form is invalid or disposed, the action is not performed.
        /// </remarks>
        public static void SafeInvoke(this Form form, Action action) {
            if (form == null || form.IsDisposed)
                return;

            if (form.InvokeRequired) {
                form.Invoke(action);
            }
            else {
                action();
            }
        }

        /// <summary>
        /// Checks whether a control contains a mouse pointer position in screen coordinates.
        /// </summary>
        /// <param name="screenCoordinates">Mouse pointer position in screen coordinates.</param>
        public static bool ContainsMousePointer(this Control ctrl, System.Drawing.Point screenCoordinates) {
            var bb = new System.Drawing.Rectangle(ctrl.Location, ctrl.Size);

            return bb.Contains(screenCoordinates);
        }

        /// <summary>
        /// Fixes the Windows Forms default font for a control and its nested controls.
        /// </summary>
        /// <remarks>
        /// Since Windows Forms defaults to Tahoma as the system font, this method is used
        /// to fix the default font for a control and all its children controls, setting it
        /// to the system message box font (Segoe on recent Windows releases).
        /// </remarks>
        public static void FixDefaultFont(this Control ctrl) {
            ctrl.Font = SystemFonts.MessageBoxFont;
            if (ctrl.Controls == null || ctrl.Controls.Count == 0)
                return;

            foreach (Control subCtrl in ctrl.Controls) {
                subCtrl.Font = SystemFonts.MessageBoxFont;

                if (subCtrl is GroupBox) {
                    subCtrl.FixDefaultFont();
                }
            }
        }

    }
}
