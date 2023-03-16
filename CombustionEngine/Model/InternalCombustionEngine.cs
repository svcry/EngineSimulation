using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulation.Model
{
    public class InternalCombustionEngine : Engine
    {
        public double MomentOfInertia { get; set; } // Момент инерции двигателя
        public double OverheatingTemp { get; set; } // Температура перегрева
        public double HeatingSpeedCoeff { get; set; } // Коэффициент зависимости скорости нагрева от крутящего момента
        public double RotationSpeedCoeff { get; set; } // Коэффициент зависимости скорости нагрева от скорости вращения коленвала
        public double CoolingCoeff { get; set; } // Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды

        public override double[] RotationSpeed { get; set; } // Скорость вращения коленвала

        public override double[] Torque { get; set; } // Крутящий момент
        public override double EngineTemp { get; set; } // Температура двигателя
        public override double TimeElapsed { get; set; } = 0.0; // Время в секундах

        public override double AmbientTemp { get; set; } // Температура окружающей среды

        public InternalCombustionEngine(double momentOfInertia, double overheatingTemp, double heatingSpeedCoeff,
            double rotationSpeedCoeff, double coolingCoeff, double initialEngineTemp)
        {
            MomentOfInertia = momentOfInertia;
            OverheatingTemp = overheatingTemp;
            HeatingSpeedCoeff = heatingSpeedCoeff;
            RotationSpeedCoeff = rotationSpeedCoeff;
            CoolingCoeff = coolingCoeff;
            AmbientTemp = initialEngineTemp;
        }

        protected override double GetTorque(double[] RotationSpeed, double[] Torque, double point)
        {
            // Значение кусочно-линейной функции в точке point (используется интерполяционный многочлен Лагранжа)
            int n = RotationSpeed.Length;
            double result = 0;

            for (int i = 0; i < n; i++)
            {
                double term = Torque[i];

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

        public override double SimulateEngine()
        {
            double rotationSpeed = RotationSpeed[0];
            double torque = Torque[0];
            EngineTemp = AmbientTemp;
            while (EngineTemp < OverheatingTemp)
            {

                // Рассчитываем ускорение двигателя
                double acceleration = torque / MomentOfInertia;
                rotationSpeed += acceleration;
                torque = GetTorque(RotationSpeed, Torque, rotationSpeed);
                // Рассчитываем скорость нагрева и охлаждения двигателя
                double heatingSpeed = torque * HeatingSpeedCoeff + rotationSpeed * rotationSpeed * RotationSpeedCoeff;
                double coolingSpeed = CoolingCoeff * (AmbientTemp - EngineTemp);

                // Рассчитываем новые значения скорости и температуры
                EngineTemp += heatingSpeed - coolingSpeed;
                TimeElapsed += 1.0; // Увеличиваем время на 1 секунду
            }
            return TimeElapsed;
        }
    }
}
