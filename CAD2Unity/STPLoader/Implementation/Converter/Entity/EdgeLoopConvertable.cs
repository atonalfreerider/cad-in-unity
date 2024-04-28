using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity;

public class EdgeLoopConvertable : IConvertable
{
    public IList<Vector3> Points { get; }
    public IList<int> Indices { get; }
    
    public EdgeLoopConvertable(EdgeLoop edgeLoop, IStpModel model)
    {
        Points = new List<Vector3>();
        Indices = new List<int>();
        
        foreach (long edgeId in edgeLoop.PointIds)
        {
            OrientedEdge edge = model.Get<OrientedEdge>(edgeId);
            foreach (long edgePointId in edge.PointIds)
            {
                if(edgePointId == 0) continue;
                EdgeCurve edgeCurve = model.Get<EdgeCurve>(edgePointId);
                foreach (long edgeCurvePointId in edgeCurve.PointIds)
                {
                    VertexPoint? vert = model.Get<VertexPoint>(edgeCurvePointId);
                    Circle? circle = model.Get<Circle>(edgeCurvePointId);
                    Line? line = model.Get<Line>(edgeCurvePointId);
                    if (vert != null)
                    {
                        CartesianPoint point = model.Get<CartesianPoint>(vert.PointId);
                    }
                    else if (circle != null)
                    {
                        CircleConvertable circleConvertable = new CircleConvertable(circle, model);
                        foreach (Vector3 circleConvertablePoint in circleConvertable.Points)
                        {
                            Points.Add(circleConvertablePoint);
                        }
                    }
                    else if (line != null)
                    {
                        LineConvertable lineConvertable = new LineConvertable(line, model);
                        foreach (Vector3 lineConvertablePoint in lineConvertable.Points)
                        {
                            Points.Add(lineConvertablePoint);
                        }
                    }
                    else
                    {
                        var z = 1;
                    }
                    
                }
            }
        }
    }
}