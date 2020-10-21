using System;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class DebugTests {
        private DyalogInterpreter GetInterpreter() {
            return new DyalogInterpreter() {
                SingleThreaded = true,
                DeleteOnUnload = true
            };
        }

        [TestMethod]
        [Description("Check if you can set a breakpoint in class and trace when triggered.")]
        public void SetBreakpointDepth1() {
            var int1 = GetInterpreter();
            var cls = new AplGroup001.ClassA(int1);
            cls.Stop("HelloWorld");
            cls.HelloWorld();
            cls.Dispose();
        }
        [TestMethod]
        [Description("Check if you can set a breakpoint in derived class and trace when triggered.")]
        public void SetBreakpointDepth2() {
            var int1 = GetInterpreter();
            var cls = new AplGroup002.ClassA(int1);
            cls.Stop("World");
            cls.Stop("HelloWorld");
            cls.World();
            cls.Dispose();
        }
        [TestMethod]
        [Description("Check if you can set a breakpoint in three levels derived class and trace when triggered.")]
        public void SetBreakpointDepth3() {
            var int1 = GetInterpreter();
            var cls = new AplGroup002.AClass(int1);
            cls.Stop("Hello");
            cls.Stop("World");
            cls.Stop("Stop");
            cls.Stop("HelloWorld");
            cls.Hello();
            cls.Dispose();
        }
    }
}
