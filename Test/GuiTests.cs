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

        private static DyalogInterpreter CreateInterpreter(bool runAsService = true) {
            var interpreterSettings = new Dictionary<string, string> {
                ["RUNASSERVICE"] = runAsService ? "2" : "0",
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

        enum TestType {
            Echo,
            Gui
        }

        private void TestReload(TestType testType, int count) {
            var process = Process.GetCurrentProcess();
            var initialGdiHandleCount = GetGdiObjectCount(process);
            var initialUserHandleCount = GetUserObjectCount(process);
            int firstGdiHandleCount = 0;
            int firstUserHandleCount = 0;
            
            for (int i = 0; i < count; i++) {
                var interpreter = CreateInterpreter(testType != TestType.Gui);

                try {
                    var apl = new AplGroup002.ResourceTests(interpreter);
                    if (testType == TestType.Echo) {
                        var result = apl.Echo(i);
                        Assert.AreEqual(i, result);
                    } else if (testType == TestType.Gui) {
                        var name = $"form{i}";
                        var result = apl.CreateForm(name);
                        Assert.IsTrue(result);
                        //result = apl.Expunge(name);
                        //Assert.IsTrue(result);
                    }
                } finally {
                    Assert.IsTrue(interpreter.Unload());
                }
                if (i == 0) {
                    firstGdiHandleCount = GetGdiObjectCount(process);
                    firstUserHandleCount = GetUserObjectCount(process);
                }
            }
            var finalGdiHandleCount = GetGdiObjectCount(process);
            var finalUserHandleCount = GetUserObjectCount(process);

            Assert.AreEqual(finalGdiHandleCount, firstGdiHandleCount);
            Assert.AreEqual(finalUserHandleCount, firstUserHandleCount);
        }

        private void TestSingleSession(TestType testType, int count) {
            var process = Process.GetCurrentProcess();
            TestReload(testType, 1);
            var initialGdiHandleCount = GetGdiObjectCount(process);
            var initialUserHandleCount = GetUserObjectCount(process);
            
            var interpreter = CreateInterpreter(testType != TestType.Gui);
            var apl = new AplGroup002.ResourceTests(interpreter);

            try {
                for (int i = 0; i < count; i++) {

                    if (testType == TestType.Echo) {
                        var result = apl.Echo(i);
                        Assert.AreEqual(i, result);
                    } else if (testType == TestType.Gui) {
                        var name = $"form{i}";
                        var result = apl.CreateForm(name);
                        Assert.IsTrue(result);
                        //result = apl.Expunge(name);
                        //Assert.IsTrue(result);
                    }
                }
            } finally {
                apl.Dispose();
                Assert.IsTrue(interpreter.Unload());
            }
            var finalGdiHandleCount = GetGdiObjectCount(process);
            var finalUserHandleCount = GetUserObjectCount(process);

            Assert.AreEqual(finalGdiHandleCount, initialGdiHandleCount);
            Assert.AreEqual(finalUserHandleCount, initialUserHandleCount);
        }

        [TestMethod]
        public void EchoReload10() {
            TestReload(TestType.Echo, 10);
        }

        [TestMethod]
        public void EchoReload100() {
            TestReload(TestType.Echo, 100);
        }

        [TestMethod]
        public void EchoReload1000() {
            TestReload(TestType.Echo, 1000);
        }

        [TestMethod]
        public void GuiReload10() {
            TestReload(TestType.Gui, 10);
        }

        [TestMethod]
        public void GuiReload100() {
            TestReload(TestType.Gui, 100);
        }

        [TestMethod]
        public void GuiReload1000() {
            TestReload(TestType.Gui, 1000);
        }

        [TestMethod]
        public void EchoSingleSession10() {
            TestSingleSession(TestType.Echo, 10);
        }

        [TestMethod]
        public void EchoSingleSession100() {
            TestSingleSession(TestType.Echo, 100);
        }

        [TestMethod]
        public void EchoSingleSession1000() {
            TestSingleSession(TestType.Echo, 1000);
        }

        [TestMethod]
        public void GuiSingleSession10() {
            TestSingleSession(TestType.Gui, 10);
        }

        [TestMethod]
        public void GuiSingleSession100() {
            TestSingleSession(TestType.Gui, 100);
        }

        [TestMethod]
        public void GuiSingleSession1000() {
            TestSingleSession(TestType.Gui, 1000);
        }

    }

}