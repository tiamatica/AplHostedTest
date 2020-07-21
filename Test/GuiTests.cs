using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Test {
    [TestClass]
    [DoNotParallelize]
    public class GuiTests {

        [DllImport("User32")]
        extern public static int GetGuiResources(IntPtr hProcess, int uiFlags);

        const int GDI_OBJECTS = 0;
        const int USER_OBJECTS = 1;

        public static int GetGdiObjectCount(Process process) {
            return GetGuiResources(process.Handle, GDI_OBJECTS);
        }

        public static int GetUserObjectCount(Process process) {
            return GetGuiResources(process.Handle, USER_OBJECTS);
        }

        private static DyalogInterpreter CreateInterpreter() {
            var interpreterSettings = new Dictionary<string, string> {
                ["RUNASSERVICE"] = "2",
                ["MAXWS"] = "1G"
            };
            return new DyalogInterpreter(null, interpreterSettings) {
                SingleThreaded = true,
                DeleteOnUnload = true
            };
        }

        [TestMethod]
        public void EchoNoLeakedGdiHandles() {
            var process = Process.GetCurrentProcess();

            var count = GetGdiObjectCount(process);
            var interpreter = CreateInterpreter();

            try {
                var apl = new AplGroup002.ResourceTests(interpreter);
                var result = apl.Echo(15);
            } finally {
                Assert.IsTrue(interpreter.Unload());
                Assert.AreEqual(count, GetGdiObjectCount(process));
            }
        }

        [TestMethod]
        public void EchoNoLeakedUserHandles() {
            var process = Process.GetCurrentProcess();

            var count = GetUserObjectCount(process);
            var interpreter = CreateInterpreter();

            try {
                var apl = new AplGroup002.ResourceTests(interpreter);
                var result = apl.Echo(15);
            } finally {
                Assert.IsTrue(interpreter.Unload());
                Assert.AreEqual(count, GetUserObjectCount(process));
            }
        }

        private void EchoReload(int count) {

            for (int i = 0; i < count; i++) {
                var interpreter = CreateInterpreter();

                try {
                    var apl = new AplGroup002.ResourceTests(interpreter);
                    var result = apl.Echo(i);
                    Assert.AreEqual(i, result);
                } finally {
                    Assert.IsTrue(interpreter.Unload());
                }
            }
        }

        [TestMethod]
        public void EchoReload10() {
            EchoReload(10);
        }

        [TestMethod]
        public void EchoReload100() {
            EchoReload(100);
        }

        [TestMethod]
        public void EchoReload1000() {
            EchoReload(1000);
        }
    }

}