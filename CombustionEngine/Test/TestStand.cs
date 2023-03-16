using EngineSimulation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulation.Test
{
    public class TestStand
    {
        private Engine engine;

        public TestStand(Engine engine)
        {
            this.engine = engine;
        }

        public double RunTest()
        {
            double timeElapsed = engine.SimulateEngine();

            Console.WriteLine($"Двигатель перегрелся через {timeElapsed} секунд");

            return timeElapsed;
        }
    }
}
