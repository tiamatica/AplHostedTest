using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Linq;

namespace DotNetClasses {
    public static class TypeConversion {
        private const long IntAsDoubleMaxValue = 9007199254740992; // 2*53

        public enum NetTypes {
            Boolean = 1,
            Int32 = 2,
            NullableInt32 = 3,
            UInt32 = 4,
            NullableUInt32 = 5,
            Double = 6,
            NullableDouble = 7,
            Char = 8,
            NullableChar = 9,
            String = 10,
            DateTime = 11,
            NullableDateTime = 12,
            TimeSpan = 13,
            NullableTimeSpan = 14,
            Object = 15,
            DomainValue = 16,
            Color = 17,
            FXRate = 18,
            NullableFXRate = 19,
            Byte = 20,
            Int64 = 21,
            NullableInt64 = 22
        }

        public static Type TypeOfNetType(NetTypes nettype) {
            switch (nettype) {
                case NetTypes.Boolean: {
                    return typeof(bool);
                }
                case NetTypes.Int32: {
                    return typeof(int);
                }
                case NetTypes.NullableInt32: {
                    return typeof(int?);
                }
                case NetTypes.UInt32: {
                    return typeof(uint);
                }
                case NetTypes.NullableUInt32: {
                    return typeof(uint?);
                }
                case NetTypes.Double: {
                    return typeof(double);
                }
                case NetTypes.NullableDouble: {
                    return typeof(double?);
                }
                case NetTypes.Char: {
                    return typeof(char);
                }
                case NetTypes.NullableChar: {
                    return typeof(char?);
                }
                case NetTypes.String: {
                    return typeof(string);
                }
                case NetTypes.DateTime: {
                    return typeof(DateTime);
                }
                case NetTypes.NullableDateTime: {
                    return typeof(DateTime?);
                }
                case NetTypes.TimeSpan: {
                    return typeof(TimeSpan);
                }
                case NetTypes.NullableTimeSpan: {
                    return typeof(TimeSpan?);
                }
                case NetTypes.Object: {
                    return typeof(object);
                }
                //case NetTypes.DomainValue: {
                //    return typeof(IDomainValue);
                //}
                //case NetTypes.Color: {
                //    return typeof(Color);
                //}
                //case NetTypes.FXRate: {
                //    return typeof(Precision);
                //}
                //case NetTypes.NullableFXRate: {
                //    return typeof(Precision?);
                //}
                case NetTypes.Byte: {
                    return typeof(byte);
                }
                case NetTypes.Int64: {
                    return typeof(long);
                }
                case NetTypes.NullableInt64: {
                    return typeof(long?);
                }
                default: {
                    throw new UnsupportedNetType("Conversion of Nettypes value {0} is not supported", (int)nettype);
                }
            }
        }

        public static NetTypes NetTypeOfType(Type type) {
            if (type == typeof(bool)) {
                return NetTypes.Boolean;
            } else if (type == typeof(int)) {
                return NetTypes.Int32;
            } else if (type == typeof(int?)) {
                return NetTypes.NullableInt32;
            } else if (type == typeof(uint)) {
                return NetTypes.UInt32;
            } else if (type == typeof(uint?)) {
                return NetTypes.NullableUInt32;
            } else if (type == typeof(double)) {
                return NetTypes.Double;
            } else if (type == typeof(double?)) {
                return NetTypes.NullableDouble;
            } else if (type == typeof(char)) {
                return NetTypes.Char;
            } else if (type == typeof(char?)) {
                return NetTypes.NullableChar;
            } else if (type == typeof(string)) {
                return NetTypes.String;
            } else if (type == typeof(DateTime)) {
                return NetTypes.DateTime;
            } else if (type == typeof(DateTime?)) {
                return NetTypes.NullableDateTime;
            } else if (type == typeof(TimeSpan)) {
                return NetTypes.TimeSpan;
            } else if (type == typeof(TimeSpan?)) {
                return NetTypes.NullableTimeSpan;
            //} else if (type == typeof(IDomainValue) || type.GetInterface(typeof(IDomainValue).FullName) != null) {
            //    return NetTypes.DomainValue;
            //} else if (type == typeof(Color)) {
            //    return NetTypes.Color;
            //} else if (type == typeof(Precision)) {
            //    return NetTypes.FXRate;
            //} else if (type == typeof(Precision?)) {
            //    return NetTypes.NullableFXRate;
            } else if (type == typeof(byte)) {
                return NetTypes.Byte;
            } else if (type == typeof(long)) {
                return NetTypes.Int64;
            } else if (type == typeof(long?)) {
                return NetTypes.NullableInt64;
            } else {
                return NetTypes.Object;
            }
        }

        public static object ToNetType(object value, NetTypes nettype) {
            switch (nettype) {
                case NetTypes.Boolean: {
                    return ToBooleanScalar(value);
                }
                case NetTypes.Int32: {
                    return ToInt32Scalar(value);
                }
                case NetTypes.NullableInt32: {
                    return ToNullableInt32Scalar(value);
                }
                case NetTypes.UInt32: {
                    return ToUInt32Scalar(value);
                }
                case NetTypes.NullableUInt32: {
                    return ToNullableUInt32Scalar(value);
                }
                case NetTypes.Double: {
                    return ToDoubleScalar(value);
                }
                case NetTypes.NullableDouble: {
                    return ToNullableDoubleScalar(value);
                }
                case NetTypes.Char: {
                    return ToCharScalar(value);
                }
                case NetTypes.NullableChar: {
                    return ToNullableCharScalar(value);
                }
                case NetTypes.String: {
                    return ToNetStringsWithLineBreaks(ToStringScalar(value));
                }
                case NetTypes.TimeSpan: {
                    return ToTimeSpanScalar(value);
                }
                case NetTypes.NullableTimeSpan: {
                    return ToNullableTimeSpanScalar(value);
                }
                case NetTypes.Object: {
                    return ToObjectScalar(value);
                }
                case NetTypes.Byte: {
                    return ToByteScalar(value);
                }
                case NetTypes.Int64: {
                    return ToInt64Scalar(value);
                }
                case NetTypes.NullableInt64: {
                    return ToNullableInt64Scalar(value);
                }
                default: {
                    throw new UnsupportedNetType("Conversion of Nettypes value {0} is not supported", (int)nettype);
                }
            }
        }

        public static bool ToBooleanScalar(object value) {
            return value == null || (value.GetType() == typeof(string) && ((string)value == "")) || value is Array ? default(bool) : Convert.ToBoolean(value);
        }

        public static bool[] ToBooleanVector(Array values) {
            bool[] result = new bool[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToBooleanScalar(values.GetValue(index));
            }
            return result;
        }

        public static bool[,] ToBooleanMatrix(Array values) {
            bool[,] result = new bool[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToBooleanScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static bool[][] ToBooleanVectorOfVectors(Array values) {
            bool[][] result = new bool[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToBooleanVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static byte ToByteScalar(object value) {
            if (value is Array) {
                return default(byte);
            }
            unchecked {
                sbyte sByte = Convert.ToSByte(value);
                return (byte)sByte;
            }
        }

        public static byte[] ToByteVector(Array values) {
            byte[] result = new byte[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToByteScalar(values.GetValue(index));
            }
            return result;
        }

        public static byte[,] ToByteMatrix(Array values) {
            byte[,] result = new byte[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToByteScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static byte[][] ToByteVectorOfVectors(Array values) {
            byte[][] result = new byte[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToByteVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static byte[,][] ToByteMatrixOfVectors(Array values) {
            byte[,][] result = new byte[values.GetLength(0), values.GetLength(1)][];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToByteVector((Array)values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static int ToInt32Scalar(object value) {
            return value is Array ? default(int) : Convert.ToInt32(value);
        }

        public static int[] ToInt32Vector(Array values) {
            int[] result = new int[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToInt32Scalar(values.GetValue(index));
            }
            return result;
        }

        public static int[,] ToInt32Matrix(Array values) {
            int[,] result = new int[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToInt32Scalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static int[][] ToInt32VectorOfVectors(Array values) {
            int[][] result = new int[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToInt32Vector((Array)values.GetValue(index));
            }
            return result;
        }

        public static int[,][] ToInt32MatrixOfVectors(Array values) {
            int[,][] result = new int[values.GetLength(0), values.GetLength(1)][];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToInt32Vector((Array)values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static int? ToNullableInt32Scalar(object value) {
            return value is Array ? default(int?) : Convert.ToInt32(value);
        }

        public static int?[] ToNullableInt32Vector(Array values) {
            int?[] result = new int?[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableInt32Scalar(values.GetValue(index));
            }
            return result;
        }

        public static int?[,] ToNullableInt32Matrix(Array values) {
            int?[,] result = new int?[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToNullableInt32Scalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static int?[][] ToNullableInt32VectorOfVectors(Array values) {
            int?[][] result = new int?[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableInt32Vector((Array)values.GetValue(index));
            }
            return result;
        }

        public static uint ToUInt32Scalar(object value) {
            return value is Array ? default(uint) : Convert.ToUInt32(value);
        }

        public static uint[] ToUInt32Vector(Array values) {
            uint[] result = new uint[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToUInt32Scalar(values.GetValue(index));
            }
            return result;
        }

        public static uint[,] ToUInt32Matrix(Array values) {
            uint[,] result = new uint[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToUInt32Scalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static uint[][] ToUInt32VectorOfVectors(Array values) {
            uint[][] result = new uint[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToUInt32Vector((Array)values.GetValue(index));
            }
            return result;
        }

        public static uint? ToNullableUInt32Scalar(object value) {
            return value is Array ? default(uint?) : Convert.ToUInt32(value);
        }

        public static uint?[] ToNullableUInt32Vector(Array values) {
            uint?[] result = new uint?[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableUInt32Scalar(values.GetValue(index));
            }
            return result;
        }

        public static uint?[,] ToNullableUInt32Matrix(Array values) {
            uint?[,] result = new uint?[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToNullableUInt32Scalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static uint?[][] ToNullableUInt32VectorOfVectors(Array values) {
            uint?[][] result = new uint?[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableUInt32Vector((Array)values.GetValue(index));
            }
            return result;
        }

        public static double ToDoubleScalar(object value) {
            return value is Array ? default(double) : Convert.ToDouble(value);
        }

        public static double[] ToDoubleVector(Array values) {
            double[] result = new double[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToDoubleScalar(values.GetValue(index));
            }
            return result;
        }

        public static double[,] ToDoubleMatrix(Array values) {
            double[,] result = new double[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToDoubleScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static double[][] ToDoubleVectorOfVectors(Array values) {
            double[][] result = new double[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToDoubleVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static double? ToNullableDoubleScalar(object value) {
            return value is Array ? default(double?) : Convert.ToDouble(value);
        }

        public static double?[] ToNullableDoubleVector(Array values) {
            double?[] result = new double?[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableDoubleScalar(values.GetValue(index));
            }
            return result;
        }

        public static double?[,] ToNullableDoubleMatrix(Array values) {
            double?[,] result = new double?[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToNullableDoubleScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static double?[][] ToNullableDoubleVectorOfVectors(Array values) {
            double?[][] result = (double?[][])Array.CreateInstance(typeof(double?[]), values.Length);
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableDoubleVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static char ToCharScalar(object value) {
            return value is Array ? default(char) : Convert.ToChar(value);
        }

        public static char[] ToCharVector(string values) {
            return values.ToCharArray();
        }

        public static char[] ToCharVector(Array values) {
            char[] result = new char[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToCharScalar(values.GetValue(index));
            }
            return result;
        }

        public static char[,] ToCharMatrix(Array values) {
            char[,] result = new char[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToCharScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static char[][] ToCharVectorOfVectors(Array values) {
            char[][] result = new char[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToCharVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static char? ToNullableCharScalar(object value) {
            return value is Array ? default(char?) : Convert.ToChar(value);
        }

        public static char?[] ToNullableCharVector(Array values) {
            char?[] result = new char?[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableCharScalar(values.GetValue(index));
            }
            return result;
        }

        public static char?[,] ToNullableCharMatrix(Array values) {
            char?[,] result = new char?[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToNullableCharScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static char?[][] ToNullableCharVectorOfVectors(Array values) {
            char?[][] result = new char?[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableCharVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static string ToStringScalar(object value) {
            return value is Array ? "" : Convert.ToString(value);
        }

        public static string[] ToStringVector(Array values) {
            string[] result = new string[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToStringScalar(values.GetValue(index));
            }
            return result;
        }

        public static string[,] ToStringMatrix(Array values) {
            string[,] result = new string[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToStringScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static string[][] ToStringVectorOfVectors(Array values) {
            string[][] result = new string[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToStringVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static string ToNetStringsWithLineBreaks(String value) {
            if (null != value) {
                value = value.Replace("\r\n", "\r");
                value = value.Replace("\r", "\r\n");
                value = value.Replace("^", "\r\n");
            }
            return value;
        }

        public static string ToNetStringsWithOutLineBreaks(String value) {
            if (null != value) {
                value = value.Replace("\r\n", " ");
                value = value.Replace("\r", " ");
                value = value.Replace("^", " ");
            }
            return value;
        }

        public static TimeSpan ToTimeSpanScalar(object value) {
            return value is Array ? new TimeSpan() : new TimeSpan(0, 0, Convert.ToInt32(86400D * Convert.ToDouble(value)));
        }

        public static TimeSpan[] ToTimeSpanVector(Array values) {
            TimeSpan[] result = new TimeSpan[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToTimeSpanScalar(values.GetValue(index));
            }
            return result;
        }

        public static TimeSpan[,] ToTimeSpanMatrix(Array values) {
            TimeSpan[,] result = new TimeSpan[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToTimeSpanScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static TimeSpan[][] ToTimeSpanVectorOfVectors(Array values) {
            TimeSpan[][] result = new TimeSpan[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToTimeSpanVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static TimeSpan? ToNullableTimeSpanScalar(object value) {
            return value is Array ? default(TimeSpan?) : new TimeSpan(0, 0, Convert.ToInt32(86400M * (Convert.ToDecimal(value) - Decimal.Truncate(Convert.ToDecimal(value)))));
        }

        public static TimeSpan?[] ToNullableTimeSpanVector(Array values) {
            TimeSpan?[] result = new TimeSpan?[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableTimeSpanScalar(values.GetValue(index));
            }
            return result;
        }

        public static TimeSpan?[,] ToNullableTimeSpanMatrix(Array values) {
            TimeSpan?[,] result = new TimeSpan?[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToNullableTimeSpanScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static TimeSpan?[][] ToNullableTimeSpanVectorOfVectors(Array values) {
            TimeSpan?[][] result = new TimeSpan?[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableTimeSpanVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static long ToInt64Scalar(object value) {
            return value is Array ? default(long) : Convert.ToInt64(value);
        }

        public static long[] ToInt64Vector(Array values) {
            long[] result = new long[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToInt64Scalar(values.GetValue(index));
            }
            return result;
        }

        public static long[,] ToInt64Matrix(Array values) {
            long[,] result = new long[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToInt64Scalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static long[][] ToInt64VectorOfVectors(Array values) {
            long[][] result = new long[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToInt64Vector((Array)values.GetValue(index));
            }
            return result;
        }

        public static long? ToNullableInt64Scalar(object value) {
            return value is Array ? default(long?) : Convert.ToInt64(value);
        }

        public static long?[] ToNullableInt64Vector(Array values) {
            long?[] result = new long?[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableInt64Scalar(values.GetValue(index));
            }
            return result;
        }

        public static long?[,] ToNullableInt64Matrix(Array values) {
            long?[,] result = new long?[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = ToNullableInt64Scalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static long?[][] ToNullableInt64VectorOfVectors(Array values) {
            long?[][] result = new long?[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = ToNullableInt64Vector((Array)values.GetValue(index));
            }
            return result;
        }

        public static object ToObjectScalar(object value) {
            return value;
        }

        public static object[] ToObjectVector(Array values) {
            if (values is object[]) {
                return (object[])values;
            } else {
                object[] result = new object[values.Length];
                values.CopyTo(result, 0);
                return result;
            }
        }

        public static object[,] ToObjectMatrix(Array values) {
            if (values is object[,]) {
                return (object[,])values;
            } else {
                object[,] result = new object[values.GetLength(0), values.GetLength(1)];
                for (int rowindex = 0; rowindex < result.GetLength(0); ++rowindex) {
                    for (int columnindex = 0; columnindex < result.GetLength(1); ++columnindex) {
                        result[rowindex, columnindex] = values.GetValue(rowindex, columnindex);
                    }
                }
                return result;
            }
        }

        public static object[][] ToObjectVectorOfVectors(Array values) {
            object[][] result = new object[values.Length][];
            for (int index = 0; index < values.Length; index++) {
                result[index] = ToObjectVector((Array)values.GetValue(index));
            }
            return result;
        }

        public static Type APLTypeOfNetType(NetTypes nettype) {
            switch (nettype) {
                case NetTypes.Boolean: {
                    return typeof(bool);
                }
                case NetTypes.Int32: {
                    return typeof(int);
                }
                case NetTypes.NullableInt32: {
                    return typeof(object);
                }
                case NetTypes.UInt32: {
                    return typeof(uint);
                }
                case NetTypes.NullableUInt32: {
                    return typeof(object);
                }
                case NetTypes.Double: {
                    return typeof(double);
                }
                case NetTypes.NullableDouble: {
                    return typeof(object);
                }
                case NetTypes.Char: {
                    return typeof(char);
                }
                case NetTypes.NullableChar: {
                    return typeof(object);
                }
                case NetTypes.String: {
                    return typeof(string);
                }
                case NetTypes.DateTime: {
                    return typeof(double);
                }
                case NetTypes.NullableDateTime: {
                    return typeof(object);
                }
                case NetTypes.TimeSpan: {
                    return typeof(double);
                }
                case NetTypes.NullableTimeSpan: {
                    return typeof(object);
                }
                case NetTypes.Object: {
                    return typeof(object);
                }
                case NetTypes.DomainValue: {
                    return typeof(object);
                }
                case NetTypes.Color: {
                    return typeof(int[]);
                }
                case NetTypes.FXRate: {
                    return typeof(Array);
                }
                case NetTypes.NullableFXRate: {
                    return typeof(Array);
                }
                case NetTypes.Byte: {
                    return typeof(sbyte);
                }
                case NetTypes.Int64: {
                    return typeof(double);
                }
                case NetTypes.NullableInt64: {
                    return typeof(object);
                }
                default: {
                    throw new UnsupportedNetType("Conversion of Nettypes value {0} is not supported", (int)nettype);
                }
            }
        }

        public static object FromNetType(object value, NetTypes nettype) {
            switch (nettype) {
                case NetTypes.Boolean: {
                    return value;
                }
                case NetTypes.Int32: {
                    return value;
                }
                case NetTypes.NullableInt32: {
                    return FromNullableScalar(value);
                }
                case NetTypes.UInt32: {
                    return value;
                }
                case NetTypes.NullableUInt32: {
                    return FromNullableScalar(value);
                }
                case NetTypes.Double: {
                    return value;
                }
                case NetTypes.NullableDouble: {
                    return FromNullableScalar(value);
                }
                case NetTypes.Char: {
                    return value;
                }
                case NetTypes.NullableChar: {
                    return FromNullableScalar(value);
                }
                case NetTypes.String: {
                    return FromStringScalar((string)value);
                }
                case NetTypes.TimeSpan: {
                    return FromTimeSpanScalar((TimeSpan)value);
                }
                case NetTypes.NullableTimeSpan: {
                    return FromNullableTimeSpanScalar((TimeSpan?)value);
                }
                case NetTypes.Object: {
                    return FromObjectScalar(value);
                }
                case NetTypes.Byte: {
                    return FromByteScalar((byte)value);
                }
                case NetTypes.Int64: {
                    return FromInt64Scalar((long)value);
                }
                case NetTypes.NullableInt64: {
                    return FromNullableInt64Scalar((long?)value);
                }
                default: {
                    throw new UnsupportedNetType("Conversion of Nettypes value {0} is not supported", (int)nettype);
                }
            }
        }

        public static sbyte FromByteScalar(byte value) {
            unchecked {
                return (sbyte)value;
            }
        }

        public static sbyte[] FromByteVector(byte[] values) {
            sbyte[] result = new sbyte[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromByteScalar(values[index]);
            }
            return result;
        }

        public static sbyte[,] FromByteMatrix(byte[,] values) {
            sbyte[,] result = new sbyte[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = FromByteScalar(values[rowindex, columnindex]);
                }
            }
            return result;
        }

        public static sbyte[][] FromByteVectorOfVectors(byte[][] values) {
            sbyte[][] result = new sbyte[values.GetLength(0)][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromByteVector(values[index]);
            }
            return result;
        }

        public static Array FromNetType(IList values, NetTypes nettype) {
            Array result = Array.CreateInstance(APLTypeOfNetType(nettype), values.Count);
            for (int index = 0; index < values.Count; ++index) {
                result.SetValue(FromNetType(values[index], nettype), index);
            }
            return result;
        }

        public static object FromNullableScalar(object value) {
            return value != null ? value : new int[] { };
        }

        public static object[] FromNullableVector(Array values) {
            object[] result = new object[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromNullableScalar(values.GetValue(index));
            }
            return result;
        }

        public static object[,] FromNullableMatrix(Array values) {
            object[,] result = new object[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = FromNullableScalar(values.GetValue(rowindex, columnindex));
                }
            }
            return result;
        }

        public static object[][] FromNullableVectorOfVectors(Array[] values) {
            object[][] result = new object[values.Length][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromNullableVector(values[index]);
            }
            return result;
        }

        public static double FromInt64Scalar(long value) {
            if (value < -IntAsDoubleMaxValue || value > IntAsDoubleMaxValue) {
                throw new ArgumentOutOfRangeException("value", string.Format("Cannot convert {0} to APL representation without losing precision", value));
            }
            return Convert.ToDouble(value);
        }

        public static double[] FromInt64Vector(long[] values) {
            double[] result = new double[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromInt64Scalar(values[index]);
            }
            return result;
        }

        public static double[,] FromInt64Matrix(long[,] values) {
            double[,] result = new double[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = FromInt64Scalar(values[rowindex, columnindex]);
                }
            }
            return result;
        }

        public static double[][] FromInt64VectorOfVectors(long[][] values) {
            double[][] result = new double[values.GetLength(0)][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromInt64Vector(values[index]);
            }
            return result;
        }

        public static object FromNullableInt64Scalar(long? value) {
            return value.HasValue ? (object)FromInt64Scalar(value.Value) : new int[] { };
        }

        public static object[] FromNullableInt64Vector(long?[] values) {
            object[] result = new object[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromNullableInt64Scalar(values[index]);
            }
            return result;
        }

        public static object[,] FromNullableInt64Matrix(long?[,] values) {
            object[,] result = new object[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = FromNullableInt64Scalar(values[rowindex, columnindex]);
                }
            }
            return result;
        }

        public static object[][] FromNullableInt64VectorOfVectors(long?[][] values) {
            object[][] result = new object[values.GetLength(0)][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromNullableInt64Vector(values[index]);
            }
            return result;
        }

        public static string FromStringScalar(string value) {
            return value != null ? value.Replace("\r\n", "\r"): "";
        }

        public static string[] FromStringVector(string[] values) {
            string[] result = new string[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromStringScalar(values[index]);
            }
            return result;
        }

        public static string[,] FromStringMatrix(string[,] values) {
            string[,] result = new string[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = FromStringScalar(values[rowindex, columnindex]);
                }
            }
            return result;
        }

        public static string[][] FromStringVectorOfVectors(string[][] values) {
            string[][] result = new string[values.GetLength(0)][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromStringVector(values[index]);
            }
            return result;
        }

        public static double FromTimeSpanScalar(TimeSpan value) {
            return value.Days + (3600000 * value.Hours + 60000 * value.Minutes + 1000 * value.Seconds + value.Milliseconds) / 86400000.0D;
        }

        public static double[] FromTimeSpanVector(TimeSpan[] values) {
            double[] result = new double[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromTimeSpanScalar(values[index]);
            }
            return result;
        }

        public static double[,] FromTimeSpanMatrix(TimeSpan[,] values) {
            double[,] result = new double[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = FromTimeSpanScalar(values[rowindex, columnindex]);
                }
            }
            return result;
        }

        public static double[][] FromTimeSpanVectorOfVectors(TimeSpan[][] values) {
            double[][] result = new double[values.GetLength(0)][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromTimeSpanVector(values[index]);
            }
            return result;
        }

        public static object FromNullableTimeSpanScalar(TimeSpan? value) {
            return value.HasValue ? (object)FromTimeSpanScalar(value.Value) : new int[] { };

        }

        public static object[] FromNullableTimeSpanVector(TimeSpan?[] values) {
            object[] result = new object[values.Length];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromNullableTimeSpanScalar(values[index]);
            }
            return result;
        }

        public static object[,] FromNullableTimeSpanMatrix(TimeSpan?[,] values) {
            object[,] result = new object[values.GetLength(0), values.GetLength(1)];
            for (int rowindex = 0; rowindex < values.GetLength(0); ++rowindex) {
                for (int columnindex = 0; columnindex < values.GetLength(1); ++columnindex) {
                    result[rowindex, columnindex] = FromNullableTimeSpanScalar(values[rowindex, columnindex]);
                }
            }
            return result;
        }

        public static object[][] FromNullableTimeSpanVectorOfVectors(TimeSpan?[][] values) {
            object[][] result = new object[values.GetLength(0)][];
            for (int index = 0; index < values.Length; ++index) {
                result[index] = FromNullableTimeSpanVector(values[index]);
            }
            return result;
        }

        public static object FromObjectScalar(object value) {
            return value != null ? value : new int[] { };
        }

        public static object[] FromObjectVector(object[] values) {
            return values;
        }

        public static object[,] FromObjectMatrix(object[,] values) {
            return values;
        }

        public static object[][] FromObjectVectorOfVectors(object[][] values) {
            return values;
        }

        public static TTo[] FromListToArray<TFrom, TTo>(List<TFrom> values, Converter<TFrom, TTo> converter) {
            TTo[] result = new TTo[values.Count];
            for (int index = 0; index < values.Count; ++index) {
                result[index] = converter(values[index]);
            }
            return result;
        }

        public static TTo[][] FromListOfListsToArrayArray<TFrom, TTo>(List<List<TFrom>> values, Converter<TFrom, TTo> converter) {
            TTo[][] result = new TTo[values.Count][];
            for (int index = 0; index < values.Count; ++index) {
                result[index] = FromListToArray<TFrom, TTo>(values[index], converter);
            }
            return result;
        }

        public static List<TTo> FromArrayToList<TFrom, TTo>(TFrom[] values, Converter<TFrom, TTo> converter) {
            List<TTo> result = new List<TTo>(values.Length);
            for (int index = 0; index < values.Length; ++index) {
                result.Add(converter(values[index]));
            }
            return result;
        }

        public static List<List<TTo>> FromArrayArrayToListOfLists<TFrom, TTo>(TFrom[][] values, Converter<TFrom, TTo> converter) {
            List<List<TTo>> result = new List<List<TTo>>(values.Length);
            for (int index = 0; index < values.Length; ++index) {
                result.Add(FromArrayToList<TFrom, TTo>(values[index], converter));
            }
            return result;
        }

        public class UnsupportedNetType : ExceptionBase {
            public UnsupportedNetType(string format, params object[] arguments) : base(format, arguments) { }
        }

        public class IllegalAPLDateTime : ExceptionBase {
            public IllegalAPLDateTime(string format, params object[] arguments) : base(format, arguments) { }
        }
    }
}
