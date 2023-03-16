﻿namespace EngineSimulation.Model {
    public abstract class Engine {
        public abstract float[] RotationSpeed { get; set; }
        public abstract float[] Torque { get; set; }
        public abstract float EngineTemp { get; set; }
        public abstract float TimeElapsed { get; set; }
        public abstract float AmbientTemp { get; set; }

        protected abstract float GetTorque(float[] rotationSpeed, float[] torque, float point);

        public abstract float SimulateEngine();
    }
}
