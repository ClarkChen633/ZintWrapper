using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ZintWrapper
{
    class ZintLib
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct zint_symbol
        {
            public int symbology;

            public int height;
            public int whitespace_width;
            public int border_width;

            public int output_options;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string fgcolour;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string bgcolour;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string outfile;

            public float scale;
            public int option_1;
            public int option_2;
            public int option_3;
            public int show_hrt;

            public int input_mode;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string text;

            public int rows;
            public int width;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string primary;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 25454)]
            public byte[] encoded_data;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I4, SizeConst = 178)]
            public int[] row_height;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string errtxt;

            public IntPtr bitmap;

            public int bitmap_width;
            public int bitmap_height;
            public IntPtr rendered;
        }

        [DllImport("zint.dll", EntryPoint = "ZBarcode_Create", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr Create();

        [DllImport("zint.dll", EntryPoint = "ZBarcode_Delete", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Delete(ref zint_symbol symbol);

        [DllImport("zint.dll", EntryPoint = "ZBarcode_Clear", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Clear(ref zint_symbol symbol);

        [DllImport("zint.dll", EntryPoint = "ZBarcode_Encode_and_Buffer", CallingConvention = CallingConvention.Cdecl)]
        public extern static int EncodeAndBuffer(
         ref zint_symbol symbol,
         string input,
         int length,
         int rotate_angle);

        [DllImport("zint.dll", EntryPoint = "ZBarcode_Encode_and_Print", CallingConvention = CallingConvention.Cdecl)]
        public extern static int EncodeAndPrint(
         ref zint_symbol symbol,
         string input,
         int length,
         int rotate_angle);

        public static void Render()
        {
            // call DLL function to generate pointer to initialized struct
            zint_symbol s = (zint_symbol)

            // generate managed counterpart of struct
            Marshal.PtrToStructure(ZintLib.Create(), typeof(zint_symbol));

            // change some settings
            s.symbology = 25;
            s.outfile = "baro.png";
            s.text = "12345";

            String str = "12345";

            // DLL function call to generate output file using changed settings -- DOES NOT WORK --
            System.Console.WriteLine(ZintLib.EncodeAndBuffer(ref s, str, str.Length, 0));

            // DLL function call to generate output file using changed settings -- WORKS --
            System.Console.WriteLine(ZintLib.EncodeAndPrint(ref s, str, str.Length, 0));

            // notice that these variables are set in ZBarcode_Create()?
            Console.WriteLine("bgcolor=" + s.bgcolour + ", fgcolor=" + s.fgcolour + ", outfile=" + s.outfile);

            // these variables maintain the same values as when they were written to in ZBarcode_Create().
            if (s.errtxt != null)
                Console.WriteLine("Error: " + s.errtxt);
            else
                Console.WriteLine("Image size rendered: " + s.bitmap_width + " x " + s.bitmap_height);
        }
    }
}
