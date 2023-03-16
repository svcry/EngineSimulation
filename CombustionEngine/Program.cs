using System;
using EngineSimulation.Test;
using EngineSimulation.Model;

namespace EngineSimulation
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите температуру окружающей среды:");
            float ambitientTemp = float.Parse(Console.ReadLine());
            Engine engine = 
                new InternalCombustionEngine(momentOfInertia: 10, overheatingTemp: 110, heatingSpeedCoeff: 0.01, rotationSpeedCoeff: 0.0001, coolingCoeff: 0.1, ambitientTemp);
            engine.RotationSpeed = new float[] { 0, 75, 150, 200, 250, 300 };
            engine.Torque = new float[] { 20, 75, 100, 105, 75, 0 };

            TestStand testStand = new TestStand(engine);
            float timeElapsed = testStand.RunTest();

            Console.ReadLine();
        }

    }
}

