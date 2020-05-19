using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dyalog;

namespace DotNetClasses {
    class TestDWA {
        public static Localp ComplexResultSet(Localp lp) {
            var result = new Localp(lp);
            result.mkarray(Dyalog.Interop.ELTYPES.APLPNTR, 3);

            // resultVector
            var vector = new Localp(result);
            vector.mkarray(Dyalog.Interop.ELTYPES.APLPNTR, 2);
            result.Dup(vector, new int[] { 2 }, new int[0]);
            vector.Cutp();

            // resultCountVector
            vector = new Localp(result);
            vector.mkarray(Dyalog.Interop.ELTYPES.APLLONG, 1);
            result.Dup(vector, new int[] { 2, 0 }, new int[0]);
            vector.Cutp();

            result.int32topock(0, new int[] { 0 });
            result.int32topock(0, new int[] { 1 });

            // resultMatrix - resultVector[1]
            var matrix = new Localp(result);
            matrix.mkarray(Dyalog.Interop.ELTYPES.APLPNTR, new int[] { 1, 2 });
            result.Dup(matrix, new int[] { 2, 1 }, new int[0]);
            matrix.Cutp();


            // columnAndNullVectors[0] - resultMatrix[0,0] - values
            vector = new Localp(result);
            vector.mkarray(Dyalog.Interop.ELTYPES.APLLONG, 1000);
            result.Dup(vector, new int[] { 2, 1, 0 }, new int[0]);
            vector.Cutp();

            // columnAndNullVectors[1] - resultMatrix[0,1] - flags
            vector = new Localp(result);
            vector.mkarray(Dyalog.Interop.ELTYPES.APLBOOL, 1000);
            result.Dup(vector, new int[] { 2, 1, 1 }, new int[0]);
            vector.Cutp();

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
    }
}
