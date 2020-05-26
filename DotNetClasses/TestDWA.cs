using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dyalog;
using Dyalog.Interop;

namespace DotNetClasses {
    class TestDWA {
        public static Localp ComplexResultSet001(Localp lp) {
            var result = new Localp(lp);
            result.mkarray(ELTYPES.APLPNTR, 3);

            // resultVector
            var vector = new Localp(result);
            vector.mkarray(ELTYPES.APLPNTR, 2);
            result.Dup(vector, new int[] { 2 }, new int[0]);
            vector.Cutp();

            // resultCountVector
            var vector1 = new Localp(result);
            vector1.mkarray(ELTYPES.APLLONG, 1);
            result.Dup(vector1, new int[] { 2, 0 }, new int[0]);
            vector1.Cutp();

            result.int32topock(0, new int[] { 0 });
            result.int32topock(0, new int[] { 1 });

            // resultMatrix - resultVector[1]
            var matrix = new Localp(result);
            matrix.mkarray(ELTYPES.APLPNTR, new int[] { 1, 2 });
            result.Dup(matrix, new int[] { 2, 1 }, new int[0]);
            matrix.Cutp();


            // columnAndNullVectors[0] - resultMatrix[0,0] - values
            var vector2 = new Localp(result);
            vector2.mkarray(ELTYPES.APLLONG, 1000);
            result.Dup(vector2, new int[] { 2, 1, 0 }, new int[0]);
            vector2.Cutp();

            // columnAndNullVectors[1] - resultMatrix[0,1] - flags
            var vector3 = new Localp(result);
            vector3.mkarray(ELTYPES.APLBOOL, 1000);
            result.Dup(vector3, new int[] { 2, 1, 1 }, new int[0]);
            vector3.Cutp();

            unsafe {
                var index = 0;
                var value = 10;
                var flag = true;
                *(int*)(result.data(new int[] { 2, 1, 0 }) + index * sizeof(int)) = value;

                byte* bp = (byte*)result.data(new int[] { 2, 1, 1 });
                long byteno = index / 8;
                byte bitno = (byte)(index % 8);
                byte boolmask = (byte)(0x80 >> bitno);
                bp += byteno;
                if (flag) {
                    *bp |= boolmask;
                } else {
                    *bp &= (byte)~boolmask;
                }
            }

            result.resizevector(1, new int[] { 2, 1, 0 });
            result.resizevector(1, new int[] { 2, 1, 1 });

            unsafe {
                *(int*)(result.data(new int[] { 2, 0 }) + 0 * sizeof(int)) = 1;
            }

            return result;
        }
        public static Localp ComplexResultSet002(Localp lp) {
            var result = new Localp(lp);
            result.mkarray(ELTYPES.APLPNTR, 3);

            // resultVector
            var vector = new Localp(result);
            vector.mkarray(ELTYPES.APLPNTR, 2);
            result.Dup(vector, new int[] { 2 }, new int[0]);

            // resultCountVector
            var vector1 = new Localp(result);
            vector1.mkarray(ELTYPES.APLLONG, 1);
            result.Dup(vector1, new int[] { 2, 0 }, new int[0]);

            result.int32topock(0, new int[] { 0 });
            result.int32topock(0, new int[] { 1 });

            // resultMatrix - resultVector[1]
            var matrix = new Localp(result);
            matrix.mkarray(ELTYPES.APLPNTR, new int[] { 1, 2 });
            result.Dup(matrix, new int[] { 2, 1 }, new int[0]);

            // columnAndNullVectors[0] - resultMatrix[0,0] - values
            var vector2 = new Localp(result);
            vector2.mkarray(ELTYPES.APLLONG, 1000);
            result.Dup(vector2, new int[] { 2, 1, 0 }, new int[0]);

            // columnAndNullVectors[1] - resultMatrix[0,1] - flags
            var vector3 = new Localp(result);
            vector3.mkarray(ELTYPES.APLBOOL, 1000);
            result.Dup(vector3, new int[] { 2, 1, 1 }, new int[0]);

            unsafe {
                var index = 0;
                var value = 10;
                var flag = true;
                *(int*)(result.data(new int[] { 2, 1, 0 }) + index * sizeof(int)) = value;

                byte* bp = (byte*)result.data(new int[] { 2, 1, 1 });
                long byteno = index / 8;
                byte bitno = (byte)(index % 8);
                byte boolmask = (byte)(0x80 >> bitno);
                bp += byteno;
                if (flag) {
                    *bp |= boolmask;
                } else {
                    *bp &= (byte)~boolmask;
                }
            }

            result.resizevector(1, new int[] { 2, 1, 0 });
            result.resizevector(1, new int[] { 2, 1, 1 });

            unsafe {
                *(int*)(result.data(new int[] { 2, 0 }) + 0 * sizeof(int)) = 1;
            }

            vector3.Cutp();
            vector2.Cutp();
            matrix.Cutp();
            vector1.Cutp();
            vector.Cutp();

            return result;
        }
        public static Localp ComplexResultSet003(Localp lp) {
            var result = new Localp(lp);
            result.mkarray(ELTYPES.APLPNTR, 3);

            // resultVector
            var vector = new Localp(result);
            vector.mkarray(ELTYPES.APLPNTR, 2);
            result.Dup(vector, new int[] { 2 }, new int[0]);

            // resultCountVector
            var vector1 = new Localp(result);
            vector1.mkarray(ELTYPES.APLLONG, 1);
            result.Dup(vector1, new int[] { 2, 0 }, new int[0]);

            result.int32topock(0, new int[] { 0 });
            result.int32topock(0, new int[] { 1 });

            // resultMatrix - resultVector[1]
            var matrix = new Localp(result);
            matrix.mkarray(ELTYPES.APLPNTR, new int[] { 1, 2 });
            result.Dup(matrix, new int[] { 2, 1 }, new int[0]);

            // columnAndNullVectors[0] - resultMatrix[0,0] - values
            var vector2 = new Localp(result);
            vector2.mkarray(ELTYPES.APLLONG, 1000);
            result.Dup(vector2, new int[] { 2, 1, 0 }, new int[0]);

            // columnAndNullVectors[1] - resultMatrix[0,1] - flags
            var vector3 = new Localp(result);
            vector3.mkarray(ELTYPES.APLBOOL, 1000);
            result.Dup(vector3, new int[] { 2, 1, 1 }, new int[0]);

            unsafe {
                var index = 0;
                var value = 10;
                var flag = true;
                *(int*)(result.data(new int[] { 2, 1, 0 }) + index * sizeof(int)) = value;

                byte* bp = (byte*)result.data(new int[] { 2, 1, 1 });
                long byteno = index / 8;
                byte bitno = (byte)(index % 8);
                byte boolmask = (byte)(0x80 >> bitno);
                bp += byteno;
                if (flag) {
                    *bp |= boolmask;
                } else {
                    *bp &= (byte)~boolmask;
                }
            }

            result.resizevector(1, new int[] { 2, 1, 0 });
            result.resizevector(1, new int[] { 2, 1, 1 });

            unsafe {
                *(int*)(result.data(new int[] { 2, 0 }) + 0 * sizeof(int)) = 1;
            }

            vector.Cutp();

            return result;
        }
        public static Localp ComplexResultSet004(Localp lp) {
            var result = new Localp(lp);
            var handle = new Localp(lp);
            result.mkarray(ELTYPES.APLPNTR, 3);

            // resultVector
            var vector = new Localp(result);
            vector.mkarray(ELTYPES.APLPNTR, 2);
            result.Dup(vector, new int[] { 2 }, new int[0]);

            // resultCountVector
            var vector1 = new Localp(result);
            vector1.mkarray(ELTYPES.APLLONG, 1);
            result.Dup(vector1, new int[] { 2, 0 }, new int[0]);

            result.int32topock(0, new int[] { 0 });
            result.int32topock(0, new int[] { 1 });

            // resultMatrix - resultVector[1]
            var matrix = new Localp(result);
            matrix.mkarray(ELTYPES.APLPNTR, new int[] { 1, 2 });
            result.Dup(matrix, new int[] { 2, 1 }, new int[0]);

            // columnAndNullVectors[0] - resultMatrix[0,0] - values
            var vector2 = new Localp(result);
            vector2.mkarray(ELTYPES.APLLONG, 1000);
            result.Dup(vector2, new int[] { 2, 1, 0 }, new int[0]);

            // columnAndNullVectors[1] - resultMatrix[0,1] - flags
            var vector3 = new Localp(result);
            vector3.mkarray(ELTYPES.APLBOOL, 1000);
            result.Dup(vector3, new int[] { 2, 1, 1 }, new int[0]);

            unsafe {
                var index = 0;
                var value = 10;
                var flag = true;
                *(int*)(result.data(new int[] { 2, 1, 0 }) + index * sizeof(int)) = value;

                byte* bp = (byte*)result.data(new int[] { 2, 1, 1 });
                long byteno = index / 8;
                byte bitno = (byte)(index % 8);
                byte boolmask = (byte)(0x80 >> bitno);
                bp += byteno;
                if (flag) {
                    *bp |= boolmask;
                } else {
                    *bp &= (byte)~boolmask;
                }
            }

            result.resizevector(1, new int[] { 2, 1, 0 });
            result.resizevector(1, new int[] { 2, 1, 1 });

            unsafe {
                *(int*)(result.data(new int[] { 2, 0 }) + 0 * sizeof(int)) = 1;
            }

            handle.Cutp();

            return result;
        }
        public static Localp ComplexResultSet005(Localp lp) {
            var result = new Localp(lp);
            result.mkarray(ELTYPES.APLPNTR, 3);

            // resultVector
            result.mkarray(ELTYPES.APLPNTR, 2, 2);

            // resultCountVector
            result.mkarray(ELTYPES.APLLONG, 1, new int[] { 2, 0 });

            result.int32topock(0, new int[] { 0 });
            result.int32topock(0, new int[] { 1 });

            // resultMatrix - resultVector[1]
            result.mkarray(ELTYPES.APLPNTR, new int[] { 1, 2 }, new int[] { 2, 1 });

            // columnAndNullVectors[0] - resultMatrix[0,0] - values
            result.mkarray(ELTYPES.APLLONG, 1000, new int[] { 2, 1, 0 });

            // columnAndNullVectors[1] - resultMatrix[0,1] - flags
            result.mkarray(ELTYPES.APLBOOL, 1000, new int[] { 2, 1, 1 });

            unsafe {
                var index = 0;
                var value = 10;
                var flag = true;
                *(int*)(result.data(new int[] { 2, 1, 0 }) + index * sizeof(int)) = value;

                byte* bp = (byte*)result.data(new int[] { 2, 1, 1 });
                long byteno = index / 8;
                byte bitno = (byte)(index % 8);
                byte boolmask = (byte)(0x80 >> bitno);
                bp += byteno;
                if (flag) {
                    *bp |= boolmask;
                } else {
                    *bp &= (byte)~boolmask;
                }
            }

            result.resizevector(1, new int[] { 2, 1, 0 });
            result.resizevector(1, new int[] { 2, 1, 1 });

            unsafe {
                *(int*)(result.data(new int[] { 2, 0 }) + 0 * sizeof(int)) = 1;
            }

            return result;
        }
        public static Localp ResizeArray001(Localp lp) {
            var result = new Localp(lp);
            result.mkarray(ELTYPES.APLPNTR, 2);

            var int32s = new Localp(lp);
            int32s.mkarray(ELTYPES.APLLONG, 10);
            result.Dup(int32s, new int[] { 0 }, new int[0]);

            var strings = new Localp(lp);
            strings.mkarray(ELTYPES.APLPNTR, 10);
            result.Dup(strings, new int[] { 1 }, new int[0]);

            for(var i = 0; i < 1000; i++) {
                if (i == result.bound(0)) {
                    result.resizevector(i + 10, 0);
                    result.resizevector(i + 10, 1);
                }

                unsafe {
                    *(int*)(result.data(0) + i * sizeof(int)) = i;
                }
                result.stringtopock(i.ToString(), new int[] { 1, i });
            }

            strings.Cutp();
            int32s.Cutp();

            return result;
        }
        public static Localp ResizeArray002(Localp lp) {
            var result = new Localp(lp);
            result.mkarray(ELTYPES.APLPNTR, 2);

            var int32s = new Localp(lp);
            int32s.mkarray(ELTYPES.APLLONG, 10);

            var strings = new Localp(lp);
            strings.mkarray(ELTYPES.APLPNTR, 10);

            for (var i = 0; i < 1000; i++) {
                if (i == int32s.bound()) {
                    int32s.resizevector(i + 10);
                    strings.resizevector(i + 10);
                }

                unsafe {
                    *(int*)(int32s.data() + i * sizeof(int)) = i;
                }
                strings.stringtopock(i.ToString(), i);
            }

            result.Dup(int32s, new int[] { 0 }, new int[0]);
            result.Dup(strings, new int[] { 1 }, new int[0]);
            strings.Cutp();
            int32s.Cutp();

            return result;

        }

    }

}
