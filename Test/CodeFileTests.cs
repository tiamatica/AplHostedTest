using System;
using System.Collections.Generic;
using System.IO;
using AplClasses;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
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

            var file = Path.Combine(Path.GetTempPath(), "LookUp.dwx");
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
            var codefile = Path.Combine(Path.GetTempPath(), "simplecodefile.dwx");
            if (File.Exists(codefile)) File.Delete(codefile);
            var interpreter1 = CreateInterpreter();
            var aplc = new CodeFile(interpreter1);
            aplc.CreateSimpleCodeFile(codefile);
            aplc.Dispose();
            interpreter1.Unload();

            var interpreter2 = CreateInterpreter(new[] { codefile });
            var interpreter3 = CreateInterpreter(new[] { codefile });
            Assert.IsTrue(interpreter2.Unload());
            Assert.IsTrue(interpreter3.Unload());

        }

        [TestMethod]
        public void TestCreateCodeFileFromSource() {

            var folderpath = Path.Combine(Path.GetTempPath(), "codefiletests");
            if (Directory.Exists(folderpath)) Directory.Delete(folderpath, true);
            Directory.CreateDirectory(folderpath);

            var fn1 = Path.Combine(folderpath, "fn1.aplf");
            File.WriteAllLines(fn1, new string[] { "fn1←{1×⍵}" }, System.Text.Encoding.UTF8);

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

        
    }
}
