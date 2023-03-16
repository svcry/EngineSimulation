

namespace EngineSimulation.Model
{
    public class InternalCombustionEngine : Engine
    {
        public float MomentOfInertia { get; set; } // Момент инерции двигателя
        public float OverheatingTemp { get; set; } // Температура перегрева
        public float HeatingSpeedCoeff { get; set; } // Коэффициент зависимости скорости нагрева от крутящего момента
        public float RotationSpeedCoeff { get; set; } // Коэффициент зависимости скорости нагрева от скорости вращения коленвала
        public float CoolingCoeff { get; set; } // Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды

        public override float[] RotationSpeed { get; set; } // Скорость вращения коленвала

        public override float[] Torque { get; set; } // Крутящий момент
        public override float EngineTemp { get; set; } // Температура двигателя
        public override float TimeElapsed { get; set; } = 0.0F; // Время в секундах

        public override float AmbientTemp { get; set; } // Температура окружающей среды

        public InternalCombustionEngine(float momentOfInertia, float overheatingTemp, float heatingSpeedCoeff,
            float rotationSpeedCoeff, float coolingCoeff, float initialEngineTemp)
        {
            MomentOfInertia = momentOfInertia;
            OverheatingTemp = overheatingTemp;
            HeatingSpeedCoeff = heatingSpeedCoeff;
            RotationSpeedCoeff = rotationSpeedCoeff;
            CoolingCoeff = coolingCoeff;
            AmbientTemp = initialEngineTemp;
        }

        protected override float GetTorque(float[] RotationSpeed, float[] Torque, float point)
        {
            // Значение кусочно-линейной функции в точке point (используется интерполяционный многочлен Лагранжа)
            int n = RotationSpeed.Length;
            float result = 0;

            for (int i = 0; i < n; i++)
            {
                float term = Torque[i];

                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        term *= (point - RotationSpeed[j]) / (RotationSpeed[i] - RotationSpeed[j]);
                    }
                }

                result += term;
            }

            return result;
        }

        public override float SimulateEngine()
        {
            float rotationSpeed = RotationSpeed[0];
            float torque = Torque[0];
            EngineTemp = AmbientTemp;
            while (EngineTemp < OverheatingTemp)
            {

                // Рассчитываем ускорение двигателя
                float acceleration = torque / MomentOfInertia;
                rotationSpeed += acceleration;
                torque = GetTorque(RotationSpeed, Torque, rotationSpeed);
                // Рассчитываем скорость нагрева и охлаждения двигателя
                float heatingSpeed = torque * HeatingSpeedCoeff + rotationSpeed * rotationSpeed * RotationSpeedCoeff;
                float coolingSpeed = CoolingCoeff * (AmbientTemp - EngineTemp);

                // Рассчитываем новые значения скорости и температуры
                EngineTemp += heatingSpeed - coolingSpeed;
                TimeElapsed += 1.0F;
            }
            return TimeElapsed;
        }
    }
}
