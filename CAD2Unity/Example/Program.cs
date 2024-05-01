using BasicLoader.Interface;
using STPLoader.Implementation.Model;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string stpFilePath = args[0];
            ILoader loader = LoaderFactory.CreateFileLoader(stpFilePath);
            IParser parser = ParserFactory.Create();
            IModel model = parser.Parse(loader);

            if (model is StpFile stpFile)
            {
                Dictionary<long, Entity> data = stpFile.Data.All().ToDictionary(x => x.Key, x => x.Value);
                List<Entity> topLevelEntities = TopLevelEntities(data);

                foreach (Entity entity in topLevelEntities)
                {
                    List<Entity> components = Components(entity, data);
                }
            }
        }

        static List<Entity> TopLevelEntities(Dictionary<long, Entity> data)
        {
            List<long> referencedIds = [];
            foreach (Entity entity in data.Values)
            {
                foreach (string s in entity.Data)
                {
                    if (s[0] == '#')
                    {
                        long parsedId = long.Parse(s[1..]);
                        referencedIds.Add(parsedId);
                    }
                }
            }

            return data.Values.Where(entity => !referencedIds.Contains(entity.Id)).ToList();
        }

        static List<Entity> Components(Entity entity, Dictionary<long, Entity> data)
        {
            List<Entity> components = [];
            switch (entity)
            {
                case AdvancedFace advancedFace:
                    foreach (long boundId in advancedFace.BoundIds)
                    {
                        Bound bound = data[boundId] as Bound;
                        components.Add(bound);
                    }
                    
                    Surface advancedFaceSurface = data[advancedFace.SurfaceId] as Surface;
                    components.Add(advancedFaceSurface);

                    return components;
                case Axis2Placement3D axis2Placement3D:
                    CartesianPoint axisPtX = data[axis2Placement3D.PointIds[0]] as CartesianPoint;
                    CartesianPoint axisPtY = data[axis2Placement3D.PointIds[1]] as CartesianPoint;
                    CartesianPoint axisPtZ = data[axis2Placement3D.PointIds[2]] as CartesianPoint;

                    components.Add(axisPtX);
                    components.Add(axisPtY);
                    components.Add(axisPtZ);

                    return components;
                case FaceBound faceBound:

                    EdgeLoop faceBoundEdgeLoop = data[faceBound.EdgeLoopId] as EdgeLoop;

                    components.Add(faceBoundEdgeLoop);

                    return components;
                case FaceOuterBound faceOuterBound:

                    EdgeLoop faceOuterBoundEdgeLoop = data[faceOuterBound.EdgeLoopId] as EdgeLoop;

                    components.Add(faceOuterBoundEdgeLoop);
                    return components;
                case Bound bound:
                    return components;
                case BSplineCurveWithKnots bSplineCurveWithKnots:
                    return components;
                case DirectionPoint directionPoint:
                    return components;
                case CartesianPoint cartesianPoint:
                    return components;
                case Circle circle:
                    Axis2Placement3D circleAxis2Placement3D = data[circle.PointId] as Axis2Placement3D;
                    CartesianPoint circlePt = data[circleAxis2Placement3D.PointIds[0]] as CartesianPoint;
                    DirectionPoint dir1 = data[circleAxis2Placement3D.PointIds[1]] as DirectionPoint;
                    DirectionPoint dir2 = data[circleAxis2Placement3D.PointIds[2]] as DirectionPoint;

                    components.Add(circleAxis2Placement3D);
                    components.Add(circlePt);
                    components.Add(dir1);
                    components.Add(dir2);

                    return components;
                case ClosedShell closedShell:
                    return components;
                case ConicalSurface conicalSurface:
                    return components;
                case CylindricalSurface cylindricalSurface:
                    Axis2Placement3D cylinderAxis2Placement3D =
                        data[cylindricalSurface.PointId] as Axis2Placement3D;
                    CartesianPoint cylinderPt = data[cylinderAxis2Placement3D.PointIds[0]] as CartesianPoint;
                    DirectionPoint cylinderDir1 = data[cylinderAxis2Placement3D.PointIds[1]] as DirectionPoint;
                    DirectionPoint cylinderDir2 = data[cylinderAxis2Placement3D.PointIds[2]] as DirectionPoint;

                    components.Add(cylindricalSurface);
                    components.Add(cylinderAxis2Placement3D);
                    components.Add(cylinderPt);
                    components.Add(cylinderDir1);
                    components.Add(cylinderDir2);
                    return components;
                case EdgeCurve edgeCurve:
                    return components;
                case EdgeLoop edgeLoop:
                    return components;
                case Line line:

                    CartesianPoint linePt1 = data[line.Point1Id] as CartesianPoint;
                    VectorPoint lineVecPt = data[line.Point2Id] as VectorPoint;
                    DirectionPoint lineDirPt = data[lineVecPt.PointId] as DirectionPoint;


                    //CartesianPoint lineEnd = lineStart + directionVector * (float)lineVecPt.Length;

                    components.Add(linePt1);
                    components.Add(lineVecPt);
                    components.Add(lineDirPt);

                    return components;
                case OrientedEdge orientedEdge:

                    EdgeCurve orientedEdgeEdgeCurve = data[orientedEdge.PointIds[2]] as EdgeCurve;

                    components.Add(orientedEdgeEdgeCurve);

                    return components;
                case Plane plane:
                    return components;
                case ToroidalSurface toroidalSurface:
                    return components;
                case Surface surface:
                    return components;
                case VectorPoint vectorPoint:
                    return components;
                case VertexPoint vertexPoint:
                    return components;
            }

            return components;
        }
    }
}