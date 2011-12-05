using System.Collections.Generic;
using Mogre;

namespace WorldEditor.Curves
{
    struct CurveOptions
    {
        public float DistanceTolerance { get; set; }
        public float DistanceToleranceSquare { get; set; }
        public float ApproximationScale { get; set; }
        public int RecursionLimit { get; set; }
        public float AngleTolerance { get; set; }
        public int CuspLimit { get; set; }

        public const float DistanceEpsilon = 1e-30f;
        public const float CollinearityEpsilon = 1e-30f;
        public const float AngleToleranceEpsilon = 0.01f;

        public void ReloadOptions()
        {
            DistanceTolerance = Properties.Settings.Default.CurveDistanceTolerance;
            ApproximationScale = Properties.Settings.Default.CurveApproximationScale;
            RecursionLimit = Properties.Settings.Default.CurveRecursionLimit;
            //CollinearityEpsilon = Properties.Settings.Default.CurveCollinearityEpsilon;
            AngleTolerance = Properties.Settings.Default.CurveAngleTolerance;
            //AngleToleranceEpsilon = Properties.Settings.Default.CurveAngleToleranceEpsilon;
            CuspLimit = Properties.Settings.Default.CurveCuspLimit;
        }
    }
    class Curve
    {
        public Vector3 Position { get; set; }
        public bool ShowGuides { get; set; }
     
        public List<Point> Points = new List<Point>();

        public UI.Tools.RoadTool.ControllPoint BeginCP { get; private set; }
        public UI.Tools.RoadTool.ControllPoint EndCP { get; private set; }

        public CurveOptions Options;

        public Curve(UI.Tools.RoadTool.ControllPoint begin, Vector3 end, Scene.BaseObjects.SimpleObj _owner, string name)
        {
            Options = new CurveOptions();
            Options.ReloadOptions();

            BeginCP = begin;
            EndCP = new UI.Tools.RoadTool.ControllPoint(end, _owner, name);
        }

        public void AddPoint(Vector3 pos, Vector3 tan)
        {
            Points.Add(new Point(pos, tan));
        }
        public Vector3 GetPoint(int n)
        {
            return Points[n].Position;
        }

        public Vector3 GetTangent(int n)
        {
            return Points[n].Position;
        }

        public void update()
        {
            Points.Clear();
            Options.DistanceToleranceSquare = 0.5f / Options.ApproximationScale;
            Options.DistanceToleranceSquare *= Options.DistanceToleranceSquare;
            Options.DistanceTolerance = 6.0f / Options.ApproximationScale;

            AddPoint(BeginCP.Position, BeginCP.HArm.SecondArm.Position - BeginCP.Position);
            recursive_bezier(BeginCP.Position, BeginCP.HArm.SecondArm.Position + BeginCP.Position, EndCP.HArm.FirstArm.Position + EndCP.Position, EndCP.Position, 0);
            AddPoint(EndCP.Position, EndCP.Position - EndCP.HArm.FirstArm.Position);
        }

        private float GetASin(Vector3 v1, Vector3 v2)
        {
            float angle;
            angle = v1.DotProduct(v2) / (v1.Length * v2.Length);
            return Mogre.Math.Abs(Mogre.Math.ACos(angle).ValueRadians);           
        }

        //------------------------------------------------------------------------
        void recursive_bezier(Vector3 c1, Vector3 c2, Vector3 c3, Vector3 c4,
                              uint level)
        {
            if (level > Options.RecursionLimit)
            {
                return;
            }

            // Вычислить все средние точки отрезков
            //----------------------
            Vector3 c12 = (c1 + c2) / 2;
            Vector3 c23 = (c2 + c3) / 2;
            Vector3 c34 = (c3 + c4) / 2;
            Vector3 c123 = (c12 + c23) / 2;
            Vector3 c234 = (c23 + c34) / 2;
            Vector3 c1234 = (c123 + c234) / 2;


            // Try to approximate the full cubic curve by a single straight line
            //------------------
            Vector3 delta = c4 - c1;

            float d2 = Mogre.Math.Abs((c2 - c4).Length * Mogre.Math.Sin(GetASin((c2 - c4), c4 - c1)));
            float d3 = Mogre.Math.Abs((c3 - c4).Length * Mogre.Math.Sin(GetASin((c3 - c4), c4 - c1)));

            float da1, da2;

            switch (((d2 > CurveOptions.CollinearityEpsilon) ? 1 : 0 << 1) + ((d3 > CurveOptions.CollinearityEpsilon) ? 1 : 0))
            {
                case 0:
                    // All collinear OR p1==p4
                    //----------------------
                    if ((c1 + c3 - c2 - c2).SquaredLength +
                       (c2 + c4 - c3 - c3).SquaredLength <= Options.DistanceTolerance)
                    {
                        AddPoint(c1234, c234 - c1234);
                        return;
                    }
                    break;
                case 1:
                    // p1,p2,p4 are collinear, p3 is considerable
                    //----------------------
                    if (d3 * d3 <= Options.DistanceToleranceSquare * delta.SquaredLength)
                    {
                        if (Options.AngleTolerance < CurveOptions.AngleToleranceEpsilon)
                        {
                            AddPoint(c23, c3 - c23);
                            return;
                        }

                        // Angle Condition
                        //----------------------
                        da1 = (c4 - c3).Length;
                        da1 = GetASin(c4 - c3,c3 - c2);

                        if (da1 >= Mogre.Math.PI) da1 = Mogre.Math.TWO_PI - da1;

                        if (da1 < Options.AngleTolerance)
                        {
                            AddPoint(c2, c2 - c1);
                            AddPoint(c3, c3 - c2);
                            return;
                        }

                        if (Options.CuspLimit != 0.0)
                        {
                            if (da1 > Options.CuspLimit)
                            {
                                AddPoint(c3, c3 - c2);
                                return;
                            }
                        }
                    }
                    break;

                case 2:
                    // p1,p3,p4 are collinear, p2 is considerable
                    //----------------------
                    if (d2 * d2 <= Options.DistanceToleranceSquare * delta.SquaredLength)
                    {
                        if (Options.AngleTolerance < CurveOptions.AngleToleranceEpsilon)
                        {
                            AddPoint(c23, c3 - c23);
                            return;
                        }

                        // Angle Condition
                        //----------------------
                        da1 = GetASin((c3 - c2), (c2 - c1));

                        if (da1 >= Mogre.Math.PI) da1 = Mogre.Math.TWO_PI - da1;

                        if (da1 < Options.AngleTolerance)
                        {
                            AddPoint(c2, c2 - c1);
                            AddPoint(c3, c3 - c2);
                            return;
                        }

                        if (Options.CuspLimit != 0.0)
                        {
                            if (da1 > Options.CuspLimit)
                            {
                                AddPoint(c2, c2 - c1);
                                return;
                            }
                        }
                    }
                    break;

                case 3:
                    // Regular care
                    //-----------------
                    if ((d2 + d3) * (d2 + d3) <= Options.DistanceToleranceSquare * delta.SquaredLength)
                    {
                        // If the curvature doesn't exceed the distance_tolerance value
                        // we tend to finish subdivisions.
                        //----------------------
                        if (Options.AngleTolerance < CurveOptions.AngleToleranceEpsilon)
                        {
                            AddPoint(c23, c3 - c23);
                            return;
                        }

                        // Angle & Cusp Condition
                        //----------------------
                        da1 = GetASin((c3 - c2), (c2 - c1));
                        da2 = GetASin((c3 - c2), (c4 - c3));     

                        if (da1 >= Mogre.Math.PI) da1 = Mogre.Math.TWO_PI - da1;
                        if (da2 >= Mogre.Math.PI) da2 = Mogre.Math.TWO_PI - da2;

                        if (da1 + da2 < Options.AngleTolerance)
                        {
                            // Finally we can stop the recursion
                            //----------------------
                            AddPoint(c23, c3 - c23);
                            return;
                        }

                        if (Options.CuspLimit != 0.0)
                        {
                            if (da1 > Options.CuspLimit)
                            {
                                AddPoint(c2, c2 - c1);
                                return;
                            }

                            if (da2 > Options.CuspLimit)
                            {
                                AddPoint(c3, c3 - c2);
                                return;
                            }
                        }
                    }
                    break;
            }

            // Продолжить деление
            //----------------------
            recursive_bezier(c1, c12, c123, c1234, level + 1);
            recursive_bezier(c1234, c234, c34, c4, level + 1);
        }
    }
}
