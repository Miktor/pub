using System;
using System.Collections.Generic;
using Mogre;
using PluginBase;

namespace WorldEditor.Scene.BaseObjects
{
    delegate void UpdateEwentHandler(ISimple sender);   

    class SimpleObj : SceneNode.Listener, ISimple
    {
        virtual public string Name { get; protected set; }
        virtual public ISimple Parent { get; protected set; }
   
        public SceneNode MainNode { get; protected set; }

        virtual public Vector3 Position
        {
            get
            {
                if (MainNode != null)
                    return MainNode.Position;
                else
                    throw new ArgumentNullException("Node", "Scene Node have not assigned");
            }
            set
            {
                if (MainNode != null)
                {
                    if (MainNode.Position != value)
                    {
                        MainNode.Position = value;                       
                    }                   
                }
                else
                    throw new ArgumentNullException("Node", "Scene Node have not assigned");
            }
        }
        virtual public Vector3 GlobalPosition
        {
            get
            {
                if (MainNode != null)
                    return MainNode.ConvertLocalToWorldPosition(MainNode.Position);
                else
                    throw new ArgumentNullException("Node", "Scene Node have not assigned");
            }
            set
            {
                if (MainNode != null)
                {
                    if (MainNode.Position != MainNode.ConvertWorldToLocalPosition(value))
                    {
                        MainNode.Position = MainNode.ConvertWorldToLocalPosition(value);                       
                    }
                }
                else
                    throw new ArgumentNullException("Node", "Scene Node have not assigned");
            }
        }

        public SimpleObj(string name)
        {
            Name = name;
            MainNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode(name);            
        }

        public SimpleObj(string name, ISimple _parent)
        {
            Parent = _parent;
            MainNode = Parent.MainNode.CreateChildSceneNode(name);
            Name = name;            
        }

        public SimpleObj(string name, ISimple _parent, Vector3 pos)
        {
            Parent = _parent;
            MainNode = Parent.MainNode.CreateChildSceneNode(name, pos);
            Name = name;       
        }

        public Entity AttachEntity(string meshName, string material = "")
        {            
            Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity(Name + "_Entity", meshName);
            if(material.Length != 0)
                ent.SetMaterial(MaterialManager.Singleton.GetByName(material, ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME));
            ent.UserObject = this;
            MainNode.AttachObject(ent);
            return ent;
        }

        public Entity AttachEntity(string meshName, object uObj = null, string material = "")
        {
            Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity(Name + "_Entity", meshName);
            if (material.Length != 0)
                ent.SetMaterial(MaterialManager.Singleton.GetByName(material, ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME));
            ent.UserObject = uObj;
            MainNode.AttachObject(ent);
            return ent;
        }

        public Entity AttachEntity(string meshName, string name, object uObj = null, string material = "")
        {
            Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity(name, meshName);
            if (material.Length != 0)
                ent.SetMaterial(MaterialManager.Singleton.GetByName(material, ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME));
            ent.UserObject = uObj;
            MainNode.AttachObject(ent);
            return ent;
        }

        public List<ISimple> Childrens = new List<ISimple>();
        virtual public void AttachChild(ISimple child)
        {
            Childrens.Add(child);
        }

        virtual public void Select()
        {
            MainNode.SetVisible(true);
            foreach (ISimple child in Childrens)
            {
                child.Select();
            }
        }
        virtual public void UnSelect()
        {
            MainNode.SetVisible(false);
            foreach (ISimple child in Childrens)
            {
                child.UnSelect();
            }
        }

        virtual public void Update()
        {
            ChildUpdated(this);
        }

        virtual public void ChildUpdated(ISimple sender)
        {
            if (Parent != null)
                Parent.ChildUpdated(sender);
        }
    }

    //class SimpleObj :  SimpleObj
    //{  
    //    public SimpleObj()
    //    {
    //    }

    //    public SimpleObj(SceneNode mainNode, string name)
    //    {
    //        MainNode = mainNode;
    //        Name = name;

    //        Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity(Name + "_Entity", "miniCube");
    //        ent.SetMaterial(MaterialManager.Singleton.GetByName("RoadGuideRed", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME));
    //        ent.UserObject = this;
    //        MainNode.AttachObject(ent);
    //    }

    //    public SimpleObj(SceneNode mainNode, string name, object userData)
    //    {
    //        MainNode = mainNode;
    //        Name = name;

    //        Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity(Name + "_Entity", "miniCube");
    //        ent.SetMaterial(MaterialManager.Singleton.GetByName("RoadGuideRed", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME));
    //        ent.UserObject = userData;            
    //        MainNode.AttachObject(ent);
    //    }

    //    public SimpleObj(SceneManager mngr, string name, Vector3 pos)
    //    {
    //        MainNode = mngr.RootSceneNode.CreateChildSceneNode(name);
    //        Name = name;

    //        Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity(Name + "_Entity", "miniCube");
    //        ent.SetMaterial(MaterialManager.Singleton.GetByName("RoadGuideRed", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME));
    //        ent.UserObject = this;
    //        MainNode.AttachObject(ent);

    //        Position = pos;
    //    }

    //    public SimpleObj(SceneManager mngr, string name, Vector3 pos, object userData)
    //    {
    //        MainNode = mngr.RootSceneNode.CreateChildSceneNode(name);
    //        Name = name;

    //        OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode(Name);

    //        Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity(Name + "_Entity", "miniCube");
    //        ent.SetMaterial(MaterialManager.Singleton.GetByName("RoadGuideRed", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME));
    //        ent.UserObject = userData;
    //        MainNode.AttachObject(ent);

    //        Position = pos;
    //    }               
    //}
}
