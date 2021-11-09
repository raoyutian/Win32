
using System;
using System.Runtime.InteropServices;
using System.Text;
using Accessibility;

namespace Win32
{/// <summary>
 ///Oleacc相关API
 /// </summary>
    public static partial class Oleacc
    {
		[DllImport("oleacc.dll")]
        public static extern uint WindowFromAccessibleObject(IAccessible pacc, ref IntPtr phwnd);

        [DllImport("oleacc.dll")]
        public static extern int AccessibleObjectFromWindow(IntPtr hwnd, uint dwObjectID, ref Guid iid, [In, Out, MarshalAs(UnmanagedType.Interface)] ref object ppvObject);
        
		[DllImport("oleacc.dll")]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static extern object AccessibleObjectFromWindow(IntPtr hwnd, uint dwObjectID, [MarshalAs(UnmanagedType.LPStruct)][In] Guid riid);

        [DllImport("oleacc.dll")]
        public static extern IntPtr AccessibleObjectFromPoint(POINT pt, [Out, MarshalAs(UnmanagedType.Interface)] out IAccessible accObj, [Out] out object ChildID);
        
        [DllImport("oleacc.dll")]
        public static extern int LresultFromObject(object riid, Int32 wParam, ref IntPtr ptr);

        [DllImport("oleacc.dll")]
        public static extern int ObjectFromLresult(int lResult, ref Guid riid, Int32 wParam, ref IntPtr ptr);

        [DllImport("oleacc.dll", CharSet = CharSet.Unicode)]
		public static extern uint GetRoleText(AccessibilityRole dwRole, [Out] out StringBuilder lpszRole, uint cchRoleMax);

        [DllImport("oleacc.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetStateText(AccessibilityState dwStateBit, [Out] StringBuilder lpszStateBit, uint cchStateBitMax);

        [DllImport("oleacc.dll")]
        public static extern uint AccessibleChildren(IAccessible paccContainer, int iChildStart, int cChildren, [Out] object[] rgvarChildren, out int pcObtained);

        public enum OBJID : uint
        {
            WINDOW = 0x00000000,
            SYSMENU = 0xFFFFFFFF,
            TITLEBAR = 0xFFFFFFFE,
            MENU = 0xFFFFFFFD,
            CLIENT = 0xFFFFFFFC,
            VSCROLL = 0xFFFFFFFB,
            HSCROLL = 0xFFFFFFFA,
            SIZEGRIP = 0xFFFFFFF9,
            CARET = 0xFFFFFFF8,
            CURSOR = 0xFFFFFFF7,
            ALERT = 0xFFFFFFF6,
            SOUND = 0xFFFFFFF5,
        }
       
        public enum OLECMDF
        {
            OLECMDF_DEFHIDEONCTXTMENU = 0x20,
            OLECMDF_ENABLED = 2,
            OLECMDF_INVISIBLE = 0x10,
            OLECMDF_LATCHED = 4,
            OLECMDF_NINCHED = 8,
            OLECMDF_SUPPORTED = 1
        }

        public enum OLECMDID
        {
            OLECMDID_PAGESETUP = 8,
            OLECMDID_PRINT = 6,
            OLECMDID_PRINTPREVIEW = 7,
            OLECMDID_PROPERTIES = 10,
            OLECMDID_SAVEAS = 4
        }

        public enum OLECMDEXECOPT
        {
            OLECMDEXECOPT_DODEFAULT,
            OLECMDEXECOPT_PROMPTUSER,
            OLECMDEXECOPT_DONTPROMPTUSER,
            OLECMDEXECOPT_SHOWHELP
        }
		
		public enum AccRoles
		{
			ROLE_SYSTEM_ALERT = 8,
			ROLE_SYSTEM_ANIMATION = 54,
			ROLE_SYSTEM_APPLICATION = 14,
			ROLE_SYSTEM_BORDER = 19,
			ROLE_SYSTEM_BUTTONDROPDOWN = 56,
			ROLE_SYSTEM_BUTTONDROPDOWNGRID = 58,
			ROLE_SYSTEM_BUTTONMENU = 57,
			ROLE_SYSTEM_CARET = 7,
			ROLE_SYSTEM_CELL = 29,
			ROLE_SYSTEM_CHARACTER = 32,
			ROLE_SYSTEM_CHART = 17,
			ROLE_SYSTEM_CHECKBUTTON = 44,
			ROLE_SYSTEM_CLIENT = 10,
			ROLE_SYSTEM_CLOCK = 61,
			ROLE_SYSTEM_COLUMN = 27,
			ROLE_SYSTEM_COLUMNHEADER = 25,
			ROLE_SYSTEM_COMBOBOX = 46,
			ROLE_SYSTEM_CURSOR = 6,
			ROLE_SYSTEM_DIAGRAM = 53,
			ROLE_SYSTEM_DIAL = 49,
			ROLE_SYSTEM_DIALOG = 18,
			ROLE_SYSTEM_DOCUMENT = 15,
			ROLE_SYSTEM_DROPLIST = 47,
			ROLE_SYSTEM_EQUATION = 55,
			ROLE_SYSTEM_GRAPHIC = 40,
			ROLE_SYSTEM_GRIP = 4,
			ROLE_SYSTEM_GROUPING = 20,
			ROLE_SYSTEM_HELPBALLOON = 31,
			ROLE_SYSTEM_HOTKEYFIELD = 50,
			ROLE_SYSTEM_INDICATOR = 39,
			ROLE_SYSTEM_IPADDRESS = 63,
			ROLE_SYSTEM_LINK = 30,
			ROLE_SYSTEM_LIST = 33,
			ROLE_SYSTEM_LISTITEM,
			ROLE_SYSTEM_MENUBAR = 2,
			ROLE_SYSTEM_MENUITEM = 12,
			ROLE_SYSTEM_MENUPOPUP = 11,
			ROLE_SYSTEM_OUTLINE = 35,
		
			ROLE_SYSTEM_OUTLINEBUTTON = 64,
		
			ROLE_SYSTEM_OUTLINEITEM = 36,
			
			ROLE_SYSTEM_PAGETAB,
		
			ROLE_SYSTEM_PAGETABLIST = 60,
		
			ROLE_SYSTEM_PANE = 16,
		
			ROLE_SYSTEM_PROGRESSBAR = 48,
		
			ROLE_SYSTEM_PROPERTYPAGE = 38,
			
			ROLE_SYSTEM_PUSHBUTTON = 43,
		
			ROLE_SYSTEM_RADIOBUTTON = 45,
			
			ROLE_SYSTEM_ROW = 28,
			
			ROLE_SYSTEM_ROWHEADER = 26,
		
			ROLE_SYSTEM_SCROLLBAR = 3,
			
			ROLE_SYSTEM_SEPARATOR = 21,
		
			ROLE_SYSTEM_SLIDER = 51,
		
			ROLE_SYSTEM_SOUND = 5,
			
			ROLE_SYSTEM_SPINBUTTON = 52,
		
			ROLE_SYSTEM_SPLITBUTTON = 62,
		
			ROLE_SYSTEM_STATICTEXT = 41,
		
			ROLE_SYSTEM_STATUSBAR = 23,
			
			ROLE_SYSTEM_TABLE,
		
			ROLE_SYSTEM_TEXT = 42,
		
			ROLE_SYSTEM_TITLEBAR = 1,
		
			ROLE_SYSTEM_TOOLBAR = 22,
		
			ROLE_SYSTEM_TOOLTIP = 13,
		
			ROLE_SYSTEM_WHITESPACE = 59,
		
			ROLE_SYSTEM_WINDOW = 9
		}
	}
}
