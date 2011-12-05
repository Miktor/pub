using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Configer
{
    public class ComponentState
    {
        private Control cntrl;
        private bool minimized;
        private bool fixsed;
        private bool unResizeble;
        private int minHeight;
        private int needingHeight;
        private int index;
        private int MaximumHeight;

        public static int a = 1;
        private int v;
        public static int maxVel = 30;

        public ComponentState(Control _cntrl,bool resize)
        {
            cntrl = _cntrl;

            minimized = true;
            fixsed = false;
            unResizeble = !resize;
            needingHeight = cntrl.Height;
            index = cntrl.TabIndex;
            MaximumHeight = 1000;

            minHeight = _cntrl.Height;
            v = 0;
        }

        public void changeState()
        {
            if (unResizeble != true)
            {
                this.minimized = !this.minimized;
                if (minimized)
                    height -= 1;
                else
                    height += 1;
            }
        }

        public int height
        {
            get { return cntrl.Height; }
            set { cntrl.Height = value; }
        }
        public int GetMinHeight
        {
            get { return minHeight; }
        }
        public int NeedingHeight
        {
            get { return needingHeight; }
            set
            {
                if (value < MaximumHeight)
                    needingHeight = value;
                else
                    needingHeight = MaximumHeight;
            }
        }
        public int MaxHeight
        {
            get { return cntrl.Height; }
            set { cntrl.Height = value; }
        }

        public Control control
        {
            get { return cntrl; }           
        }

        public int X
        {
            get { return cntrl.Location.X; }
            set { cntrl.Location = new System.Drawing.Point(value, Y); }
        }
        public int Y
        {
            get { return cntrl.Location.Y; }
            set { cntrl.Location = new System.Drawing.Point(X, value); }
        }

        public bool state
        {
            get { return minimized; }
        }

        public bool Fixed
        {
            get { return fixsed; }
            set { fixsed = true; }
        }
        public bool resize
        {
            get { return !unResizeble; }
        }

        public int GetIndex
        {
            get { return index; }
        }

        public int Vel
        {
            get { return v; }
            set { v = value; }
        }
    }

    class SlideComponents
    {   
        Control MainControlCol;
        List<ComponentState> ComponentList;                

        public SlideComponents(Control.ControlCollection control,Control[] handler)
        {
            ComponentList = new List<ComponentState> { };
            MainControlCol = control[control.Count - 1].Parent;
            ComponentState thisState;
            foreach (Control cntrl in control)
            {
                bool resize = true;
                thisState = new ComponentState(cntrl, resize);

                if (cntrl.AccessibleRole == AccessibleRole.ButtonMenu)
                    resize = false;
                thisState = new ComponentState(cntrl, resize); 
                ComponentList.Add(thisState);
            }
            foreach (Control h in handler)
            {
                h.MouseWheel += new MouseEventHandler(Scrool);                
                h.MouseDoubleClick += new MouseEventHandler(LockControl);

            }
        }

        private void CalculateState(ComponentState state)
        {
            if (state.NeedingHeight != state.height)          
                state.height += getVelosoty(state);                       
        }

        private void CalculatePositions()
        {
            int lenght = 0;
            int i=1;
            
            while (i <= ComponentList.Count)
            {
                for (int j = 0; j < ComponentList.Count; j++)
                {
                    ComponentState state = ComponentList[j];
                    if (state.GetIndex == i)
                    {
                        if (state.Y != lenght)
                            state.Y = lenght;
                        lenght += state.height;
                        i++;
                    }
                }
            }            
        }

        //private void ChangeState(object sendler, MouseEventArgs args)
        //{
        //    Control con = (sendler as Control).Parent;
        //    ComponentList.Find(delegate(ComponentState st)
        //    {
        //        if (st.control == con)
        //            return true;
        //        else
        //            return false;
        //    }).changeState();
        //    foreach (ComponentState state in ComponentList)
        //        if (state.control != con && !state.Fixed && state.state == false)
        //            state.changeState();
        //    foreach (ComponentState state in ComponentList)
        //        state.NeedingHeight = NeedingHeiht(state);

        //}

        private void LockControl(object sendler, MouseEventArgs args)
        {
            Control con = (sendler as Control).Parent;
            ComponentState list = ComponentList.Find(delegate(ComponentState st)
            {
                if (st.control == con)
                    return true;
                else
                    return false;
            });
            list.changeState();
            list.Fixed = true;

            foreach (ComponentState state in ComponentList)
                if (state.control != con && !state.Fixed && state.state == false)
                {
                    state.changeState();                    
                }
            foreach (ComponentState state in ComponentList)
                state.NeedingHeight = NeedingHeiht(state);
            

            ////Твой текст
            //string s = "Hello";
            ////Размер шрифта
            //Size size = g.MeasureString(s, f);

        }

        private void Scrool(object sendler, MouseEventArgs args)
        {
            Control con = (sendler as Control).Parent;
            int scrool = args.Delta * 10;
            
            ComponentList.Find(delegate(ComponentState st)
            {
                if (st.control == con)
                    return true;
                else
                    return false;
            }).NeedingHeight += scrool;
        }

        public void TimerTick(object sender, EventArgs args)
        {
            foreach(ComponentState state in ComponentList)
                if(!state.state)
                    CalculateState(state);
            foreach (ComponentState state in ComponentList)
                if (state.state)
                    CalculateState(state);
            foreach (ComponentState state in ComponentList)
                CalculatePositions();
        }

        private int getVelosoty(ComponentState state)
        {        
            int a = 0;

            if (state.NeedingHeight > state.height)
                a = ComponentState.a;
            else
                a = -ComponentState.a;

            int toStop = length2Stop(state.Vel, ComponentState.a);

            if (Math.Abs(state.NeedingHeight - state.height) > toStop)
                state.Vel += a;
            else
                if (Math.Abs(state.Vel) <= ComponentState.a)
                    state.Vel = 0;
                else
                    state.Vel -= a;

            return state.Vel;
        }
        private int length2Stop(int v,int a)
        {
            return (2 * v + a * (v / a - 1)) * (v / a) / 2;
        }
        private int Get2Go(ComponentState s)
        {
            int toGo = 0;
            
            if (!s.state)
            {
                int UsingSpace = 0;
                foreach (ComponentState state in ComponentList)  
                    UsingSpace += state.height;

                toGo = MainControlCol.Height - UsingSpace - 10;
            }
            else            
                toGo = s.height - s.GetMinHeight;            

            return toGo;
        }
        private int NeedingHeiht(ComponentState state)
        {
            if (state.state == false)
            {
                int Height = MainControlCol.Height - GetTotalHeight(true);
                Height /= GetNumberofFixed(false);
                return Height;
            }
            else
                return state.GetMinHeight;
        }
        private int GetTotalHeight(bool minimized)
        {
            int heiht=0;
            foreach (ComponentState state in ComponentList)
                if (state.state == minimized)
                    heiht += state.GetMinHeight;
            return heiht; 
        }
        private int GetNumberofFixed(bool minimized)
        {
            int num = 0;
            foreach (ComponentState state in ComponentList)
                if (state.Fixed && state.state == minimized)
                    num++;
            return num;
        }
    }
}
