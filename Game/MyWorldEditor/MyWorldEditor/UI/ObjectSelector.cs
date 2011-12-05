using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;

namespace MyWorldEditor
{
    class RayCaster : Singleton<RayCaster>
    {
        private RaySceneQuery mRayQ;

        private SceneNode DragingNode;
        public void StopDrag() { DragingNode = null; }
        public SceneNode DragSurface { get; set; }

        private RayCaster()
        {
            mRayQ = OgreRoot.Instance.mSceneMgr.CreateRayQuery(new Ray(), Mogre.SceneManager.ENTITY_TYPE_MASK);
            //DragSurface = OgreRoot.Instance.mSceneMgr.GetSceneNode("ground");
        }

        public uint TypeMask { get { return mRayQ.QueryTypeMask; } set { mRayQ.QueryTypeMask = value; } }

        public Vector3 getContactpoint(Ray ray, AxisAlignedBox obj)
        {
            Pair<bool, float> result = ray.Intersects(obj);
                
            if(result.first) 
                return ray.GetPoint(result.second);          

            return Vector3.ZERO;
        }

        public void SelectObject(Ray ray, object defObj)
        {
            if (CastRay(ray) == null)
                OptionViewer.Instance.Object = defObj;
        }

        public void createRoadPoint(Ray ray)
        {
            if (DragingNode == null)
            {
                MovableObject obj = CastRay(ray);
                if (obj != null)
                {
                    if (!obj.Name.Contains("PointPointer_"))
                    {
                        Road.Instance.AddPoint(getContactpoint(ray, obj.ParentSceneNode._getWorldAABB()));
                    }
                }
            }
        }

        public MovableObject CastRay(Ray ray)
        {
            mRayQ.QueryTypeMask = Mogre.SceneManager.ENTITY_TYPE_MASK;
            mRayQ.Ray = ray;
            RaySceneQueryResult res = mRayQ.Execute();

            if (!res.IsEmpty)
            {
                return res[0].movable;
            }
            return null;
        }

        public void Drag(Ray ray)
        {
            if (DragingNode == null)
            {
                MovableObject obj = CastRay(ray);
                if (obj != null)
                {
                    if (obj.Name.Contains("PointPointer_"))
                        DragingNode = obj.ParentSceneNode;
                }
                else
                    StopDrag();
            }
            else
            {
                DragingNode.Position = getContactpoint(ray, DragSurface._getWorldAABB()) - DragingNode.Parent.Position;
                Road.Instance.DrawRoad();
            }
        }        
    }
}
