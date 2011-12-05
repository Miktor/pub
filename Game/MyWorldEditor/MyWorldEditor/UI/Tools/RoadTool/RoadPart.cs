using System.Collections.Generic;
using System;
using Mogre;
namespace MyWorldEditor
{
    /// <summary>
    /// type of Bezie Curves
    /// </summary>
    public enum CurveType
    {
        ThreePoint = 1,
        FourPoint = 2,
        AllPoints = 3
    }

    /// <summary>
    /// Interface class fore part of  road i.e. road, crossroad
    /// </summary>
    class RoadPart : IRoadObject
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        public float Length { get; private set; }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets the item ID.
        /// </summary>
        public int itemID { get; private set; }

        /// <summary>
        /// Private list of Curve Points
        /// </summary>
        private List<CurvePoint> points;

        public SceneNode RoadDebugNode { get; private set; }
        public ManualObject RoadGuide { get; private set; }
        public ManualObject CurveGuides { get; private set; }    
        public ManualObject CurveTangents { get; private set; }

        public SceneNode RoadNode { get; private set; }
        public ManualObject RoadMesh { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadPart"/> class.
        /// </summary>
        /// <param name="_id">The _id of road part ! Uniqe.</param>
        public RoadPart(int _id)
        {
            ID = _id;
            points = new List<CurvePoint>();

            RoadNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("RoadNode_" + ID.ToString());          

            RoadDebugNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("RoadDebugNode_" + ID.ToString());

            RoadMesh = new ManualObject("RoadMesh_" + ID.ToString());
            RoadNode.AttachObject(RoadMesh);

            RoadGuide = new ManualObject("RoadMesh_" + ID.ToString());
            RoadDebugNode.AttachObject(RoadGuide);

            CurveGuides = new ManualObject("CubucCurveGuides" + ID.ToString());
            RoadDebugNode.AttachObject(CurveGuides);
                      
            CurveTangents = new ManualObject("CurveTangents" + ID.ToString());
            RoadDebugNode.AttachObject(CurveTangents);
        }

        public RoadPart(int _id, SceneNode roadNode, SceneNode debugNode)
        {
            ID = _id;
            points = new List<CurvePoint>();

            RoadNode = roadNode;
            RoadDebugNode = debugNode;

            
            RoadMesh = new ManualObject("RoadMesh_" + ID.ToString());
            RoadNode.AttachObject(RoadMesh);
            
            CurveGuides = new ManualObject("CubucCurveGuides" + ID.ToString());
            RoadDebugNode.AttachObject(CurveGuides);

            CurveTangents = new ManualObject("CurveTangents" + ID.ToString());
            RoadDebugNode.AttachObject(CurveTangents);

            RoadGuide = new ManualObject("RoadMesh_" + ID.ToString());
            RoadDebugNode.AttachObject(RoadGuide);
        }

        #region Interface implementation

        /// <summary>
        /// Adds the point.
        /// </summary>
        /// <param name="point">The point.</param>
        public void AddPoint(Vector3 point)
        {
            points.Add(new CurvePoint(point, itemID++, this));
        }

        /// <summary>
        /// Removes the point by index
        /// </summary>
        /// <param name="n">The index.</param>
        /// <returns></returns>
        public bool RemovePoint(int n)
        {
            try
            {
                points.RemoveAt(n);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public bool RemovePoint(CurvePoint point)
        {
            return points.Remove(point);
        }

        /// <summary>
        /// Removes the point by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public bool RemovePointById(int id)
        {
            return RemovePoint(FindPointById(id));
        }

        /// <summary>
        /// Finds the point by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public int FindPointById(int id)
        {
            for (int n = 0; n < points.Count; n++)
                if (points[n].ID == id)
                    return n;
            return -1;            
        }

        /// <summary>
        /// Gets the begin point.
        /// </summary>
        /// <returns></returns>
        public CurvePoint getBeginPoint()
        {
            if (points.Count > 0)
                return points[0];
            else
                return null;
        }

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <returns></returns>
        public CurvePoint getEndPoint()
        {
            if (points.Count > 0)
                return points[0];
            else
                return null;
        }

        /// <summary>
        /// Gets the border points.
        /// </summary>
        /// <returns></returns>
        public List<CurvePoint> getBorderPoints()
        {
            return new List<CurvePoint> { getBeginPoint(), getEndPoint() };
        }

       
        public void DrawRoad(int precision = 15)
        {
            precision *= points.Count;

            RoadMesh.Clear();

            RoadMesh.Begin("RoadGray", RenderOperation.OperationTypes.OT_TRIANGLE_STRIP);

            Vector3 pos, backSide;
            for (int i = 0; i < points.Count - 1; i++)
                for (int n = 0; n <= precision; n++)
                {
                    pos = getPoint((float)n / (float)precision, points[i], points[i + 1]);
                    backSide = getTangent((float)n / (float)precision, points[i], points[i + 1]).CrossProduct(Vector3.UNIT_Y).NormalisedCopy * 2;

                    RoadMesh.Position(pos - backSide);
                    RoadMesh.Position(pos + backSide);
                }

            RoadMesh.End();
        }

        public void DrawRoadGuide(int precision = 15)
        {
            precision *= points.Count;

            RoadGuide.Clear();

            RoadGuide.Begin("RoadGuideYellow", RenderOperation.OperationTypes.OT_LINE_STRIP);

            for(int i = 0; i < points.Count - 1; i ++)
                for (int n = 0; n <= precision; n++)
                {
                    RoadGuide.Position(getPoint((float)n / (float)precision, points[i], points[i + 1]));
                }

            RoadGuide.End();
        }
     
        public void ShowDebug(bool show)
        {
            RoadDebugNode.SetVisible(show, true);       
        }
     
        public void DrawDebug()
        {
            DrawRoadGuide();
            DrawTangents();
        }

        #endregion
                
        private void ChangePointVisiblity(bool visible, int n = 0)
        {
            if (n == 0)            
                foreach (CurvePoint point in points)
                {
                    point.Visible = visible;
                }
            else
                points[n - 1].Visible = visible;
        }
       
        private void DrawTangents(int precision = 15)
        {
            Vector3 pos;
            precision *= points.Count;

            CurveTangents.Clear();

            CurveTangents.Begin("RoadGuideGreen", RenderOperation.OperationTypes.OT_LINE_LIST);

            for(int i = 0; i < points.Count - 1; i ++)
                for (int n = 0; n <= precision; n++)
                {                    
                    pos = getPoint((float)n / (float)precision, points[i], points[i+1]);
                    CurveTangents.Position(pos - getTangent((float)n / (float)precision, points[i], points[i + 1]).CrossProduct(Vector3.UNIT_Y).NormalisedCopy);
                    CurveTangents.Position(pos + getTangent((float)n / (float)precision, points[i], points[i+1]).CrossProduct(Vector3.UNIT_Y).NormalisedCopy);
                }

            CurveTangents.End();
        }

        /// <summary>
        /// Gets the point.
        /// </summary>
        /// <param name="a">A.</param>
        /// <returns></returns>
        private Vector3 getPoint(float a, CurvePoint firstpoint,CurvePoint secondPoint)
        {
            Vector3 vec = Vector3.ZERO;
            Vector3[] _points = new Vector3[4] { firstpoint.MainPosition, firstpoint.MainPosition + firstpoint.SecondArmPosition, secondPoint.MainPosition + secondPoint.FirstArmPosition, secondPoint.MainPosition };
            int p = 3;
            for (int i = 0; i <= p; i++)
                vec += ((float)(Binomial.BinomialCoefficient(p, i) * System.Math.Pow(1 - a, p - i) * System.Math.Pow(a, i))) * _points[i];

            return vec;
        }

        /// <summary>
        /// Gets the tangent.
        /// </summary>
        /// <param name="a">A.</param>
        /// <returns></returns>
        private Vector3 getTangent(float a, CurvePoint firstpoint, CurvePoint secondPoint)
        {
            Vector3 vec = Vector3.ZERO;

            Vector3[] _points = new Vector3[4] { firstpoint.MainPosition, firstpoint.MainPosition + firstpoint.SecondArmPosition, secondPoint.MainPosition + secondPoint.FirstArmPosition, secondPoint.MainPosition };
            int p = 3;
            for (int i = 0; i < p; i++)
                vec += ((float)(Binomial.BinomialCoefficient(p - 1, i) * System.Math.Pow(1 - a, p - i - 1) * System.Math.Pow(a, i))) * (_points[i + 1] - _points[i]);

            return vec;
        }    
    }
}