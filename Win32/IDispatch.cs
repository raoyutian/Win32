using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Win32
{
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00020400-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IDispatch
	{
		int GetTypeInfoCount();
		[return: MarshalAs(UnmanagedType.Interface)]
		ITypeInfo GetTypeInfo([MarshalAs(UnmanagedType.U4)][In] int iTInfo, [MarshalAs(UnmanagedType.U4)][In] int lcid);
		int GetIDsOfNames([In] ref Guid riid, [MarshalAs(UnmanagedType.LPArray)][In] string[] rgszNames, [MarshalAs(UnmanagedType.U4)][In] int cNames, [MarshalAs(UnmanagedType.U4)][In] int lcid, [MarshalAs(UnmanagedType.LPArray)][Out] int[] rgDispId);
	}
}
