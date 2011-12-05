using System;
using System.Collections.Generic;
using Mogre;

namespace WorldEditor.UI
{
    class ExeptionHandler : Singleton<ExeptionHandler>
    {
        Log mLog = null;
        public Dictionary<object, Exception> Exeptions = new Dictionary<object, Exception>();
        private ExeptionHandler()
        {
            mLog = LogManager.Singleton.CreateLog("WorldEditor");
        }

        public void CatchException(Exception ex, object sender)
        {            
            Exeptions.Add(sender, ex);
            mLog.LogMessage("New error from :" + sender.ToString() + Environment.NewLine + ex.ToString());
        }
    }
}
