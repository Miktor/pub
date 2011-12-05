using System;
using System.Windows.Forms;

namespace MyWorldEditor
{
    public class RedrowEWent : Singleton<RedrowEWent>
    {
        public delegate void RedrowEwentHandler(object sender, EventArgs e);

        public event RedrowEwentHandler NeedRedrow;

        private RedrowEWent()
        {

        }

        public virtual void AskRedrow()
        {
            if (NeedRedrow != null)
                NeedRedrow(this, EventArgs.Empty);
        }

        public virtual void AskRedrow(EventArgs e)
        {
            if (NeedRedrow != null)
                NeedRedrow(this, e);
        }
    }
}
