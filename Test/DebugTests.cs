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
            var cls = new AplGroup001.ClassA();
            cls.Stop();
            cls.HelloWorld();
        }
        [TestMethod]
        [Description("Check if you can set a breakpoint in derived class and trace when triggered.")]
        public void SetBreakpointDepth2() {
            var int1 = GetInterpreter();
            var cls = new AplGroup002.ClassA();
            cls.Stop();
            cls.HelloWorld();
        }
        [TestMethod]
        [Description("Check if you can set a breakpoint in three levels derived class and trace when triggered.")]
        public void SetBreakpointDepth3() {
            var int1 = GetInterpreter();
            var cls = new AplGroup002.AClass();
            cls.Stop();
            cls.HelloWorld();
        }
    }
}
