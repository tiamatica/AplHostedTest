using System;
using System.Collections.Generic;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class TestNameScopes {

        private DyalogInterpreter MultiHostNullNull() {
            //return new DyalogInterpreter(null, null) {
            return new DyalogInterpreter(".\\dyalog180_64_unicode.dll", null) {
                SingleThreaded = true,
                DeleteOnUnload = true
            };
        }


        [TestMethod]
        public void EchoNoDispose() {
            var interpreterSettings = new Dictionary<string, string> {
                ["RUNASSERVICE"] = "2",
                ["MAXWS"] = "1G"
            };
            for (int i = 0; i < 1000; i++) {
                var interpreter = new DyalogInterpreter(null, interpreterSettings) {
                    SingleThreaded = true,
                    DeleteOnUnload = true
                };

                try {
                    var apl = new AplGroup001.SubSpace.A1(interpreter);
                    var result = apl.Echo(i);
                    Assert.AreEqual(i, result);
                } finally {
                    interpreter.Unload();
                }
            }
        }

        [TestMethod]
        public void EchoWithDispose() {
            var interpreterSettings = new Dictionary<string, string> {
                ["RUNASSERVICE"] = "2",
                ["MAXWS"] = "1G"
            };
            for (int i = 0; i < 1000; i++) {
                var interpreter = new DyalogInterpreter(null, interpreterSettings) {
                    SingleThreaded = true,
                    DeleteOnUnload = true
                };

                try {
                    var apl = new AplGroup001.SubSpace.A1(interpreter);
                    var result = apl.Echo(i);
                    Assert.AreEqual(i, result);
                    apl.Dispose();
                } finally {
                    interpreter.Unload();
                }
            }
        }

        [TestMethod]
        public void NestedClassSameAssemblySimpleName() {
            var int1 = MultiHostNullNull();

            try {
                var apl1 = new AplGroup002.NestedClassSimpleName(1, int1);
                Assert.AreEqual(1, apl1.ID());
            } finally {
                int1.Unload();
            }
        }

        [TestMethod]
        public void NestedClassSameAssemblyQualifiedName() {
            var int1 = MultiHostNullNull();

            try {
                var apl1 = new AplGroup002.NestedClassQualifiedName(1, int1);
                Assert.AreEqual(1, apl1.ID());
            } finally {
                int1.Unload();
            }
        }

        [TestMethod]
        public void DerivedClassSameAssemblySimpleName() {
            var int1 = MultiHostNullNull();

            try {
                var apl1 = new AplGroup002.DerivedClassSimpleName(1, int1);
                Assert.AreEqual(1, apl1.ID());
            } finally {
                int1.Unload();
            }
        }

        [TestMethod]
        [Ignore]
        public void DerivedClassSameAssemblyQualifiedName() {
            var int1 = MultiHostNullNull();

            try {
                //var apl1 = new AplGroup002.DerivedClassQualifiedName(1, int1);
                //Assert.AreEqual(1, apl1.ID());
            } finally {
                int1.Unload();
            }
        }

        [TestMethod]
        public void SimpleClassSingleInterpreter() {
            var int1 = MultiHostNullNull();

            try {
                var apl1 = new AplGroup002.SimpleClass(1, int1);
                Assert.AreEqual(1, apl1.ID());

                var apl2 = new AplGroup002.SimpleClass(2, int1);
                Assert.AreEqual(2, apl1.ID());
                Assert.AreEqual(2, apl2.ID());
            } finally {
                int1.Unload();
            }
        }

        [TestMethod]
        public void SimpleClassSplitInterpreter() {
            var int1 = MultiHostNullNull();
            var int2 = MultiHostNullNull();

            try {
                var apl1 = new AplGroup002.SimpleClass(1, int1);
                Assert.AreEqual(1, apl1.ID());

                var apl2 = new AplGroup002.SimpleClass(2, int2);
                Assert.AreEqual(1, apl1.ID());
                Assert.AreEqual(2, apl2.ID());
            } finally {
                int1.Unload();
                int2.Unload();
            }
        }

        [TestMethod]
        public void NestedInstanceSingleInterpreterSimpleName() {
            var interpreter = MultiHostNullNull();

            try {
                var apl = new AplGroup003.NestedClassSimpleName(1, interpreter);
                Assert.AreEqual(1, apl.ID());
                Assert.AreEqual(1, apl.ThisID());
                apl.Init();
                Assert.AreEqual(2, apl.ID());
                Assert.AreEqual(2, apl.NestedID());
                Assert.AreEqual(2, apl.ThisID());
            } finally {
                interpreter.Unload();
            }
        }

        [TestMethod]
        public void NestedInstanceSingleInterpreterQualifiedName() {
            var interpreter = MultiHostNullNull();

            try {
                var apl = new AplGroup003.NestedClassQualifiedName(1, interpreter);
                Assert.AreEqual(1, apl.ID());
                Assert.AreEqual(1, apl.ThisID());
                apl.Init();
                Assert.AreEqual(2, apl.ID());
                Assert.AreEqual(2, apl.NestedID());
                Assert.AreEqual(2, apl.ThisID());
            } finally {
                interpreter.Unload();
            }
        }

        [TestMethod]
        public void NestedInstanceSplitInterpreterSimpleName() {
            var interpreter1 = MultiHostNullNull();
            var interpreter2 = MultiHostNullNull();

            try {
                var apl1 = new AplGroup003.NestedClassSimpleName(1, interpreter1);
                Assert.AreEqual(1, apl1.ID());
                Assert.AreEqual(1, apl1.NestedID());
                Assert.AreEqual(1, apl1.ThisID());
                apl1.Init();
                Assert.AreEqual(2, apl1.ID());
                Assert.AreEqual(2, apl1.NestedID());
                Assert.AreEqual(2, apl1.ThisID());

                var apl2 = new AplGroup003.NestedClassSimpleName(11, interpreter2);
                Assert.AreEqual(11, apl2.ID());
                Assert.AreEqual(11, apl2.NestedID());
                Assert.AreEqual(11, apl2.ThisID());
                apl2.Init();
                Assert.AreEqual(12, apl2.ID());
                Assert.AreEqual(12, apl2.NestedID());
                Assert.AreEqual(12, apl2.ThisID());
            } finally {
                interpreter1.Unload();
                interpreter2.Unload();
            }
        }

        [TestMethod]
        public void NestedInstanceSplitInterpreterQualifiedName() {
            var interpreter1 = MultiHostNullNull();
            var interpreter2 = MultiHostNullNull();

            try {
                var apl1 = new AplGroup003.NestedClassQualifiedName(1, interpreter1);
                Assert.AreEqual(1, apl1.ID());
                Assert.AreEqual(1, apl1.ThisID());
                apl1.Init();
                Assert.AreEqual(2, apl1.ID());
                Assert.AreEqual(2, apl1.NestedID());
                Assert.AreEqual(2, apl1.ThisID());

                var apl2 = new AplGroup003.NestedClassQualifiedName(11, interpreter2);
                Assert.AreEqual(11, apl2.ID());
                Assert.AreEqual(11, apl2.ThisID());
                apl2.Init();
                Assert.AreEqual(12, apl2.ID());
                Assert.AreEqual(12, apl2.NestedID());
                Assert.AreEqual(12, apl2.ThisID());
            } finally {
                interpreter1.Unload();
                interpreter2.Unload();
            }
        }

        [TestMethod]
        public void NestedInstanceSingleHost() {
            var interpreter = new DyalogInterpreter(".\\dyalog180_64_unicode.dll", null) {
                SingleThreaded = true,
                DeleteOnUnload = true
            };

            try {
                var apl = new AplGroup003.NestedClassSimpleName(1, interpreter);
                Assert.AreEqual(1, apl.ID());
                Assert.AreEqual(1, apl.NestedID());
                Assert.AreEqual(1, apl.ThisID());
                apl.Init();
                Assert.AreEqual(2, apl.ID());
                Assert.AreEqual(2, apl.NestedID());
                Assert.AreEqual(2, apl.ThisID());
            } finally {
                interpreter.Unload();
            }

        }
        
        [TestMethod]
        public void InterfaceTestWithCast() {
            var interpreter = MultiHostNullNull();

            try {
                var apl = new AplInterfaceTest.Herd(interpreter);

                Assert.AreEqual(0, apl.FlyWithCast().Length);
                apl.AddPenguinWithCast();
                Assert.AreEqual(1, apl.FlyWithCast().Length);

            } finally {
                interpreter.Unload();
            }

        }

        [TestMethod]
        public void InterfaceTestNoCast() {
            var interpreter = MultiHostNullNull();

            try {
                var apl = new AplInterfaceTest.Herd(interpreter);

                Assert.AreEqual(0, apl.FlyNoCast().Length);
                apl.AddPenguinNoCast();
                Assert.AreEqual(1, apl.FlyNoCast().Length);

            } finally {
                interpreter.Unload();
            }

        }
    }
}
