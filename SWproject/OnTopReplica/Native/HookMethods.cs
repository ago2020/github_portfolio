using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OnTopReplica.Native {

    /// <summary>
    /// Helpers for interop with native Windows hooks.
    /// </summary>
    static class HookMethods {

        static HookMethods() {
            WM_SHELLHOOKMESSAGE = RegisterWindowMessage("SHELLHOOK");
            if (WM_SHELLHOOKMESSAGE == 0) {
                Log.Write("Failed to register SHELLHOOK window message");
            }
        }

        public static int WM_SHELLHOOKMESSAGE {
            get;
            private set;
        }

        const int HSHELL_HIGHBIT = 0x8000;
        
        public const int HSHELL_WINDOWCREATED = 1;
        public const int HSHELL_WINDOWDESTROYED = 2;
        public const int HSHELL_WINDOWACTIVATED = 4;
        public const int HSHELL_REDRAW = 6;
        public const int HSHELL_RUDEAPPACTIVATED = (HSHELL_WINDOWACTIVATED | HSHELL_HIGHBIT);
        public const int HSHELL_FLASH = (HSHELL_REDRAW | HSHELL_HIGHBIT);

        /// <summary>
        /// Registers the WM_ID for a window message.
        /// </summary>
        /// <param name="wndMessageName">Name of the window message.</param>
        [DllImport("User32.dll")]
        public static extern int RegisterWindowMessage(string wndMessageName);

        /// <summary>
        /// Registers a window as a shell hook window.
        /// </summary>
        [DllImport("User32.dll")]
        public static extern bool RegisterShellHookWindow(IntPtr hwnd);

        /// <summary>
        /// Deregisters a window as a shell hook window.
        /// </summary>
        [DllImport("User32.dll")]
        public static extern bool DeregisterShellHookWindow(IntPtr hwnd);

    }
}
