using EngineSimulation.Model;
using System;


namespace EngineSimulation.Test
{
    public class TestStand
    {
        private Engine engine;

        public TestStand(Engine engine)
        {
            this.engine = engine;
        }

        public float RunTest()
        {
            float timeElapsed = engine.SimulateEngine();

            Console.WriteLine($"Engine overheated after {timeElapsed} seconds");

            return timeElapsed;
        }
    }
}
