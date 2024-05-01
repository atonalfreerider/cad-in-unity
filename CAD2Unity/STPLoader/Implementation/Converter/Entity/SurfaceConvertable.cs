using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity
{
    class SurfaceConvertable : IConvertable
    {
        readonly Surface _surface;
        readonly IStpModel _model;

        public SurfaceConvertable(Surface surface, IStpModel model)
        {
            _surface = surface;
            _model = model;
            Init();
        }

        void Init()
        {
            IConvertable child = Convertable(_surface, _model);
            Points = child.Points;
            Indices = child.Indices;
        }

        static IConvertable Convertable(Surface surface, IStpModel model)
        {
            if (surface.GetType() == typeof (CylindricalSurface))
            {
                return new CylindricalSurfaceConvertable(surface, model);
            }

            if (surface.GetType() == typeof(ConicalSurface))
            {
                return new ConicalSurfaceConvertable(surface, model);
            }
            if (surface.GetType() == typeof(Plane))
            {
                return new PlaneConvertable(surface, model);
            }
            if (surface.GetType() == typeof(ToroidalSurface))
            {
                return new ToroidalSurfaceConvertable(surface, model);
            }

            throw new Exception("No convertable found!");
        }

        public IList<Vector3> Points { get; private set; }
        public IList<int> Indices { get; private set; }
    }

    class ToroidalSurfaceConvertable : IConvertable
    {
        readonly Surface _surface;
        readonly IStpModel _model;

        public ToroidalSurfaceConvertable(Surface surface, IStpModel model)
        {
            _surface = surface;
            _model = model;
            Init();
        }

        void Init()
        {
            Points = new List<Vector3>();
            Indices = new List<int>();
            
        }

        public IList<Vector3> Points { get; private set; }
        public IList<int> Indices { get; private set; }
    }

    class ConicalSurfaceConvertable : IConvertable
    {
        readonly Surface _surface;
        readonly IStpModel _model;

        public ConicalSurfaceConvertable(Surface surface, IStpModel model)
        {
            _surface = surface;
            _model = model;
            Init();
        }

        void Init()
        {
            Points = new List<Vector3>();
            Indices = new List<int>();
        }

        public IList<Vector3> Points { get; private set; }
        public IList<int> Indices { get; private set; }
    }
}
