using System.Collections.Generic;
using Mogre;

namespace MyWorldEditor
{
    interface IRoadObject
    {
        int ID { get; }
        /// <summary>
        /// Adds the point.
        /// </summary>
        /// <param name="point">The point.</param>
        void AddPoint(Vector3 point);
        /// <summary>
        /// Removes the point by index
        /// </summary>
        /// <param name="n">The index.</param>
        /// <returns></returns>
        bool RemovePoint(int n);
        /// <summary>
        /// Removes the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        bool RemovePoint(CurvePoint point);
        /// <summary>
        /// Removes the point by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        bool RemovePointById(int id);
        /// <summary>
        /// Finds the point by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        int FindPointById(int id);
        /// <summary>
        /// Gets the begin point.
        /// </summary>
        /// <returns></returns>
        CurvePoint getBeginPoint();
        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <returns></returns>
        CurvePoint getEndPoint();
        /// <summary>
        /// Gets the border points.
        /// </summary>
        /// <returns></returns>
        List<CurvePoint> getBorderPoints();
        /// <summary>
        /// Draws the road part
        /// </summary>
        /// <param name="precision">The precision.</param>
        void DrawRoad(int precision = 15);
        /// <summary>
        /// Draws the Debug.
        /// </summary>
        void DrawDebug();
        /// <summary>
        /// Shows the debug lines.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        void ShowDebug(bool show);
    }
}
