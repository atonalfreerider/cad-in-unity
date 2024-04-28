using BasicLoader.Implementation.Model;
using BasicLoader.Interface;
using STPLoader.Implementation.Converter.Entity;
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
                    IConvertable? convertedEntity = CreateConvertable(entity, stpFile);
                    
                    switch (convertedEntity)
                    {
                        case AdvancedFaceConvertable advancedFaceConvertable:
                            break;
                        case Axis2Placement3DConvertable axis2Placement3DConvertable:
                            break;
                        case BoundConvertable boundConvertable:
                            break;
                        case CircleConvertable circleConvertable:
                            break;
                        case ClosedShellConveratable closedShellConveratable:
                            break;
                        case CylindricalSurfaceConvertable cylindricalSurfaceConvertable:
                            break;
                        case EdgeCurveConvertable edgeCurveConvertable:
                            break;
                        case EdgeLoopConvertable edgeLoopConvertable:
                            break;
                        case FaceBoundConvertable faceBoundConvertable:
                            break;
                        case FaceOuterBoundConvertable faceOuterBoundConvertable:
                            break;
                        case LineConvertable lineConvertable:
                            break;
                        case OrientedEdgeConvertable orientedEdgeConvertable:
                            break;
                        case PlaneConvertable planeConvertable:
                            break;
                        case SurfaceConvertable surfaceConvertable:
                            break;
                        case null:
                            break;
                    }
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
      
        static IConvertable? CreateConvertable(Entity entity, IStpModel model)
        {
            switch (entity)
            {
                case AdvancedFace advancedFace:
                    return new AdvancedFaceConvertable(advancedFace, model);
                case Axis2Placement3D axis2Placement3D:
                    return new Axis2Placement3DConvertable(axis2Placement3D, model);
                case FaceBound faceBound:
                    return new FaceBoundConvertable(faceBound, model);
                case FaceOuterBound faceOuterBound:
                    return new FaceOuterBoundConvertable(faceOuterBound, model);
                case Bound bound:
                    return new BoundConvertable(bound, model);
                case BSplineCurveWithKnots bSplineCurveWithKnots:
                    break;
                case DirectionPoint directionPoint:
                    break;
                case CartesianPoint cartesianPoint:
                    break;
                case Circle circle:
                    return new CircleConvertable(circle, model);
                case ClosedShell closedShell:
                    return new ClosedShellConveratable(closedShell, model);
                case ConicalSurface conicalSurface:
                    break;
                case CylindricalSurface cylindricalSurface:
                    return new CylindricalSurfaceConvertable(cylindricalSurface, model);
                case EdgeCurve edgeCurve:
                    return new EdgeCurveConvertable(edgeCurve, model);
                case EdgeLoop edgeLoop:
                    return new EdgeLoopConvertable(edgeLoop, model);
                case Line line:
                    return new LineConvertable(line, model);
                case OrientedEdge orientedEdge:
                    return new OrientedEdgeConvertable(orientedEdge, model);
                case Plane plane:
                    return new PlaneConvertable(plane, model);
                case ToroidalSurface toroidalSurface:
                    break;
                case Surface surface:
                    return new SurfaceConvertable(surface, model);
                case VectorPoint vectorPoint:
                    break;
                case VertexPoint vertexPoint:
                    break;
            }

            return null;

        }
    }
}