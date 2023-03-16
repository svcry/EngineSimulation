using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulation.Model
{
    public abstract class Engine
    {
        // Общие свойства и методы для всех типов двигателей
        public abstract double[] RotationSpeed { get; set; }
        public abstract double[] Torque { get; set; }
        public abstract double EngineTemp { get; set; }
        public abstract double TimeElapsed { get; set; }
        public abstract double AmbientTemp { get; set; }

        protected abstract double GetTorque(double[] RotationSpeed, double[] Torque, double point);

        public abstract double SimulateEngine();
    }
}
