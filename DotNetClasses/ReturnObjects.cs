using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetClasses {
    public class ReturnObjects {
        public static int[] ListOfInts(int no) {
            var list = new int[no];
            for (int i = 0; i < no; ++i) {
                list[i] = i;
            }

            return list;
        }
        public static DataItem[] ListOfStructs(int no) {
            var list = new DataItem[no];
            for (int i = 0; i < no; ++i) {
                list[i] = new DataItem() {
                    intItem = i,
                    stringItem = i.ToString(),
                    subItem = new DataSubItem() {
                        substringItem = "Sub" + i.ToString(),
                        subintList = new int[] { 1, 2, 3 }
                    }
                };
            }

            return list;
        }

        public static DataItemClass[] ListOfClasses(int no) {
            var list = new DataItemClass[no];
            for (int i = 0; i < no; ++i) {
                list[i] = new DataItemClass() {
                    intItem = i,
                    stringItem = i.ToString(),
                    subItem = new DataSubItem() {
                        substringItem = "Sub" + i.ToString(),
                        subintList = new int[] { 1, 2, 3 }
                    }
                };
            }

            return list;
        }


    }

    public struct DataSubItem {
        public string substringItem;
        public int[] subintList;
        public DataSubItem(string substringItem, int[] subintList) {
            this.substringItem = substringItem;
            this.subintList = subintList;
        }
    }

    public struct DataItem {
        public string stringItem;
        public int intItem;
        public DataSubItem subItem;

    }

    public class DataItemClass {
        public string stringItem;
        public int intItem;
        public DataSubItem subItem;
    }

}
