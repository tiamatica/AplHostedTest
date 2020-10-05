using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AplClasses;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    [DoNotParallelize]
    public class CodeFileTests {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }
        private DyalogInterpreter CreateInterpreter(string[] codeFiles = null) {
            var interpreterSettings = new Dictionary<string, string> {
                ["RUNASSERVICE"] = "2",
                ["MAXWS"] = "1G"
            };
            var interpreter = new DyalogInterpreter(null, interpreterSettings) {
                SingleThreaded = true,
                DeleteOnUnload = true,
                UnloadWhenEmpty = false
            };
            if (codeFiles != null) {
                var aplc = new CodeFile(interpreter);
                aplc.AttachCodeFiles(codeFiles);
                aplc.Dispose();
            }
            return interpreter;
        }

        private void TestLookUpLength(int length) {
            var interpreter1 = CreateInterpreter();
            var interpreter2 = CreateInterpreter();

            var file = Path.Combine(Path.GetTempPath(), $"LookUp{length}.dwx");
            File.Delete(file);
            var test1 = new CodeFile(interpreter1);

            try {
                test1.CreateLookUp(file, length);
            } finally { }
            Assert.IsTrue(interpreter1.Unload());

            var test2 = new CodeFile(interpreter2);
            try {
                test2.AttachCodeFiles(new[] { file });
                Assert.AreEqual(0, test2.WSCheck());
            } finally { }
            Assert.IsTrue(interpreter2.Unload());
        }

        [TestMethod]
        public void TestLookUp15() {
            TestLookUpLength(15);
        }

        [TestMethod]
        public void TestLookUp16() {
            TestLookUpLength(16);
        }

        [TestMethod]
        public void TestAttachSameCodeFileTwice() {
            var codefile = Path.Combine(Path.GetTempPath(), "simplecodefile2.dwx");
            if (File.Exists(codefile)) File.Delete(codefile);
            var interpreter1 = CreateInterpreter();
            var aplc = new CodeFile(interpreter1);
            aplc.CreateSimpleCodeFile(codefile);
            aplc.Dispose();
            interpreter1.Unload();

            var interpreter2 = CreateInterpreter(new[] { codefile });
            var interpreter3 = CreateInterpreter(new[] { codefile });
            Assert.IsTrue(interpreter2.Unload());
            //Assert.IsTrue(interpreter3.Unload());

        }

        [TestMethod]
        [Ignore]
        public void TestCreateCodeFileFromSource() {

            var folderpath = Path.Combine(Path.GetTempPath(), "codefiletests");
            if (Directory.Exists(folderpath)) Directory.Delete(folderpath, true);
            Directory.CreateDirectory(folderpath);

            File.WriteAllLines(Path.Combine(folderpath, "fn1.aplf"), new string[] {
                "fn1←{1×⍵}"
            });
            File.WriteAllLines(Path.Combine(folderpath, "fn2.aplf"), new string[] {
                " fn2←{",
                "    2×⍵",
                " }"
            });

            var interpreter = CreateInterpreter();
            var test1 = new CodeFile(interpreter);
            var codefile = Path.Combine(folderpath, "fn1.dwx");

            try {
                test1.CreateCodeFileFromFolder(1, codefile, folderpath);
            } finally {
                Directory.Delete(folderpath, true);
            }
            Assert.IsTrue(interpreter.Unload());

        }

        [TestMethod]
        public void TestUseSharedCodeFileMultiHost() {
            var codefile = Path.Combine(Path.GetTempPath(), "simplecodefile.dwx");
            if (File.Exists(codefile)) File.Delete(codefile);
            var interpreter = CreateInterpreter();
            var aplc = new CodeFile(interpreter);
            aplc.CreateSimpleCodeFile(codefile);
            aplc.Dispose();
            interpreter.Unload();

            var interpreter1 = CreateInterpreter(new[] { codefile });
            var interpreter2 = CreateInterpreter(new[] { codefile });
            var interpreter3 = CreateInterpreter(new[] { codefile });

            var aplc1 = new CodeFile(interpreter1);
            var aplc2 = new CodeFile(interpreter2);
            var aplc3 = new CodeFile(interpreter3);

            aplc1.Execute("#.fn1 1");
            aplc2.Execute("#.fn1 2");
            aplc3.Execute("#.fn1 3");
            
        }
        [TestMethod]
        public void TestUseSharedCodeFileNamespaceMultiHost() {
            var codefile = Path.Combine(Path.GetTempPath(), "LookUp.dwx");
            if (File.Exists(codefile)) File.Delete(codefile);
            var interpreter = CreateInterpreter();
            var aplc = new CodeFile(interpreter);
            aplc.CreateLookUp(codefile, 100);
            aplc.Dispose();
            interpreter.Unload();

            var interpreter1 = CreateInterpreter(new[] { codefile });
            var interpreter2 = CreateInterpreter(new[] { codefile });
            var interpreter3 = CreateInterpreter(new[] { codefile });

        }

    }
}
