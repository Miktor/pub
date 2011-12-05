using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

namespace Parsec.Configuration
{
    class Mail
    {
        public bool AddRecipientTo(string email)
        {
            return AddRecipient(email, HowTo.MAPI_TO);
        }

        public bool AddRecipientCC(string email)
        {
            return AddRecipient(email, HowTo.MAPI_CC);
        }

        public bool AddRecipientBCC(string email)
        {
            return AddRecipient(email, HowTo.MAPI_BCC);
        }

        public bool AddRecipientORIG(string email)
        {
            return AddRecipient(email, HowTo.MAPI_ORIG);
        }

        public void AddAttachment(string strAttachmentFileName)
        {
            m_attachments.Add(strAttachmentFileName);
        }

        public int SendMailPopup(string strSubject, string strBody)
        {
            return SendMail(strSubject, strBody, MAPI_ALLOW_OTHERS | MAPI_LOGON_UI | MAPI_NEW_SESSION);
        }

        [DllImport("MAPI32.DLL")]
        static extern int MAPISendMail(IntPtr sess, IntPtr hwnd,
            MapiMessage message, int flg, int rsv);

        int SendMail(string strSubject, string strBody, int how)
        {
            MapiMessage msg = new MapiMessage();
            msg.subject = strSubject;
            msg.noteText = strBody;

            msg.recips = GetRecipients(out msg.recipCount);
            msg.files = GetAttachments(out msg.fileCount);

            m_lastError = MAPISendMail(
                new IntPtr(0),
                System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle,
                msg, how, 0);           

            Cleanup(ref msg);

            return m_lastError;
        }

        bool AddRecipient(string email, HowTo howTo)
        {
            MapiRecipDesc recipient = new MapiRecipDesc();

            recipient.recipClass = (int)howTo;
            recipient.name = email;
            m_recipients.Add(recipient);

            return true;
        }

        IntPtr GetRecipients(out int recipCount)
        {
            recipCount = 0;
            if (m_recipients.Count == 0)
                return IntPtr.Zero;

            int size = Marshal.SizeOf(typeof(MapiRecipDesc));
            IntPtr intPtr = Marshal.AllocHGlobal(m_recipients.Count * size);

            int ptr = (int)intPtr;
            foreach (MapiRecipDesc mapiDesc in m_recipients)
            {
                Marshal.StructureToPtr(mapiDesc, (IntPtr)ptr, false);
                ptr += size;
            }

            recipCount = m_recipients.Count;
            return intPtr;
        }

        IntPtr GetAttachments(out int fileCount)
        {
            fileCount = 0;
            if (m_attachments == null)
                return IntPtr.Zero;

            if ((m_attachments.Count <= 0) || (m_attachments.Count >
                maxAttachments))
                return IntPtr.Zero;

            int size = Marshal.SizeOf(typeof(MapiFileDesc));
            IntPtr intPtr = Marshal.AllocHGlobal(m_attachments.Count * size);

            MapiFileDesc mapiFileDesc = new MapiFileDesc();
            mapiFileDesc.position = -1;
            int ptr = (int)intPtr;

            foreach (string strAttachment in m_attachments)
            {
                mapiFileDesc.name = Path.GetFileName(strAttachment);
                mapiFileDesc.path = strAttachment;
                Marshal.StructureToPtr(mapiFileDesc, (IntPtr)ptr, false);
                ptr += size;
            }

            fileCount = m_attachments.Count;
            return intPtr;
        }

        void Cleanup(ref MapiMessage msg)
        {
            int size = Marshal.SizeOf(typeof(MapiRecipDesc));
            int ptr = 0;

            if (msg.recips != IntPtr.Zero)
            {
                ptr = (int)msg.recips;
                for (int i = 0; i < msg.recipCount; i++)
                {
                    Marshal.DestroyStructure((IntPtr)ptr,
                        typeof(MapiRecipDesc));
                    ptr += size;
                }
                Marshal.FreeHGlobal(msg.recips);
            }

            if (msg.files != IntPtr.Zero)
            {
                size = Marshal.SizeOf(typeof(MapiFileDesc));

                ptr = (int)msg.files;
                for (int i = 0; i < msg.fileCount; i++)
                {
                    Marshal.DestroyStructure((IntPtr)ptr,
                        typeof(MapiFileDesc));
                    ptr += size;
                }
                Marshal.FreeHGlobal(msg.files);
            }

            m_recipients.Clear();
            m_attachments.Clear();
            //m_lastError = 0;
        }    
        
        List<MapiRecipDesc> m_recipients = new
            List<MapiRecipDesc>();
        List<string> m_attachments = new List<string>();
        int m_lastError = 0;

        const int MAPI_LOGON_UI = 0x00000001;
        const int MAPI_NEW_SESSION = 0x00000002;
        const int MAPI_ALLOW_OTHERS = 0x00000008;
        const int MAPI_EXPLICIT_PROFILE = 0x00000010;
        const int MAPI_EXTENDED = 0x00000020;
        const int MAPI_USE_DEFAULT = 0x00000050;
        const int MAPI_FORCE_DOWNLOAD = 0x00001000;
        const int MAPI_SERVICE_UI_ALWAYS = 0x00002000;
        const int MAPI_NO_MAIL = 0x00008000;
        const int MAPI_PASSWORD_UI = 0x00020000;
        const int MAPI_TIMEOUT_SHORT = 0x00100000;
        const int maxAttachments = 20;
        enum HowTo { MAPI_ORIG = 0, MAPI_TO, MAPI_CC, MAPI_BCC };
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class MapiMessage
    {
        public int reserved;
        public string subject;
        public string noteText;
        public string messageType;
        public string dateReceived;
        public string conversationID;
        public int flags;
        public IntPtr originator;
        public int recipCount;
        public IntPtr recips;
        public int fileCount;
        public IntPtr files;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class MapiFileDesc
    {
        public int reserved;
        public int flags;
        public int position;
        public string path;
        public string name;
        public IntPtr type;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class MapiRecipDesc
    {
        public int reserved;
        public int recipClass;
        public string name;
        public string address;
        public int eIDSize;
        public IntPtr entryID;
    }
   
}