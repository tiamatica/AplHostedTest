using System;
using System.Collections.Generic;
using System.Threading;
using SimpleAplSample;
using Dyalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Test {
    [TestClass]
    [DoNotParallelize]
    public class SimpleAplClassTests {
        private static bool DeleteOnUnload = true;
        private static readonly int TasklLoopSize = 10;
        private static List<int> LoopSizeDefinition = new List<int> { 0, 1, 100, 5000 };
      
        [TestMethod]
        public void CallWithLoopedOperationSequential() {
            foreach (int innerLoopSize in LoopSizeDefinition) {
                for (int index = 0; index < TasklLoopSize; index++) {
                    CreateAPLObjectCallWithLoopedOperationAndUnload(innerLoopSize);
                }
            }
        }

        [TestMethod]
        public void CallWithLoopedOperationParallel() {
            foreach (int innerLoopSize in LoopSizeDefinition) {
                Parallel.ForEach(Enumerable.Range(0, TasklLoopSize).ToList(), (counter) => CreateAPLObjectCallWithLoopedOperationAndUnload(innerLoopSize));
            }
        }


        [TestMethod]
        public void CallWithLoopedOperationThreaded() {
            foreach (int innerLoopSize in LoopSizeDefinition) {
                Thread[] waitHandles = new Thread[TasklLoopSize];

                for (int index = 0; index < TasklLoopSize; index++) {
                    waitHandles[index] = new Thread(() => {
                        CreateAPLObjectCallWithLoopedOperationAndUnload(innerLoopSize);
                    });
                    waitHandles[index].Start();
                }
                for (int index = 0; index < TasklLoopSize; index++) {
                    waitHandles[index].Join();
                }
            }
        }

        [TestMethod]
        public void CallWithLoopedOperationTasks() {
            foreach (int innerLoopSize in LoopSizeDefinition) {
                Task[] waitHandles = new Task[TasklLoopSize];

                for (int index = 0; index < TasklLoopSize; index++) {
                    waitHandles[index] = Task.Run(() => {
                        CreateAPLObjectCallWithLoopedOperationAndUnload(innerLoopSize);
                    });
                }
                Task.WaitAll(waitHandles);
            }
        }

        [TestMethod]
        public void CallWithLoopedOperationParallelSleepBeforeStart() {
            foreach (int innerLoopSize in LoopSizeDefinition) {
                Parallel.ForEach(Enumerable.Range(0, TasklLoopSize).ToList(), (counter) => {
                    Thread.Sleep(counter * 3);
                    CreateAPLObjectCallWithLoopedOperationAndUnload(innerLoopSize);
                });
            }
        }

        /// <summary>
        /// This test has nosence as APL session should be unloaded in same thread.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CallWithLoopedOperationParallelLateDispose() {
            foreach (int innerLoopSize in LoopSizeDefinition) {
                List<DyalogInterpreter> interpreters = new List<DyalogInterpreter>();
                Parallel.ForEach(Enumerable.Range(0, TasklLoopSize).ToList(), (counter) => {
                    DyalogInterpreter interpreter = new DyalogInterpreter() { SingleThreaded = true, DeleteOnUnload = DeleteOnUnload };
                    lock (interpreters) {
                        interpreters.Add(interpreter);
                    }
                    try {
                        var apl = new SimpleAplClass(interpreter);
                        var result = apl.Call(innerLoopSize);
                    } catch (Exception e) {

                    }
                });
                foreach (var interpreter in interpreters) {
                    interpreter.Unload();
                }
            }
        }

        /// <summary>
        /// This test has nosence as APL session should be unloaded in same thread.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CallWithLoopedOperationParallelSleepBeforeStartLateDispose() {
            foreach (int innerLoopSize in LoopSizeDefinition) {

                List<DyalogInterpreter> interpreters = new List<DyalogInterpreter>();
                Parallel.ForEach(Enumerable.Range(0, TasklLoopSize).ToList(), (counter) => {
                    Thread.Sleep(counter * 3);
                    DyalogInterpreter interpreter = new DyalogInterpreter() { SingleThreaded = true, DeleteOnUnload = DeleteOnUnload };
                    lock (interpreters) {
                        interpreters.Add(interpreter);
                    }
                    try {
                        var apl = new SimpleAplClass(interpreter);
                        var result = apl.Call(innerLoopSize);
                    } catch (Exception e) {

                    }
                });
                foreach (var interpreter in interpreters) {
                    interpreter.Unload();
                }
            }
        }

        [TestMethod]
        public void CallWithArgSequential() {
            for (int index = 0; index < TasklLoopSize; index++) {
                CreateAPLObjectCallWithArgAndUnload();
            }
        }


        [TestMethod]
        public void CallWithArgParallel() {
            Parallel.ForEach(Enumerable.Range(0, TasklLoopSize).ToList(), (counter) => CreateAPLObjectCallWithArgAndUnload());
        }

        [TestMethod]
        public void CallWithArgParallelSleepBeforeStart() {
            Parallel.ForEach(Enumerable.Range(0, TasklLoopSize).ToList(), (counter) => {
                Thread.Sleep(counter * 3);
                CreateAPLObjectCallWithArgAndUnload();
            });
        }

        /// <summary>
        /// This test has nosence as APL session should be unloaded in same thread.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CallWithArgParallelLateDispose() {
            List<DyalogInterpreter> interpreters = new List<DyalogInterpreter>();
            Parallel.ForEach(Enumerable.Range(0, TasklLoopSize).ToList(), (counter) => {
                DyalogInterpreter interpreter = new DyalogInterpreter() { SingleThreaded = true, DeleteOnUnload = DeleteOnUnload };
                lock (interpreters) {
                    interpreters.Add(interpreter);
                }
                try {
                    var apl = new SimpleAplClass(interpreter);
                    var result = apl.CallWithArgument(15);
                    Assert.AreEqual(15, result);
                } catch (Exception e) {

                }
            });
            foreach (var interpreter in interpreters) {
                Assert.IsTrue(interpreter.Unload());
            }
        }

        /// <summary>
        /// This test has nosence as APL session should be unloaded in same thread.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CallWithArgParallelSleepBeforeStartLateDispose() {
            List<DyalogInterpreter> interpreters = new List<DyalogInterpreter>();
            Parallel.ForEach(Enumerable.Range(0, TasklLoopSize).ToList(), (counter) => {
                Thread.Sleep(counter * 3);
                DyalogInterpreter interpreter = new DyalogInterpreter() { SingleThreaded = true, DeleteOnUnload = DeleteOnUnload };
                lock (interpreters) {
                    interpreters.Add(interpreter);
                }
                try {
                    var apl = new SimpleAplClass(interpreter);
                    var result = apl.CallWithArgument(15);
                    Assert.AreEqual(15, result);
                } catch (Exception e) {

                }
            });
            foreach (var interpreter in interpreters) {
                interpreter.Unload();
            }
        }
        private void CreateAPLObjectCallWithLoopedOperationAndUnload(int innerLoopSize) {
            DyalogInterpreter interpreter = new DyalogInterpreter();

            interpreter.SingleThreaded = true;
            interpreter.DeleteOnUnload = DeleteOnUnload;
            try {
                var apl = new SimpleAplClass(interpreter);
                int result = apl.Call(innerLoopSize);
                Assert.AreEqual(1, result);
            } /*catch (Exception e) {


            } 
            */finally {
                Assert.IsTrue(interpreter.Unload());
            }
        }
        private void CreateAPLObjectCallWithArgAndUnload() {
            DyalogInterpreter interpreter = new DyalogInterpreter();

            interpreter.SingleThreaded = true;
            interpreter.DeleteOnUnload = DeleteOnUnload;
            try {
                var apl = new SimpleAplClass(interpreter);
                int result = apl.CallWithArgument(15);
                Assert.AreEqual(15, result);
            } finally {
                Assert.IsTrue(interpreter.Unload());
            }
        }
    }
}
