using System;

namespace WorldEditor
{
    public class RedrowEwent : Singleton<RedrowEwent>
    {
        public delegate void RedrowEwentHandler(object sender, EventArgs e);

        public event RedrowEwentHandler NeedRedrow;

        private RedrowEwent()
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
