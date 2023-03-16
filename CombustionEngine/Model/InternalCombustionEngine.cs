namespace EngineSimulation.Model {
    public class InternalCombustionEngine : Engine {
        private float _momentOfInertia { get; }
        private float _overheatingTemp { get; }
        private float _heatingSpeedCoeff { get; }
        private float _rotationSpeedCoeff { get; }
        private float _coolingCoeff { get; }

        public override float[] RotationSpeed { get; set; }
        public override float[] Torque { get; set; }
        public override float EngineTemp { get; set; }
        public override float TimeElapsed { get; set; }
        public override float AmbientTemp { get; set; }

        public InternalCombustionEngine(float momentOfInertia, float overheatingTemp, float heatingSpeedCoeff,
            float rotationSpeedCoeff, float coolingCoeff, float initialEngineTemp) {
            _momentOfInertia = momentOfInertia;
            _overheatingTemp = overheatingTemp;
            _heatingSpeedCoeff = heatingSpeedCoeff;
            _rotationSpeedCoeff = rotationSpeedCoeff;
            _coolingCoeff = coolingCoeff;
            AmbientTemp = initialEngineTemp;
        }

        protected override float GetTorque(float[] rotationSpeed, float[] torque, float point) {
            // Значение кусочно-линейной функции в точке point (используется интерполяционный многочлен Лагранжа)
            int n = rotationSpeed.Length;
            float result = 0;

            for (int i = 0; i < n; i++) {
                float term = torque[i];

                for (int j = 0; j < n; j++) {
                    if (i != j) {
                        term *= (point - rotationSpeed[j]) / (rotationSpeed[i] - rotationSpeed[j]);
                    }
                }

                result += term;
            }

            return result;
        }

        public override float SimulateEngine() {
            float rotationSpeed = RotationSpeed[0];
            float torque = Torque[0];
            EngineTemp = AmbientTemp;

            while (EngineTemp < _overheatingTemp) {
                float acceleration = torque / _momentOfInertia;
                rotationSpeed += acceleration;
                torque = GetTorque(RotationSpeed, Torque, rotationSpeed);

                float heatingSpeed = torque * _heatingSpeedCoeff + rotationSpeed * rotationSpeed * _rotationSpeedCoeff;
                float coolingSpeed = _coolingCoeff * (AmbientTemp - EngineTemp);

                EngineTemp += heatingSpeed - coolingSpeed;
                TimeElapsed += 1.0F;
            }

            return TimeElapsed;
        }
    }
}