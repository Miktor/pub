using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Configer
{
    public partial class Main : Form
    {
		[DllImport("kernel32", SetLastError=true)]
        static extern int WritePrivateProfileString(string section, string key, string value, string fileName);
        [DllImport("kernel32", SetLastError=true)]
		static extern int WritePrivateProfileString(string section, string key, int value, string fileName);
        [DllImport("kernel32", SetLastError=true)]
        static extern int WritePrivateProfileString(string section, int key, string value, string fileName);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder result, int size, string fileName);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, int key, string defaultValue, [MarshalAs(UnmanagedType.LPArray)] byte[] result, int size, string fileName);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(int section, string key, string defaultValue, [MarshalAs(UnmanagedType.LPArray)] byte[] result, int size, string fileName);

        public void SetValue(string section, string entry, object value)
		{
			// If the value is null, remove the entry
			if (value == null)
			{
				RemoveEntry(section, entry);
				return;
			}

            WritePrivateProfileString(section, entry, value.ToString(), INIfName);
		}

        public object GetValue(string section, string entry)
		{
			for (int maxSize = 250; true; maxSize *= 2)
			{
				StringBuilder result = new StringBuilder(maxSize);
            	int size = GetPrivateProfileString(section, entry, "", result, maxSize, INIfName);
				
				if (size < maxSize - 1)
				{					
					if (size == 0)
						return null;
					return result.ToString();
				}
			}
		}

		public void RemoveEntry(string section, string entry)
		{
            WritePrivateProfileString(section, entry, 0, INIfName);
		}

		public string[] GetEntryNames(string section)
		{
			// Verify the section exists
			
			for (int maxSize = 500; true; maxSize *= 2)
			{
				byte[] bytes = new byte[maxSize];				
            	int size = GetPrivateProfileString(section, 0, "", bytes, maxSize, INIfName);
				
				if (size < maxSize - 2)
				{
					// Convert the buffer to a string and split it
					string entries = Encoding.ASCII.GetString(bytes, 0, size - (size > 0 ? 1 : 0));			
					if (entries == "")
						return new string[0];
		            return entries.Split(new char[] {'\0'});			
				}
			}
		}

		public string[] GetSectionNames()
		{
            for (int maxSize = 500; true; maxSize *= 2)
			{
				byte[] bytes = new byte[maxSize];				
            	int size = GetPrivateProfileString(0, "", "", bytes, maxSize, INIfName);
				
				if (size < maxSize - 2)
				{
					// Convert the buffer to a string and split it
					string sections = Encoding.ASCII.GetString(bytes, 0, size - (size > 0 ? 1 : 0));			
					if (sections == "")
						return new string[0];
		            return sections.Split(new char[] {'\0'});			
				}
			}
		}
	}
}

