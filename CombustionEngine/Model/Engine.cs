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
        public abstract float[] RotationSpeed { get; set; }
        public abstract float[] Torque { get; set; }
        public abstract float EngineTemp { get; set; }
        public abstract float TimeElapsed { get; set; }
        public abstract float AmbientTemp { get; set; }

        protected abstract float GetTorque(float[] RotationSpeed, float[] Torque, float point);

        public abstract float SimulateEngine();
    }
}
