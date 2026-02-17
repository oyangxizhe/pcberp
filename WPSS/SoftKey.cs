using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
//公共函数说明

//***查找加密锁 
//int FindPort(int start, ref string OutKeyPath);

//查找指定的加密锁(使用普通算法一)
//int FindPort_2(int start, int in_data, int verf_data, ref string OutKeyPath);

//***获到锁的版本
//int NT_GetIDVersion(ref short version,  string KeyPath);

//获到锁的扩展版本
//int NT_GetIDVersionEx(ref short version,  string KeyPath);

//***获到锁的ID
//int GetID(ref int id_1, ref int id_2, string KeyPath);

//***从加密锁中读取一批字节
//int YReadEx(byte[] OutData, short Address, short mylen, string HKey, string LKey, string KeyPath);

//***从加密锁中读取一个字节数据，一般不使用
//int YRead(ref byte OutData, short Address,string HKey, string LKey, string KeyPath);

//***写一批字节到加密锁中
//int YWriteEx(byte[] InData, short Address, short mylen, string HKey, string LKey, string KeyPath);

//***写一个字节的数据到加密锁中，一般不使用
//int YWrite(byte InData, short Address, string HKey, string LKey, string KeyPath);

//***从加密锁中读字符串
//int YReadString(ref string outstring, short Address, short mylen, string HKey, string LKey, string KeyPath);

//***写字符串到加密锁中
//int YWriteString(string InString, short Address, string HKey, string LKey, string KeyPath);

//***算法函数
//int sWriteEx(int in_data , ref int out_data , string KeyPath);
//int sWrite_2Ex(int in_data , ref int out_data ,string KeyPath);
//int sRead(ref int in_data, string KeyPath);
//int sWrite(int out_data, string KeyPath);
//int sWrite_2(int out_data, string KeyPath);

//***设置写密码
//int SetWritePassword(string W_HKey, string W_LKey, string new_HKey, string new_LKey, string KeyPath);

//***设置读密码
//int SetReadPassword(string W_HKey, string W_LKey, string new_HKey, string new_LKey, string KeyPath);

//'设置增强算法密钥一
//int SetCal_2(string Key , string KeyPath);

//使用增强算法一对字符串进行加密
//int EncString(string InString , ref string outstring , string KeyPath);

//使用增强算法一对二进制数据进行加密
// int Cal(byte[] InBuf, byte[] OutBuf, string KeyPath);

//'设置增强算法密钥二
//int SetCal_New(string Key , string KeyPath);

//使用增强算法二对字符串进行加密
//int EncString_New(string InString , ref string outstring , string KeyPath);

//使用增强算法二对二进制数据进行加密
// int Cal_New(byte[] InBuf, byte[] OutBuf, string KeyPath);

//***初始化加密锁函数
//int ReSet( string Path);

//***获取字符串长度
//int lstrlenA(string InString );
namespace WPSS
{
   public struct  GUID
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
       public byte [] Data1;
       [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
       public byte[] Data2;
       [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
       public byte[] Data3;
       [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
       public byte[] Data4;
    }

    public struct SP_INTERFACE_DEVICE_DATA
    {
        public int cbSize;
        public GUID InterfaceClassGuid;
        public int Flags;
        public IntPtr Reserved;
    }



    public struct SP_DEVINFO_DATA
    {
        public int cbSize;
        public GUID ClassGuid;
        public int DevInst;
        public IntPtr Reserved;
    }


    public struct SP_DEVICE_INTERFACE_DETAIL_DATA
    {
        public int cbSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public byte[] DevicePath;
    }


    public struct HIDD_ATTRIBUTES
    {
        public int Size;
        public ushort VendorID;
        public ushort ProductID;
        public ushort VersionNumber;
    }


    public struct HIDP_CAPS
    {
        public short Usage;
        public short UsagePage;
        public short InputReportByteLength;
        public short OutputReportByteLength;
        public short FeatureReportByteLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public short[] Reserved;
        public short NumberLinkCollectionNodes;
        public short NumberInputButtonCaps;
        public short NumberInputValueCaps;
        public short NumberInputDataIndices;
        public short NumberOutputButtonCaps;
        public short NumberOutputValueCaps;
        public short NumberOutputDataIndices;
        public short NumberFeatureButtonCaps;
        public short NumberFeatureValueCaps;
        public short NumberFeatureDataIndices;
    }
    public class SoftKey
    {
        private const ushort VID = 0x3689;
        private const ushort PID = 0x8762;
         private const ushort  PID_NEW= 0X2020;
        private const ushort VID_NEW = 0X3689;
        private const ushort PID_NEW_2 = 0X2020;
        private const ushort VID_NEW_2 = 0X2020;
        private const short DIGCF_PRESENT = 0x2;
        private const short DIGCF_DEVICEINTERFACE = 0x10;
        private const short INVALID_HANDLE_VALUE = (-1);
        private const short ERROR_NO_MORE_ITEMS = 259;

        private const uint GENERIC_READ = 0x80000000;
        private const int GENERIC_WRITE = 0x40000000;
        private const uint FILE_SHARE_READ = 0x1;
        private const uint FILE_SHARE_WRITE = 0x2;
        private const uint OPEN_EXISTING = 3;
        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        private const uint INFINITE = 0xFFFF;

        private const short MAX_LEN = 495;

        public const int FAILEDGENKEYPAIR = -21;
        public const int FAILENC = -22;
        public const int FAILDEC = -23;
        public const int FAILPINPWD = -24;
        public const int USBStatusFail = -50;  //USB操作失败，可能是没有找到相关的指令

        public const int SM2_ADDBYTE = 97;//加密后的数据会增加的长度
        public const int MAX_ENCLEN = 128; //最大的加密长度分组
        public const int MAX_DECLEN = (MAX_ENCLEN + SM2_ADDBYTE); //最大的解密长度分组
        public const int SM2_USENAME_LEN = 80;// '最大的用户名长度


        public const int ECC_MAXLEN = 32;
        public const int PIN_LEN = 16;

        private const byte GETVERSION = 0x01;
        private const byte GETID = 0x02;
        private const byte GETVEREX = 0x05;
        private const byte CAL_TEA = 0x08;
        private const byte SET_TEAKEY = 0x09;
        private const byte READBYTE = 0x10;
        private const byte WRITEBYTE = 0x11;
        private const byte YTREADBUF = 0x12;
        private const byte YTWRITEBUF = 0x13;
        private const byte MYRESET = 0x20;
        private const byte YTREBOOT = 0x24;
        private const byte SET_ECC_PARA = 0x30;
        private const byte GET_ECC_PARA = 0x31;
        private const byte SET_ECC_KEY = 0x32;
        private const byte GET_ECC_KEY = 0x33;
        private const byte MYENC = 0x34;
        private const byte MYDEC = 0X35;
        private const byte SET_PIN = 0X36;
        private const byte GEN_KEYPAIR = 0x37;
        private const byte YTSIGN = 0x51;
        private const byte YTVERIFY = 0x52;
        private const byte GET_CHIPID = 0x53;
        private const byte YTSIGN_2 = 0x53;

        [DllImport("kernel32.dll")]
        public static extern int lstrlenA(string InString);
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyStringToByte(byte[] pDest, string pSourceg, int ByteLenr);
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyByteToString(StringBuilder pDest, byte[] pSource, int ByteLenr);

        [DllImport("HID.dll")]
        private static extern bool HidD_GetAttributes(IntPtr HidDeviceObject, ref HIDD_ATTRIBUTES Attributes);

        [DllImport("HID.dll")]
        private static extern int HidD_GetHidGuid(ref GUID HidGuid);

        [DllImport("HID.dll")]
        private static extern bool HidD_GetPreparsedData(IntPtr HidDeviceObject, ref IntPtr PreparsedData);

        [DllImport("HID.dll")]
        private static extern int HidP_GetCaps(IntPtr PreparsedData, ref HIDP_CAPS Capabilities);

        [DllImport("HID.dll")]
        private static extern bool HidD_FreePreparsedData(IntPtr PreparsedData);

        [DllImport("HID.dll")]
        private static extern bool HidD_SetFeature(IntPtr HidDeviceObject, byte[] ReportBuffer, int ReportBufferLength);

        [DllImport("HID.dll")]
        private static extern bool HidD_GetFeature(IntPtr HidDeviceObject, byte[] ReportBuffer, int ReportBufferLength);

        [DllImport("SetupApi.dll")]
        private static extern IntPtr SetupDiGetClassDevsA(ref GUID ClassGuid, int Enumerator, int hwndParent, int Flags);

        [DllImport("SetupApi.dll")]
        private static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("SetupApi.dll")]
        private static extern bool SetupDiGetDeviceInterfaceDetailA(IntPtr DeviceInfoSet, ref  SP_INTERFACE_DEVICE_DATA DeviceInterfaceData, ref SP_DEVICE_INTERFACE_DETAIL_DATA DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, int DeviceInfoData);

        [DllImport("SetupApi.dll")]
        private static extern bool SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, int DeviceInfoData, ref GUID InterfaceClassGuid, int MemberIndex, ref SP_INTERFACE_DEVICE_DATA DeviceInterfaceData);

        [DllImport("kernel32.dll", EntryPoint = "CreateFileA")]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, uint hTemplateFile);

        [DllImport("kernel32.dll")]
        private static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        private static extern int GetLastError();


        [DllImport("kernel32.dll", EntryPoint = "CreateSemaphoreA")]
        private static extern IntPtr CreateSemaphore(int lpSemaphoreAttributes, int lInitialCount, int lMaximumCount, string lpName);

        [DllImport("kernel32.dll")]
        private static extern int WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        private static extern int ReleaseSemaphore(IntPtr hSemaphore, int lReleaseCount, int lpPreviousCount);

 

        //以下函数用于将字节数组转化为宽字符串
        private static string ByteConvertString(byte[] buffer)
        {
            char[] null_string ={ '\0', '\0' };
            System.Text.Encoding encoding = System.Text.Encoding.Default;
            return encoding.GetString(buffer).TrimEnd(null_string);
        }

        //以下用于将16进制字符串转化为无符号长整型
        private uint HexToInt(string s)
        {
            string[] hexch = { "0", "1", "2", "3", "4", "5", "6", "7",
								       "8", "9", "A", "B", "C", "D", "E", "F"};
            s = s.ToUpper();
            int i, j;
            int r, n, k;
            string ch;

            k = 1; r = 0;
            for (i = s.Length; i > 0; i--)
            {
                ch = s.Substring(i - 1, 1);
                n = 0;
                for (j = 0; j < 16; j++)
                    if (ch == hexch[j])
                        n = j;
                r += (n * k);
                k *= 16;
            }
            return unchecked((uint)r);
        }

        private int HexStringToByteArray(string InString, ref byte [] b)
        {
            int nlen;
            int retutn_len;
            int n,i;
            string temp;
            nlen = InString.Length;
            if (nlen < 16) retutn_len = 16;
            retutn_len = nlen / 2;
            b = new byte[retutn_len];
            i = 0;
            for(n=0;n<nlen;n=n+2)
            {
                temp = InString.Substring( n, 2);
                b[i] =(byte) HexToInt(temp);
                i = i + 1;
             }
             return retutn_len;
        }


        public void EnCode(byte[] inb, byte[] outb, string Key)
        {

            UInt32 cnDelta, y, z, a, b, c, d, temp_2;
            UInt32[] buf = new UInt32[16];
            int n, i, nlen;
            UInt32 sum;
            //UInt32 temp, temp_1;
            string temp_string;


            cnDelta = 2654435769;
            sum = 0;

            nlen = Key.Length;
            i = 0;
            for (n = 1; n <= nlen; n = n + 2)
            {
                temp_string = Key.Substring(n - 1, 2);
                buf[i] = HexToInt(temp_string);
                i = i + 1;
            }
            a = 0; b = 0; c = 0; d = 0;
            for (n = 0; n <= 3; n++)
            {
                a = (buf[n] << (n * 8)) | a;
                b = (buf[n + 4] << (n * 8)) | b;
                c = (buf[n + 4 + 4] << (n * 8)) | c;
                d = (buf[n + 4 + 4 + 4] << (n * 8)) | d;
            }



            y = 0;
            z = 0;
            for (n = 0; n <= 3; n++)
            {
                temp_2 = inb[n];
                y = (temp_2 << (n * 8)) | y;
                temp_2 = inb[n + 4];
                z = (temp_2 << (n * 8)) | z;
            }


            n = 32;

            while (n > 0)
            {
                sum = cnDelta + sum;

                /*temp = (z << 4) & 0xFFFFFFFF;
                temp = (temp + a) & 0xFFFFFFFF;
                temp_1 = (z + sum) & 0xFFFFFFFF;
                temp = (temp ^ temp_1) & 0xFFFFFFFF;
                temp_1 = (z >> 5) & 0xFFFFFFFF;
                temp_1 = (temp_1 + b) & 0xFFFFFFFF;
                temp = (temp ^ temp_1) & 0xFFFFFFFF;
                temp = (temp + y) & 0xFFFFFFFF;
                y = temp & 0xFFFFFFFF;*/
                y += ((z << 4) + a) ^ (z + sum) ^ ((z >> 5) + b);

                /*temp = (y << 4) & 0xFFFFFFFF;
                temp = (temp + c) & 0xFFFFFFFF;
                temp_1 = (y + sum) & 0xFFFFFFFF;
                temp = (temp ^ temp_1) & 0xFFFFFFFF;
                temp_1 = (y >> 5) & 0xFFFFFFFF;
                temp_1 = (temp_1 + d) & 0xFFFFFFFF;
                temp = (temp ^ temp_1) & 0xFFFFFFFF;
                temp = (z + temp) & 0xFFFFFFFF;
                z = temp & 0xFFFFFFFF;*/
                z += ((y << 4) + c) ^ (y + sum) ^ ((y >> 5) + d);
                n = n - 1;

            }

            for (n = 0; n <= 3; n++)
            {
                outb[n] = System.Convert.ToByte((y >> (n * 8)) & 255);
                outb[n + 4] = System.Convert.ToByte((z >> (n * 8)) & 255);
            }

        }

        public void DeCode(byte[] inb, byte[] outb, string Key)
        {

            UInt32 cnDelta, y, z, a, b, c, d, temp_2;
            UInt32[] buf = new UInt32[16];
            int n, i, nlen;
            UInt32 sum;
            //UInt32 temp, temp_1;
            string temp_string;


            cnDelta = 2654435769;
            sum = 0xC6EF3720;

            nlen = Key.Length;
            i = 0;
            for (n = 1; n <= nlen; n = n + 2)
            {
                temp_string = Key.Substring(n - 1, 2);
                buf[i] = HexToInt(temp_string);
                i = i + 1;
            }
            a = 0; b = 0; c = 0; d = 0;
            for (n = 0; n <= 3; n++)
            {
                a = (buf[n] << (n * 8)) | a;
                b = (buf[n + 4] << (n * 8)) | b;
                c = (buf[n + 4 + 4] << (n * 8)) | c;
                d = (buf[n + 4 + 4 + 4] << (n * 8)) | d;
            }



            y = 0;
            z = 0;
            for (n = 0; n <= 3; n++)
            {
                temp_2 = inb[n];
                y = (temp_2 << (n * 8)) | y;
                temp_2 = inb[n + 4];
                z = (temp_2 << (n * 8)) | z;
            }


            n = 32;

            while (n-- > 0)
            {
                z -= ((y << 4) + c) ^ (y + sum) ^ ((y >> 5) + d);
                y -= ((z << 4) + a) ^ (z + sum) ^ ((z >> 5) + b);
                sum -= cnDelta;

            }

            for (n = 0; n <= 3; n++)
            {
                outb[n] = System.Convert.ToByte((y >> (n * 8)) & 255);
                outb[n + 4] = System.Convert.ToByte((z >> (n * 8)) & 255);
            }

        }


        public string StrEnc(string InString, string Key)//使用增强算法，加密字符串
        {

            byte[] b, outb;
            byte[] temp = new byte[8], outtemp = new byte[8];
            int n, i, nlen, outlen;
            string outstring;


            nlen = lstrlenA(InString) + 1;
            if (nlen < 8)
                outlen = 8;
            else
                outlen = nlen;
            b = new byte[outlen];
            outb = new byte[outlen];

            CopyStringToByte(b, InString, nlen);

            b.CopyTo(outb, 0);

            for (n = 0; n <= outlen - 8; n = n + 8)
            {
                for (i = 0; i < 8; i++) temp[i] = b[i + n];
                EnCode(temp, outtemp, Key);
                for (i = 0; i < 8; i++) outb[i + n] = outtemp[i];
            }

            outstring = "";
            for (n = 0; n <= outlen - 1; n++)
            {
                outstring = outstring + outb[n].ToString("X2");
            }
            return outstring;
        }
        public string StrDec(string InString, string Key) //使用增强算法，加密字符串
        {
            byte[] b, outb;
            byte[] temp = new byte[8], outtemp = new byte[8];
            int n, i, nlen, outlen;
            string temp_string;
            StringBuilder c_str;


            nlen = InString.Length;
            if (nlen < 16) outlen = 16;
            outlen = nlen / 2;
            b = new byte[outlen];
            outb = new byte[outlen];

            i = 0;
            for (n = 1; n <= nlen; n = n + 2)
            {
                temp_string = InString.Substring(n - 1, 2);
                b[i] = System.Convert.ToByte(HexToInt(temp_string));
                i = i + 1;
            }

            b.CopyTo(outb, 0);

            for (n = 0; n <= outlen - 8; n = n + 8)
            {
                for (i = 0; i < 8; i++) temp[i] = b[i + n];
                DeCode(temp, outtemp, Key);
                for (i = 0; i < 8; i++) outb[i + n] = outtemp[i];
            }

            c_str = new StringBuilder("", outlen);
            CopyByteToString(c_str, outb, outlen);
            return c_str.ToString();

        }
        
         private bool isfindmydevice(int pos, ref int count, ref string OutPath)
        {

               return  Subisfindmydevice(pos,ref count,ref OutPath);

        }



        private bool Subisfindmydevice(int pos, ref int count, ref string OutPath)
        {
            IntPtr hardwareDeviceInfo;
            SP_INTERFACE_DEVICE_DATA DeviceInfoData= new SP_INTERFACE_DEVICE_DATA();
            int i;
            GUID HidGuid=new GUID();
            SP_DEVICE_INTERFACE_DETAIL_DATA functionClassDeviceData= new SP_DEVICE_INTERFACE_DETAIL_DATA();
            int requiredLength;
            IntPtr d_handle;
            HIDD_ATTRIBUTES Attributes= new HIDD_ATTRIBUTES();

            i = 0; count = 0;
            HidD_GetHidGuid(ref HidGuid);

            hardwareDeviceInfo = SetupDiGetClassDevsA(ref HidGuid, 0, 0, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

            if (hardwareDeviceInfo == (IntPtr)INVALID_HANDLE_VALUE)return false;

            DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);

            while (SetupDiEnumDeviceInterfaces(hardwareDeviceInfo, 0, ref HidGuid, i, ref DeviceInfoData))
            {
                if (GetLastError() == ERROR_NO_MORE_ITEMS )break;
                if (System.IntPtr.Size == 4)
                    functionClassDeviceData.cbSize = Marshal.SizeOf(functionClassDeviceData) - 255;// 5;
                else
                    functionClassDeviceData.cbSize = 8;
                requiredLength = 0;
                if (!SetupDiGetDeviceInterfaceDetailA(hardwareDeviceInfo, ref DeviceInfoData, ref functionClassDeviceData, 300, ref requiredLength, 0) )
                {
                    SetupDiDestroyDeviceInfoList(hardwareDeviceInfo);
                    return false;
                }
                OutPath = ByteConvertString(functionClassDeviceData.DevicePath);
                d_handle = CreateFile(OutPath,  GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
                if ((IntPtr)INVALID_HANDLE_VALUE != d_handle )
                {
                    if (HidD_GetAttributes(d_handle, ref Attributes))
                    {
                        if ((Attributes.ProductID == PID) && (Attributes.VendorID == VID) ||
                            (Attributes.ProductID == PID_NEW) && (Attributes.VendorID == VID_NEW) ||
                            (Attributes.ProductID == PID_NEW_2) && (Attributes.VendorID == VID_NEW_2))
                        {
                            if (pos == count)
                            {
                                SetupDiDestroyDeviceInfoList(hardwareDeviceInfo);
                                CloseHandle(d_handle);
                                return true;
                            }
                            count = count + 1;
                        }
                    }
                    CloseHandle(d_handle);
                }
                i = i + 1;
                
            }
            SetupDiDestroyDeviceInfoList(hardwareDeviceInfo);
            return false;
        }

        private bool GetFeature(IntPtr hDevice, byte[] array_out, int out_len)
    {

        bool FeatureStatus;
        bool Status;
        int i;
        byte []FeatureReportBuffer=new byte[512];
        IntPtr Ppd =System.IntPtr.Zero;
        HIDP_CAPS Caps=new HIDP_CAPS();

        if(!HidD_GetPreparsedData(hDevice, ref Ppd))return false;

        if(HidP_GetCaps(Ppd, ref Caps)<=0 )
        {
            HidD_FreePreparsedData(Ppd);
            return false;
         }

        Status = true;

        FeatureReportBuffer[0] = 1;

        FeatureStatus = HidD_GetFeature(hDevice, FeatureReportBuffer, Caps.FeatureReportByteLength);
        if( FeatureStatus)
        {
            for(i=0;i<out_len;i++)
            {
                 array_out[i] = FeatureReportBuffer[i];
            }
        }


        Status = Status && FeatureStatus;
        HidD_FreePreparsedData(Ppd);

        return  Status;
           
        }

        private bool SetFeature(IntPtr hDevice, byte[] array_in, int in_len) 
    {
            bool FeatureStatus;
            bool Status;
            int i;
            byte []FeatureReportBuffer=new byte[512];
            IntPtr Ppd = System.IntPtr.Zero;
            HIDP_CAPS Caps=new HIDP_CAPS();

            if(!HidD_GetPreparsedData(hDevice, ref Ppd))return false;

            if(HidP_GetCaps(Ppd, ref Caps)<=0 )
            {
                HidD_FreePreparsedData(Ppd);
                return false;
             }

            Status = true;

            FeatureReportBuffer[0] = 2;

            for(i=0;i<in_len;i++)
            {
                FeatureReportBuffer[i + 1] = array_in[i + 1];

            }
            FeatureStatus = HidD_SetFeature(hDevice, FeatureReportBuffer, Caps.FeatureReportByteLength);


            Status = Status && FeatureStatus;
            HidD_FreePreparsedData(Ppd);

            return  Status;

    }

        private int NT_FindPort(int start, ref string OutPath)
        {
            int count=0;
            if (!isfindmydevice(start,ref count, ref OutPath))
            {
                return -92;
            }
            return 0;
        }

    private int NT_FindPort_2(int start, int in_data, int verf_data, ref string OutPath )
{
        int count=0;
        int pos;
        int out_data=0;
        int ret;
        for(pos=start;pos<256;pos++)
        {
            if (!isfindmydevice(pos, ref count,ref OutPath) )return  -92 ;
            ret = WriteDword(in_data, OutPath);
            if (ret != 0) continue;
            ret = ReadDword(ref out_data, OutPath);
            if (ret != 0) continue;
            if (out_data == verf_data ){ return 0;}
        }
        return (-92);
    }
        private int OpenMydivece(ref IntPtr hUsbDevice, string Path)
    {
        string OutPath;
        bool biao ;
        int count=0;
        if (Path.Length < 1)
        {
            OutPath = "";
            biao = isfindmydevice(0, ref count, ref OutPath);
            if (!biao )return -92;
            hUsbDevice = CreateFile(OutPath, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);
            if (hUsbDevice == (IntPtr)INVALID_HANDLE_VALUE ) return -92;
        }
        else
        {
            hUsbDevice = CreateFile(Path, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);
            if (hUsbDevice == (IntPtr)INVALID_HANDLE_VALUE ) return -92;
        }
        return 0;
    }

    private int NT_Read(ref byte ele1, ref byte ele2, ref byte ele3, ref byte ele4, string Path)
    {
        byte []array_out=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        if( OpenMydivece(ref hUsbDevice, Path) != 0 ) return -92;
        if (!GetFeature(hUsbDevice, array_out, 5) ){ CloseHandle(hUsbDevice) ; return -93;}
        CloseHandle(hUsbDevice);
        ele1 = array_out[0];
        ele2 = array_out[1];
        ele3 = array_out[2];
        ele4 = array_out[3];
        return 0;
    }

    private int NT_Write(byte ele1, byte ele2, byte ele3, byte ele4, string Path)
    {
        byte []array_in=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        if (OpenMydivece(ref hUsbDevice, Path) != 0){ return -92;}
        array_in[1] = 3 ; array_in[2] = ele1 ; array_in[3] = ele2 ; array_in[4] = ele3 ; array_in[5] = ele4;
        if (!SetFeature(hUsbDevice, array_in, 5) ) {CloseHandle(hUsbDevice) ; return -93;}
        CloseHandle(hUsbDevice);
        return 0;
    }

    private int NT_Write_2(byte ele1, byte ele2, byte ele3, byte ele4, string Path)
    {
        byte []array_in=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        if( OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 4 ; array_in[2] = ele1 ; array_in[3] = ele2 ; array_in[4] = ele3 ; array_in[5] = ele4;
        if (!SetFeature(hUsbDevice, array_in, 5) ){ CloseHandle(hUsbDevice) ; return -93;}
        CloseHandle(hUsbDevice);
        return 0;
    }
    private int GetIDVersion(ref short Version , string Path)
    {
        byte []array_in=new byte[25];
        byte []array_out=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 1;
        if (!SetFeature(hUsbDevice, array_in, 1) ){CloseHandle(hUsbDevice) ; return -93;}
        if (!GetFeature(hUsbDevice, array_out, 1)) {CloseHandle(hUsbDevice) ; return -93;}
        CloseHandle(hUsbDevice);
        Version = array_out[0];
        return 0;
    }

    private int NT_GetID(ref int ID_1, ref int ID_2 , string Path)
    {
        int [] t=new int[8];
        byte []array_in=new byte[25];
        byte []array_out=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        if( OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 2;
        if (!SetFeature(hUsbDevice, array_in, 1)) {CloseHandle(hUsbDevice) ; return -93;}
        if (!GetFeature(hUsbDevice, array_out, 8)){ CloseHandle(hUsbDevice) ; return -93;}
        CloseHandle(hUsbDevice);
        t[0] = array_out[0] ; t[1] = array_out[1] ; t[2] = array_out[2] ; t[3] = array_out[3];
        t[4] = array_out[4] ; t[5] = array_out[5] ; t[6] = array_out[6] ; t[7] = array_out[7];
        ID_1 = t[3] | (t[2] << 8) | (t[1] << 16) | (t[0] << 24);
        ID_2 = t[7] | (t[6] << 8) | (t[5] << 16) | (t[4] << 24);
        return 0;
    }


    private int Y_Read(byte [] OutData, int address , int nlen , byte [] password ,string Path , int pos)
    {
        int addr_l;
        int addr_h ;
        int n;
        byte []array_in=new byte[512];
        byte []array_out=new byte[512];
        if ((address > MAX_LEN) || (address < 0)) return -81;
        if ((nlen > 255)) return -87;
        if ((nlen + address) > MAX_LEN) return -88;
        addr_h = (address >> 8) * 2;
        addr_l = address & 255;
        IntPtr hUsbDevice=IntPtr.Zero;
        if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;

        array_in[1] = 0x12;
        array_in[2] = (byte)addr_h;
        array_in[3] = (byte)addr_l;
        array_in[4] = (byte)nlen;
        for(n=0;n<=7;n++)
        {
            array_in[5 + n] = password[n];
        }
        if (!SetFeature(hUsbDevice, array_in, 13)){ CloseHandle(hUsbDevice) ; return -93;}
        if (!GetFeature(hUsbDevice, array_out, nlen + 1) ){ CloseHandle(hUsbDevice) ; return -94;}
        CloseHandle(hUsbDevice);
        if (array_out[0] != 0)
        {
            return -83;
        }
        for(n=0;n<nlen;n++)
        {
            OutData[n + pos] = array_out[n + 1];
        }
        return 0;
    }

    private int Y_Write(byte [] indata, int address, int nlen, byte [] password, string Path, int pos)
    {
        int addr_l;
        int addr_h ;
        int n;
        byte []array_in=new byte[512];
        byte []array_out=new byte[512];
        if ((nlen > 255)) return -87;
        if ((address + nlen - 1) > (MAX_LEN + 17) || (address < 0)) return -81;
        addr_h = (address >> 8) * 2;
        addr_l = address & 255;
        IntPtr hUsbDevice=IntPtr.Zero;
        if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 0x13;
        array_in[2] = (byte)addr_h;
        array_in[3] = (byte)addr_l;
        array_in[4] = (byte)nlen;
        for(n=0;n<=7;n++)
        {
            array_in[5 + n] = password[n];
        }
        for(n=0;n<nlen;n++)
        {
            array_in[13 + n] = indata[n + pos];
        }
        if (!SetFeature(hUsbDevice, array_in, 13 + nlen)){ CloseHandle(hUsbDevice) ; return -93;}
        if (!GetFeature(hUsbDevice, array_out, 2)){ CloseHandle(hUsbDevice) ; return -94;}
        CloseHandle(hUsbDevice);
        if (array_out[0] != 0)
        {
            return -82;
        }
        return 0;
    }

    private int NT_Cal(byte [] InBuf , byte [] outbuf, string Path, int pos)
    {
        int n;
        byte []array_in=new byte[25];
        byte []array_out=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 8;
        for(n=2;n<=9;n++)
        {
            array_in[n] = InBuf[n - 2 + pos];
        }
        if (!SetFeature(hUsbDevice, array_in, 9)) {CloseHandle(hUsbDevice) ; return -93;}
        if (!GetFeature(hUsbDevice, array_out, 9)){ CloseHandle(hUsbDevice) ; return -93;}
        CloseHandle(hUsbDevice);
        for(n=0;n <8;n++)
        {
            outbuf[n + pos] = array_out[n];
        }
        if( array_out[8] != 0x55)
        {
            return -20;
        }
        return 0;
    }

    private int NT_SetCal_2(byte [] indata, byte IsHi, string Path, short pos)
    {

        int n;
        byte []array_in=new byte[25];
        byte []array_out=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 9;
        array_in[2] = IsHi;
        for(n=0;n <8;n++)
        {
            array_in[3 + n] = indata[n + pos];
        }
        if (!SetFeature(hUsbDevice, array_in, 11)){ CloseHandle(hUsbDevice) ; return -93;}
        if (!GetFeature(hUsbDevice, array_out, 2) ){ CloseHandle(hUsbDevice) ; return -94;}
        CloseHandle(hUsbDevice);
        if (array_out[0] != 0)
        {
            return -82;
        }

        return 0;
    }

    private int ReadDword(ref int out_data, string Path)
    {
        byte b1=0;
        byte b2=0;
        byte b3=0;
        byte b4=0;
        int t1;
        int t2;
        int t3;
        int t4;
        int ret;
        ret = NT_Read(ref b1, ref b2,ref  b3, ref b4, Path);
        t1 = b1 ; t2 = b2 ; t3 = b3 ; t4 = b4;
        out_data = t1 | (t2 << 8) | (t3 << 16) | (t4 << 24);
        return ret;
    }

    private int WriteDword(int in_data, string Path)
    {
        byte b1;
        byte b2;
        byte b3;
        byte b4 ;
        b1 = (byte)( in_data & 255);
        b2 = (byte)((in_data >> 8) & 255);
        b3 = (byte)((in_data >> 16) & 255);
        b4 = (byte)((in_data >> 24) & 255);
        return NT_Write(b1, b2, b3, b4, Path);
    }

    private int WriteDword_2(int in_data , string Path)
    {
        byte b1;
        byte b2;
        byte b3;
        byte b4;
        b1 = (byte)(in_data & 255);
        b2 = (byte)((in_data >> 8) & 255);
        b3 = (byte)((in_data >> 16) & 255);
        b4 = (byte)((in_data >> 24) & 255);
        return NT_Write_2(b1, b2, b3, b4, Path);
    }
    public int NT_GetIDVersion(ref short Version, string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = GetIDVersion(ref Version, Path);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int GetID(ref int ID_1, ref int ID_2 , string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = NT_GetID(ref ID_1, ref ID_2, Path);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int sRead(ref int in_data , string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = ReadDword(ref in_data, Path);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int sWrite(int out_data, string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = WriteDword(out_data, Path);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int YWriteEx(byte [] indata , int address, int nlen , string HKey , string LKey , string Path)
    {
        int ret=0;
        IntPtr hsignal;
        byte [] password=new byte[8];
        int n, trashLen = 0;
        int leave;
        int temp_leave ;
        if ((address + nlen - 1 > MAX_LEN) || (address < 0))return -81;

        ret = GetTrashBufLen(Path, ref trashLen);
        if (trashLen < 100) trashLen = 16;
        trashLen = trashLen - 8;
        if (ret != 0) return ret;

        myconvert(HKey, LKey, password);
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        temp_leave = address % trashLen;
        leave = trashLen - temp_leave;
        if (leave > nlen) leave = nlen;
        if (leave > 0)
        {
            for(n=0;n<leave / trashLen;n++)
            {
                ret = Y_Write(indata, address + n * trashLen, trashLen, password, Path, trashLen * n);
                if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YWriteEx(indata, address, nlen,HKey, LKey, Path); }
            }
            if (leave - trashLen * n > 0)
            {
                ret = Y_Write(indata, address + n * trashLen, leave - n * trashLen, password, Path, trashLen * n);
                if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YWriteEx(indata, address, nlen, HKey, LKey, Path); }
            }
        }
        nlen = nlen - leave ; address = address + leave;
        if (nlen > 0)
        {

            for(n=0;n<nlen/trashLen;n++)
            {
                ret = Y_Write(indata, address + n * trashLen, trashLen, password, Path, leave + trashLen * n);
                if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YWriteEx(indata, address, nlen, HKey, LKey, Path); }
            }
            if (nlen - trashLen * n > 0)
            {
                ret = Y_Write(indata, address + n * trashLen, nlen - n * trashLen, password, Path, leave + trashLen * n);
                if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YWriteEx(indata, address, nlen, HKey, LKey, Path); }
            }
        }
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int YReadEx(byte [] OutData, short address , short nlen , string HKey, string LKey , string Path)
    {
        int ret=0;
        IntPtr hsignal;
        byte [] password=new byte[8];
        int n, trashLen = 0;

        if ((address + nlen - 1 > MAX_LEN) || (address < 0))return (-81);

        ret = GetTrashBufLen(Path, ref trashLen);
        if (trashLen < 100) trashLen = 16;
        if (ret != 0) return ret;


        myconvert(HKey, LKey, password);
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        for(n=0;n<nlen/trashLen;n++)
        {
            ret = Y_Read(OutData, address + n * trashLen, trashLen, password, Path, n * trashLen);
            if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YReadEx(OutData, address, nlen, HKey, LKey, Path); }
        }
        if (nlen - trashLen * n > 0)
        {
            ret = Y_Read(OutData, address + n * trashLen, nlen - trashLen * n, password, Path, trashLen * n);
            if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YReadEx(OutData, address, nlen, HKey, LKey, Path); }
        }
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int FindPort_2(int start, int in_data , int verf_data , ref string OutPath )
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = NT_FindPort_2(start, in_data, verf_data, ref OutPath);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int FindPort(int start, ref string OutPath)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = NT_FindPort(start,ref  OutPath);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }


    public int sWrite_2(int out_data , string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = WriteDword_2(out_data, Path);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }
    private string AddZero(string InKey)
    {
        int nlen;
        int n;
        nlen =InKey.Length;
        for(n=nlen;n<=7;n++)
        {
            InKey = "0" + InKey;
        }
        return  InKey;
    }

    private void myconvert(string HKey, string LKey, byte [] out_data)
    {
        HKey = AddZero(HKey);
        LKey = AddZero(LKey);
        int n;
        for(n=0;n<=3;n++)
        {
            out_data[n] = (byte)HexToInt(HKey.Substring(  n * 2, 2));
        }
        for(n=0;n<=3;n++)
        {
            out_data[n + 4] = (byte)HexToInt(LKey.Substring( n * 2, 2));
        }
    }
    public int YRead(ref byte indata, int address, string HKey, string LKey, string Path)
    {
        int ret;
        IntPtr hsignal;
        byte []ary1=new byte[8];

        if ((address > 495) || (address < 0))return  -81;
        myconvert(HKey, LKey, ary1);
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = sub_YRead(ref indata, address, ary1, Path);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }
    private int sub_YRead(ref byte OutData , int address, byte [] password, string Path)
    {
        int n;
        byte []array_in=new byte[25];
        byte []array_out=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        byte opcode;
        if ((address > 495) || (address < 0))return -81;
        opcode = 128;
        if (address > 255)
        {
            opcode = 160;
            address = address - 256;
        }

        if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 16;
        array_in[2] = opcode;
        array_in[3] = (byte)address;
        array_in[4] = (byte)address;
        for(n=0;n<8;n++)
        {
            array_in[5 + n] = password[n];
        }
        if (!SetFeature(hUsbDevice, array_in, 13))
        {
            CloseHandle(hUsbDevice) ; return -93;
        }
        if (!GetFeature(hUsbDevice, array_out, 2))
        {
            CloseHandle(hUsbDevice) ; return -94;
        }
        CloseHandle(hUsbDevice);
        if (array_out[0] != 83)
        {
            return -83;
        }
        OutData = array_out[1];
        return 0;
    }

    public int YWrite(byte indata, int address, string HKey, string LKey, string Path)
    {
        int ret;
        IntPtr hsignal;
        byte [] ary1=new byte[8];

        if ((address > 495) || (address < 0)) return  -81;
        myconvert(HKey, LKey, ary1);
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = sub_YWrite(indata, address, ary1, Path);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }
    private int sub_YWrite(byte indata, int address, byte [] password, string Path)
    {
        int n;
        byte []array_in=new byte[25];
        byte []array_out=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        byte opcode ;
        if ((address > 511) || (address < 0)) return -81;
        opcode = 64;
        if (address > 255)
        {
            opcode = 96;
            address = address - 256;
        }

        if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 17;
        array_in[2] = opcode;
        array_in[3] = (byte)address;
        array_in[4] = indata;
        for(n=0;n<8;n++)
        {
            array_in[5 + n] = password[n];
        }
        if (!SetFeature(hUsbDevice, array_in, 13) )
        {
            CloseHandle(hUsbDevice) ; return -93;
        }
        if (!GetFeature(hUsbDevice, array_out, 2))
        {
            CloseHandle(hUsbDevice) ; return -94;
        }
        CloseHandle(hUsbDevice);
        if (array_out[1] != 1)
        {
            return -82;
        }
        return 0;
    }
    public int SetReadPassword(string W_HKey , string W_LKey, string new_HKey, string new_LKey, string Path)
    {
        int ret;
        IntPtr hsignal;
        byte [] ary1=new byte[8];
        byte [] ary2=new byte[8];
        short address;
        myconvert(W_HKey, W_LKey, ary1);
        myconvert(new_HKey, new_LKey, ary2);
        address = 496;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = Y_Write(ary2, address, 8, ary1, Path, 0);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }


    public int SetWritePassword(string W_HKey, string W_LKey, string new_HKey, string new_LKey, string Path)
    {
        int ret;
        IntPtr hsignal;
        byte [] ary1=new byte[8];
        byte [] ary2=new byte[8];
        short address;
        myconvert(W_HKey, W_LKey, ary1);
        myconvert(new_HKey, new_LKey, ary2);
        address = 504;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = Y_Write(ary2, address, 8, ary1, Path, 0);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int YWriteString(string InString, int address, string HKey, string LKey, string Path)
    {
        int ret=0;
        byte [] ary1=new byte[8];
        IntPtr hsignal;
        int n, trashLen = 0;
        int outlen ;
        int total_len;
        int temp_leave ;
        int leave;
        byte [] b;
        if ((address < 0)) return -81;

        ret = GetTrashBufLen(Path, ref trashLen);
        if (trashLen < 100) trashLen = 16;
        trashLen = trashLen - 8;
        if (ret != 0) return ret;

        myconvert(HKey, LKey, ary1);

        outlen = lstrlenA(InString); //注意，这里不写入结束字符串，与原来的兼容，也可以写入结束字符串，与原来的不兼容，写入长度会增加1
        b=new byte[outlen];
        CopyStringToByte(b, InString, outlen);

        total_len = address + outlen;
        if (total_len > MAX_LEN)return -47;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        temp_leave = address % trashLen;
        leave = trashLen - temp_leave;
        if (leave > outlen)leave = outlen;

        if (leave > 0)
        {
            for(n=0;n<(leave / trashLen);n++)
            {
                ret = Y_Write(b, address + n * trashLen, trashLen, ary1, Path, n * trashLen);
                if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YWrite_new((InString), address, HKey, LKey, Path); }
            }
            if (leave - trashLen * n > 0)
            {
                ret = Y_Write(b, address + n * trashLen, leave - n * trashLen, ary1, Path, trashLen * n);
                if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YWrite_new((InString), address, HKey, LKey, Path); }
            }
        }
        outlen = outlen - leave;
        address = address + leave;
        if (outlen > 0)
        {
            for(n=0;n<(outlen / trashLen);n++)
            {
                ret = Y_Write(b, address + n * trashLen, trashLen, ary1, Path, leave + n * trashLen);
                if (ret != 0){ ReleaseSemaphore(hsignal, 1, 0) ; CloseHandle(hsignal) ; return ret;}
            }
            if (outlen - trashLen * n > 0)
            {
                ret = Y_Write(b, address + n * trashLen, outlen - n * trashLen, ary1, Path, leave + trashLen * n);
                if (ret != 0){ ReleaseSemaphore(hsignal, 1, 0) ; CloseHandle(hsignal) ; return ret;}
            }
        }
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int YReadString(ref string  OutString, int address, int nlen, string HKey, string LKey,  string Path)
    {
        int ret=0;
        byte [] ary1=new byte[8];
        IntPtr hsignal;
        int n, trashLen = 0;
        int total_len ;
        byte [] outb;
        StringBuilder temp_OutString ;
        outb=new byte[nlen];
        myconvert(HKey, LKey, ary1);
        if (address < 0) return -81;

        ret = GetTrashBufLen(Path, ref trashLen);
        if (trashLen < 100) trashLen = 16;
        if (ret != 0) return ret;

        total_len = address + nlen;
        if (total_len > MAX_LEN) return -47;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        for(n=0;n<(nlen /trashLen);n++)
        {
            ret = Y_Read(outb, address + n * trashLen, trashLen, ary1, Path, n * trashLen);
            if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YRead_new(ref OutString, address, nlen, HKey, LKey, Path); }
        }
        if (nlen - trashLen * n > 0)
        {
            ret = Y_Read(outb, address + n * trashLen, nlen - trashLen * n, ary1, Path, trashLen * n);
            if (ret != 0) { ReleaseSemaphore(hsignal, 1, 0); CloseHandle(hsignal); return sub_YRead_new(ref OutString, address, nlen, HKey, LKey, Path); }
        }
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        temp_OutString = new StringBuilder("", nlen);
        //初始化数据为0，注意，这步一定是需要的
        for (n = 0; n < nlen; n++)
        {
            temp_OutString.Append(0);
        }
        CopyByteToString(temp_OutString, outb, nlen);
        OutString = temp_OutString.ToString();
        return ret;
    }

    public int SetCal_2(string Key, string Path)
    {
        int ret;
        IntPtr hsignal;
        byte [] KeyBuf=new byte[16];
        byte [] inb=new byte[8];
        HexStringToByteArray(Key, ref KeyBuf);
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = NT_SetCal_2(KeyBuf, 0, Path, 8);
        if (ret != 0) goto error1;
        ret = NT_SetCal_2(KeyBuf, 1, Path, 0);
    error1:
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int Cal(byte [] InBuf, byte [] outbuf, string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = NT_Cal(InBuf, outbuf, Path, 0);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int EncString(string InString, ref string OutString, string Path)
    {
        IntPtr hsignal;
        byte[] b;
        byte[] outb;
        int n;
        int nlen, temp_len;
        int ret = 0;

        nlen = lstrlenA(InString) + 1;
        temp_len = nlen;
        if (nlen < 8)
        {
            nlen = 8;
        }


        b = new byte[nlen];
        outb = new byte[nlen];

        CopyStringToByte(b, InString, temp_len);

        b.CopyTo(outb, 0);

        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        for(n=0;n<=(nlen-8);n=n+8)
        {
            ret = NT_Cal(b, outb, Path, n);
            if (ret != 0) break;
        }
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        OutString = "";
        for(n=0;n<nlen;n++)
        {
            OutString = OutString + outb[n].ToString("X2");
        }
        return ret;

    }
    public int sWriteEx(int in_data , ref int out_data , string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = WriteDword(in_data, Path);
        if (ret != 0) goto error1;
        ret = ReadDword(ref out_data, Path);
error1:
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int sWrite_2Ex(int in_data , ref int out_data, string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = WriteDword_2(in_data, Path);
        if (ret != 0) goto error1;
        ret = ReadDword(ref out_data, Path);
error1:
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }
    public int ReSet( string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = NT_ReSet(Path);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }
    private int NT_ReSet( string Path)
    {
        byte []array_in=new byte[25];
        byte []array_out=new byte[25];
        IntPtr hUsbDevice=IntPtr.Zero;
        if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
        array_in[1] = 32;
        if (!SetFeature(hUsbDevice, array_in, 2))
        {
            CloseHandle(hUsbDevice) ; return -93;
        }
        if (!GetFeature(hUsbDevice, array_out, 2))
        {
            CloseHandle(hUsbDevice) ; return -93;
        }
        CloseHandle(hUsbDevice);
        if (array_out[0] != 0)
        {
            return -82;
        }
        return 0;
    }

    public int SetCal_New(string Key, string Path)
    {
        int ret;
        IntPtr hsignal;
        byte[] KeyBuf = new byte[16];
        byte[] inb = new byte[8];
        HexStringToByteArray(Key, ref KeyBuf);
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = NT_SetCal_New(KeyBuf, 0, Path, 8);
        if (ret != 0) goto error1;
        ret = NT_SetCal_New(KeyBuf, 1, Path, 0);
    error1:
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int Cal_New(byte[] InBuf, byte[] outbuf, string Path)
    {
        int ret;
        IntPtr hsignal;
        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        ret = NT_Cal_New(InBuf, outbuf, Path, 0);
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        return ret;
    }

    public int EncString_New(string InString, ref string OutString, string Path)
    {
        IntPtr hsignal;
        byte[] b;
        byte[] outb;
        int n;
        int nlen, temp_len;
        int ret = 0;

        nlen = lstrlenA(InString) + 1;
        temp_len = nlen;
        if (nlen < 8)
        {
            nlen = 8;
        }


        b = new byte[nlen];
        outb = new byte[nlen];

        CopyStringToByte(b, InString, temp_len);

        b.CopyTo(outb, 0);

        hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
        WaitForSingleObject(hsignal, INFINITE);
        for (n = 0; n <= (nlen - 8); n = n + 8)
        {
            ret = NT_Cal_New(b, outb, Path, n);
            if (ret != 0) break;
        }
        ReleaseSemaphore(hsignal, 1, 0);
        CloseHandle(hsignal);
        OutString = "";
        for (n = 0; n < nlen; n++)
        {
            OutString = OutString + outb[n].ToString("X2");
        }
        return ret;

    }

        public int NT_GetVersionEx(ref short Version, string Path)
        {
            int ret;
            IntPtr hsignal;
            hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = F_GetVersionEx(ref Version, Path);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;
        }

        private int NT_Cal_New(byte[] InBuf, byte[] outbuf, string Path, int pos)
        {
            int n;
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 12;
            for (n = 2; n <= 9; n++)
            {
                array_in[n] = InBuf[n - 2 + pos];
            }
            if (!SetFeature(hUsbDevice, array_in, 9)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 9)) { CloseHandle(hUsbDevice); return -93; }
            CloseHandle(hUsbDevice);
            for (n = 0; n < 8; n++)
            {
                outbuf[n + pos] = array_out[n];
            }
            if (array_out[8] != 0x55)
            {
                return -20;
            }
            return 0;
        }

        private int NT_SetCal_New(byte[] indata, byte IsHi, string Path, short pos)
        {

            int n;
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 13;
            array_in[2] = IsHi;
            for (n = 0; n < 8; n++)
            {
                array_in[3 + n] = indata[n + pos];
            }
            if (!SetFeature(hUsbDevice, array_in, 11)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 2)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0)
            {
                return -82;
            }

            return 0;
        }
        private int F_GetVersionEx(ref short Version, string Path)
        {
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 5;
            if (!SetFeature(hUsbDevice, array_in, 1)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 1)) { CloseHandle(hUsbDevice); return -93; }
            CloseHandle(hUsbDevice);
            Version = array_out[0];
            return 0;
        }


        int sub_YRead_new(ref string OutString, int Address, int nlen, string HKey, string LKey, string Path)
        {
            int n, ret = 0;
            byte[] outb;
            StringBuilder temp_OutString;
            outb = new byte[nlen];
            for (n = 0; n < nlen; n++)
            {
                ret = YRead(ref outb[n], Address + n, HKey, LKey, Path);
                if (ret != 0) return ret;
            }
            temp_OutString = new StringBuilder("", nlen);
            //初始化数据为0，注意，这步一定是需要的
            for (n = 0; n < nlen; n++)
            {
                temp_OutString.Append(0);
            }
            CopyByteToString(temp_OutString, outb, nlen);
            OutString = temp_OutString.ToString();
            return ret;
        }

        int sub_YWrite_new(string InString, int Address, string HKey, string LKey, string Path)
        {
            int n, ret = 0;
            byte[] b;
            int outlen = lstrlenA(InString); //注意，这里不写入结束字符串，与原来的兼容，也可以写入结束字符串，与原来的不兼容，写入长度会增加1
            b = new byte[outlen];
            CopyStringToByte(b, InString, outlen);
            for (n = 0; n < outlen; n++)
            {
                ret = YWrite(b[n], Address + n, HKey, LKey, Path);
                if (ret != 0) return ret;
            }
            return ret;
        }

        int sub_YReadEx(byte [] OutData, int Address, int nlen, string HKey, string LKey, string Path)
        {
            int n, ret = 0;
            for (n = 0; n < nlen; n++)
            {
                ret = YRead(ref OutData[n], Address + n, HKey, LKey, Path);
                if (ret != 0) return ret;
            }
            return ret;
        }

        int sub_YWriteEx(byte[] indata, int Address,int len, string HKey, string LKey, string Path)
        {
            int n, ret = 0;
            for (n = 0; n < len; n++)
            {
                ret = YWrite(indata[n], Address + n, HKey, LKey, Path);
                if (ret != 0) return ret;
            }
            return ret;
        }

        public int SetHidOnly(bool IsHidOnly, string Path)
        {
            int ret;
            IntPtr hsignal;
            hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_SetHidOnly(IsHidOnly, Path);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;
        }
        private int NT_SetHidOnly(bool IsHidOnly, string Path)
        {
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 0x55;
            if (IsHidOnly) array_in[2] = 0; else array_in[2] = 0xff;
            if (!SetFeature(hUsbDevice, array_in, 3))
            {
                CloseHandle(hUsbDevice); return -93;
            }
            if (!GetFeature(hUsbDevice, array_out, 1))
            {
                CloseHandle(hUsbDevice); return -93;
            }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0)
            {
                return -82;
            }
            return 0;
        }

        public int SetUReadOnly(string Path)
        {
            int ret;
            IntPtr hsignal;
            hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_SetUReadOnly(Path);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;
        }
        private int NT_SetUReadOnly(string Path)
        {
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 0x56;
            if (!SetFeature(hUsbDevice, array_in, 3))
            {
                CloseHandle(hUsbDevice); return -93;
            }
            if (!GetFeature(hUsbDevice, array_out, 1))
            {
                CloseHandle(hUsbDevice); return -93;
            }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0)
            {
                return -82;
            }
            return 0;
        }

        private int sub_GetTrashBufLen(IntPtr hDevice, ref int out_len)
        {

            IntPtr Ppd = System.IntPtr.Zero;
            HIDP_CAPS Caps = new HIDP_CAPS();

            if (!HidD_GetPreparsedData(hDevice, ref Ppd)) return -93;

            if (HidP_GetCaps(Ppd, ref Caps) <= 0)
            {
                HidD_FreePreparsedData(Ppd);
                return -93;
            }
            HidD_FreePreparsedData(Ppd);
            out_len = Caps.FeatureReportByteLength - 5;
            return 0;

        }
        private int GetTrashBufLen(string Path, ref int out_len)
        {

            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            int ret = sub_GetTrashBufLen(hUsbDevice, ref out_len);
            CloseHandle(hUsbDevice);
            return ret;

        }
        private int NT_Set_SM2_KeyPair(byte[] PriKey, byte[] PubKeyX, byte[] PubKeyY, byte[] sm2_UerName, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[25];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 0x32;
            for (n = 0; n < ECC_MAXLEN; n++)
            {
                array_in[2 + n + ECC_MAXLEN * 0] = PriKey[n];
                array_in[2 + n + ECC_MAXLEN * 1] = PubKeyX[n];
                array_in[2 + n + ECC_MAXLEN * 2] = PubKeyY[n];
            }
            for (n = 0; n < SM2_USENAME_LEN; n++)
            {
                array_in[2 + n + ECC_MAXLEN * 3] = sm2_UerName[n];
            }

            if (!SetFeature(hUsbDevice, array_in, ECC_MAXLEN * 3 + SM2_USENAME_LEN + 2)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 2)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0x20) return USBStatusFail;

            return 0;
        }
        private int NT_GenKeyPair(byte[] PriKey, byte[] PubKey, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[256];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;
            array_out[0] = 0xfb;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = GEN_KEYPAIR;
            if (!SetFeature(hUsbDevice, array_in, 2)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, ECC_MAXLEN * 3 + 2)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0x20)
            {

                return FAILEDGENKEYPAIR;//表示读取失败；

            }
            for (n = 0; n < ECC_MAXLEN; n++)
            {
                PriKey[n] = array_out[1 + ECC_MAXLEN * 0 + n];
            }
            for (n = 0; n < (ECC_MAXLEN * 2 + 1); n++)
            {
                PubKey[n] = array_out[1 + ECC_MAXLEN * 1 + n];
            }
            return 0;
        }

        private int NT_GetChipID(byte[] OutChipID, string Path)
        {
            int[] t = new int[8]; int n;
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = GET_CHIPID;
            if (!SetFeature(hUsbDevice, array_in, 1)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 17)) { CloseHandle(hUsbDevice); return -93; }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0x20) return USBStatusFail;
            for (n = 0; n < 16; n++)
            {
                OutChipID[n] = array_out[1 + n];
            }

            return 0;
        }


        private int NT_Get_SM2_PubKey(byte[] KGx, byte[] KGy, byte[] sm2_UerName, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[256];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 0x33;
            if (!SetFeature(hUsbDevice, array_in, 2)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, ECC_MAXLEN * 2 + SM2_USENAME_LEN + 2)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0x20) return USBStatusFail;

            for (n = 0; n < ECC_MAXLEN; n++)
            {
                KGx[n] = array_out[1 + ECC_MAXLEN * 0 + n];
                KGy[n] = array_out[1 + ECC_MAXLEN * 1 + n];
            }
            for (n = 0; n < SM2_USENAME_LEN; n++)
            {
                sm2_UerName[n] = array_out[1 + ECC_MAXLEN * 2 + n];
            }

            return 0;
        }

        private int NT_Set_Pin(string old_pin, string new_pin, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[256];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = SET_PIN;
            byte[] b_oldpin = new byte[PIN_LEN];
            CopyStringToByte(b_oldpin, old_pin, PIN_LEN);
            byte[] b_newpin = new byte[PIN_LEN];
            CopyStringToByte(b_newpin, new_pin, PIN_LEN);
            for (n = 0; n < PIN_LEN; n++)
            {
                array_in[2 + PIN_LEN * 0 + n] = b_oldpin[n];
                array_in[2 + PIN_LEN * 1 + n] = b_newpin[n];
            }

            if (!SetFeature(hUsbDevice, array_in, PIN_LEN * 2 + 2)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 2)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0x20) return USBStatusFail;
            if (array_out[1] != 0x20) return FAILPINPWD;
            return 0;
        }


        private int NT_SM2_Enc(byte[] inbuf, byte[] outbuf, byte inlen, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[256];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = MYENC;
            array_in[2] = inlen;
            for (n = 0; n < inlen; n++)
            {
                array_in[3 + n] = inbuf[n];
            }
            if (!SetFeature(hUsbDevice, array_in, inlen + 1 + 2)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, inlen + SM2_ADDBYTE + 3)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0x20) return USBStatusFail;
            if (array_out[1] == 0) return FAILENC;

            for (n = 0; n < (inlen + SM2_ADDBYTE); n++)
            {
                outbuf[n] = array_out[2 + n];
            }

            return 0;
        }

        private int NT_SM2_Dec(byte[] inbuf, byte[] outbuf, byte inlen, string pin, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[256];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = MYDEC;
            byte[] b_pin = new byte[PIN_LEN];
            CopyStringToByte(b_pin, pin, PIN_LEN);
            for (n = 0; n < PIN_LEN; n++)
            {
                array_in[2 + PIN_LEN * 0 + n] = b_pin[n];
            }
            array_in[2 + PIN_LEN] = inlen;
            for (n = 0; n < inlen; n++)
            {
                array_in[2 + PIN_LEN + 1 + n] = inbuf[n];
            }
            if (!SetFeature(hUsbDevice, array_in, inlen + 1 + 2 + PIN_LEN)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, inlen - SM2_ADDBYTE + 4)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[2] != 0x20) return FAILPINPWD;
            if (array_out[1] == 0) return FAILENC;
            if (array_out[0] != 0x20) return USBStatusFail;
            for (n = 0; n < (inlen - SM2_ADDBYTE); n++)
            {
                outbuf[n] = array_out[3 + n];
            }

            return 0;
        }

        private int NT_Sign(byte[] inbuf, byte[] outbuf, string pin, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[256];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = YTSIGN;
            byte[] b_pin = new byte[PIN_LEN];
            CopyStringToByte(b_pin, pin, PIN_LEN);
            for (n = 0; n < PIN_LEN; n++)
            {
                array_in[2 + PIN_LEN * 0 + n] = b_pin[n];
            }
            for (n = 0; n < 32; n++)
            {
                array_in[2 + PIN_LEN + n] = inbuf[n];
            }
            if (!SetFeature(hUsbDevice, array_in, 32 + 2 + PIN_LEN)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 64 + 3)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[1] != 0x20) return FAILPINPWD;
            if (array_out[0] != 0x20) return USBStatusFail;
            for (n = 0; n < 64; n++)
            {
                outbuf[n] = array_out[2 + n];
            }

            return 0;
        }

        private int NT_Sign_2(byte[] inbuf, byte[] outbuf, string pin, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[256];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = YTSIGN_2;
            byte[] b_pin = new byte[PIN_LEN];
            CopyStringToByte(b_pin, pin, PIN_LEN);
            for (n = 0; n < PIN_LEN; n++)
            {
                array_in[2 + PIN_LEN * 0 + n] = b_pin[n];
            }
            for (n = 0; n < 32; n++)
            {
                array_in[2 + PIN_LEN + n] = inbuf[n];
            }
            if (!SetFeature(hUsbDevice, array_in, 32 + 2 + PIN_LEN)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 64 + 3)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            if (array_out[1] != 0x20) return FAILPINPWD;
            if (array_out[0] != 0x20) return USBStatusFail;
            for (n = 0; n < 64; n++)
            {
                outbuf[n] = array_out[2 + n];
            }

            return 0;
        }


        private int NT_Verfiy(byte[] inbuf, byte[] InSignBuf, ref bool outbiao, string Path)
        {

            byte[] array_in = new byte[256];
            byte[] array_out = new byte[256];
            int n = 0; IntPtr hUsbDevice=IntPtr.Zero;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = YTVERIFY;
            for (n = 0; n < 32; n++)
            {
                array_in[2 + n] = inbuf[n];
            }
            for (n = 0; n < 64; n++)
            {
                array_in[2 + 32 + n] = InSignBuf[n];
            }
            if (!SetFeature(hUsbDevice, array_in, 32 + 2 + 64)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 3)) { CloseHandle(hUsbDevice); return -94; }
            CloseHandle(hUsbDevice);
            outbiao = (array_out[1] != 0);
            if (array_out[0] != 0x20) return USBStatusFail;

            return 0;
        }
        private string ByteArrayToHexString(byte[] in_data, int nlen)
        {
            string OutString = ""; int n;
            for (n = 0; n < nlen; n++)
            {
                OutString = OutString + in_data[n].ToString("X2");
            }
            return OutString;
        }

        public int YT_GenKeyPair(ref string PriKey, ref string PubKeyX, ref string PubKeyY, string InPath)
        {

            int ret, n; byte[] b_PriKey = new byte[ECC_MAXLEN], b_PubKey = new byte[ECC_MAXLEN * 2 + 1];//其中第一个字节是标志位，忽略
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_GenKeyPair(b_PriKey, b_PubKey, InPath);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            PriKey = ByteArrayToHexString(b_PriKey, ECC_MAXLEN);
            PubKeyX = ""; PubKeyY = "";
            for (n = 0; n < ECC_MAXLEN; n++)
            {
                PubKeyX = PubKeyX + b_PubKey[n + 1].ToString("X2");
                PubKeyY = PubKeyY + b_PubKey[n + 1 + ECC_MAXLEN].ToString("X2");

            }
            return ret;

        }

        public int Set_SM2_KeyPair(string PriKey, string PubKeyX, string PubKeyY, string SM2_UserName, string InPath)
        {

            int ret; byte[] b_PriKey = new byte[ECC_MAXLEN], b_PubKeyX = new byte[ECC_MAXLEN], b_PubKeyY = new byte[ECC_MAXLEN], b_SM2UserName = new byte[SM2_USENAME_LEN];
            HexStringToByteArray(PriKey, ref b_PriKey);
            HexStringToByteArray(PubKeyX, ref b_PubKeyX);
            HexStringToByteArray(PubKeyY, ref b_PubKeyY);
            CopyStringToByte(b_SM2UserName, SM2_UserName, SM2_USENAME_LEN);
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_Set_SM2_KeyPair(b_PriKey, b_PubKeyX, b_PubKeyY, b_SM2UserName, InPath);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);

            return ret;

        }

        public int Get_SM2_PubKey(ref string PubKeyX, ref string PubKeyY, ref string sm2UserName, string InPath)
        {

            int ret; byte[] b_PubKeyX = new byte[ECC_MAXLEN], b_PubKeyY = new byte[ECC_MAXLEN], b_sm2UserName = new byte[SM2_USENAME_LEN];
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_Get_SM2_PubKey(b_PubKeyX, b_PubKeyY, b_sm2UserName, InPath);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            PubKeyX = ByteArrayToHexString(b_PubKeyX, ECC_MAXLEN);
            PubKeyY = ByteArrayToHexString(b_PubKeyY, ECC_MAXLEN);
            StringBuilder c_str = new StringBuilder("", SM2_USENAME_LEN);
            CopyByteToString(c_str, b_sm2UserName, SM2_USENAME_LEN);
            sm2UserName = c_str.ToString();
            return ret;

        }

        public int GetChipID(ref string OutChipID, string InPath)
        {

            int ret; byte[] b_OutChipID = new byte[16];
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_GetChipID(b_OutChipID, InPath);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            OutChipID = ByteArrayToHexString(b_OutChipID, 16);
            return ret;

        }

        public int SM2_EncBuf(byte[] InBuf, byte[] OutBuf, int inlen, string InPath)
        {

            int ret = 0, n, temp_inlen, incount = 0, outcount = 0; byte[] temp_InBuf = new byte[MAX_ENCLEN+ SM2_ADDBYTE], temp_OutBuf = new byte[MAX_ENCLEN + SM2_ADDBYTE];
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            while (inlen > 0)
            {
                if (inlen > MAX_ENCLEN)
                    temp_inlen = MAX_ENCLEN;
                else
                    temp_inlen = inlen;
                for (n = 0; n < temp_inlen; n++)
                {
                    temp_InBuf[n] = InBuf[incount + n];
                }
                ret = NT_SM2_Enc(temp_InBuf, temp_OutBuf, (byte)temp_inlen, InPath);
                for (n = 0; n < (temp_inlen + SM2_ADDBYTE); n++)
                {
                    OutBuf[outcount + n] = temp_OutBuf[n];
                }
                if (ret != 0) goto err;
                inlen = inlen - MAX_ENCLEN;
                incount = incount + MAX_ENCLEN;
                outcount = outcount + MAX_DECLEN;
            }
        err:
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;

        }

        public int SM2_DecBuf(byte[] InBuf, byte[] OutBuf, int inlen, string pin, string InPath)
        {

            int ret = 0, temp_inlen, n, incount = 0, outcount = 0; byte[] temp_InBuf = new byte[MAX_ENCLEN+ SM2_ADDBYTE], temp_OutBuf = new byte[MAX_ENCLEN + SM2_ADDBYTE];
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            while (inlen > 0)
            {
                if (inlen > MAX_DECLEN)
                    temp_inlen = MAX_DECLEN;
                else
                    temp_inlen = inlen;
                for (n = 0; n < temp_inlen; n++)
                {
                    temp_InBuf[n] = InBuf[incount + n];
                }
                ret = NT_SM2_Dec(InBuf, temp_OutBuf, (byte)temp_inlen, pin, InPath);
                for (n = 0; n < (temp_inlen - SM2_ADDBYTE); n++)
                {
                    OutBuf[outcount + n] = temp_OutBuf[n];
                }
                if (ret != 0) goto err;
                inlen = inlen - MAX_DECLEN;
                incount = incount + MAX_DECLEN;
                outcount = outcount + MAX_ENCLEN;
            }
        err:
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;

        }

        public int SM2_EncString(string InString, ref string OutString, string InPath)
        {
            int n, incount = 0, outcount = 0; byte[] temp_InBuf = new byte[MAX_ENCLEN+ SM2_ADDBYTE], temp_OutBuf = new byte[MAX_ENCLEN + SM2_ADDBYTE];
            int inlen = lstrlenA(InString) + 1;
            int outlen = (inlen / (MAX_ENCLEN)+1) * SM2_ADDBYTE + inlen;
            byte[] OutBuf = new byte[outlen];
            byte[] InBuf = new byte[inlen];
            CopyStringToByte(InBuf, InString, inlen);
            int ret = 0, temp_inlen;
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            while (inlen > 0)
            {
                if (inlen > MAX_ENCLEN)
                    temp_inlen = MAX_ENCLEN;
                else
                    temp_inlen = inlen;
                for (n = 0; n < temp_inlen; n++)
                {
                    temp_InBuf[n] = InBuf[incount + n];
                }
                ret = NT_SM2_Enc(temp_InBuf, temp_OutBuf, (byte)temp_inlen, InPath);
                for (n = 0; n < (temp_inlen + SM2_ADDBYTE); n++)
                {
                    OutBuf[outcount + n] = temp_OutBuf[n];
                }
                if (ret != 0) goto err;
                inlen = inlen - MAX_ENCLEN;
                incount = incount + MAX_ENCLEN;
                outcount = outcount + MAX_DECLEN;
            }
        err:
            OutString = ByteArrayToHexString(OutBuf, outlen);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;

        }

        public int SM2_DecString(string InString, ref string OutString, string pin, string InPath)
        {
            int n, incount = 0, outcount = 0; byte[] temp_InBuf = new byte[MAX_ENCLEN+ SM2_ADDBYTE], temp_OutBuf = new byte[MAX_ENCLEN + SM2_ADDBYTE];
            int inlen = lstrlenA(InString) / 2;
            int outlen = inlen - (inlen / (MAX_DECLEN)+1) * SM2_ADDBYTE;
            byte[] InBuf = new byte[inlen];
            byte[] OutBuf = new byte[outlen];
            int ret = 0, temp_inlen;
            HexStringToByteArray(InString, ref InBuf);
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            while (inlen > 0)
            {
                if (inlen > MAX_DECLEN)
                    temp_inlen = MAX_DECLEN;
                else
                    temp_inlen = inlen;
                for (n = 0; n < temp_inlen; n++)
                {
                    temp_InBuf[n] = InBuf[incount + n];
                }
                ret = NT_SM2_Dec(temp_InBuf, temp_OutBuf, (byte)temp_inlen, pin, InPath);
                for (n = 0; n < (temp_inlen - SM2_ADDBYTE); n++)
                {
                    OutBuf[outcount + n] = temp_OutBuf[n];
                }
                if (ret != 0) goto err;
                inlen = inlen - MAX_DECLEN;
                incount = incount + MAX_DECLEN;
                outcount = outcount + MAX_ENCLEN;
            }
        err:
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            StringBuilder c_str = new StringBuilder("", outlen);
            CopyByteToString(c_str, OutBuf, outlen);
            OutString = c_str.ToString();
            return ret;

        }



        public int YtSetPin(string old_pin, string new_pin, string InPath)
        {
            int ret;
            IntPtr hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_Set_Pin(old_pin, new_pin, InPath);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;
        }
        private int NT_SetID(byte[] InBuf, string Path)
        {
            int n;
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 7;
            for (n = 2; n <= 9; n++)
            {
                array_in[n] = InBuf[n - 2];
            }
            if (!SetFeature(hUsbDevice, array_in, 9)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 9)) { CloseHandle(hUsbDevice); return -93; }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0x0)
            {
                return -82;
            }
            return 0;
        }

        public int SetID(string Seed, string Path)
        {
            int ret;
            IntPtr hsignal;
            int n;
            byte[] KeyBuf = new byte[8];
            for (n = 0; n < 8; n++)
            {
                KeyBuf[n] = 0;
            }
            HexStringToByteArray(Seed, ref KeyBuf);
            hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_SetID(KeyBuf, Path);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;
        }

        private int NT_GetProduceDate(ref string OutDate, string Path)
        {
            int n;
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 15;
            if (!SetFeature(hUsbDevice, array_in, 1)) { CloseHandle(hUsbDevice); return -93; }
            if (!GetFeature(hUsbDevice, array_out, 8)) { CloseHandle(hUsbDevice); return -93; }
            CloseHandle(hUsbDevice);
            OutDate = "";
            for (n = 0; n < 8; n++)
            {
                OutDate = OutDate + array_out[n].ToString("X2");
            }
            return 0;
        }

        //返回锁的出厂编码
        public int GetProduceDate(ref string PDate, string Path)
        {
            int ret;
            IntPtr hsignal;
            hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = NT_GetProduceDate(ref PDate, Path);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;
        }

        public string SnToProduceDate(string InSn)
        {
            string OutString;
            OutString = (2000 + HexToInt(InSn.Substring(0, 2))).ToString() + "年";
            OutString = OutString + (HexToInt(InSn.Substring(2, 2))).ToString() + "月";
            OutString = OutString + (HexToInt(InSn.Substring(4, 2))).ToString() + "日";
            OutString = OutString + (HexToInt(InSn.Substring(6, 2))).ToString() + "时";
            OutString = OutString + (HexToInt(InSn.Substring(8, 2))).ToString() + "分";
            OutString = OutString + (HexToInt(InSn.Substring(10, 2))).ToString() + "秒--";
            OutString = OutString + "序号：" + HexToInt(InSn.Substring(12, 4)).ToString();
            return OutString;
        }

        private int y_setcal(byte[] indata, int address, int nlen, byte[] password, string Path)
        {
            int n;
            byte[] array_in = new byte[25];
            byte[] array_out = new byte[25];
            IntPtr hUsbDevice=IntPtr.Zero;
            if (nlen > 8) return -87;

            if (OpenMydivece(ref hUsbDevice, Path) != 0) return -92;
            array_in[1] = 6;
            array_in[2] = 0;
            array_in[3] = 0;
            array_in[4] = (byte)nlen;
            for (n = 0; n <= 7; n++)
            {
                array_in[5 + n] = password[n];
            }
            for (n = 0; n < nlen; n++)
            {
                array_in[13 + n] = indata[n];
            }
            if (!SetFeature(hUsbDevice, array_in, 13 + nlen))
            {
                CloseHandle(hUsbDevice); return -93;
            }
            if (!GetFeature(hUsbDevice, array_out, 2))
            {
                CloseHandle(hUsbDevice); return -93;
            }
            CloseHandle(hUsbDevice);
            if (array_out[0] != 0)
            {
                return -82;
            }
            return 0;
        }

        public int SetCal(string W_HKey, string W_LKey, string new_HKey, string new_LKey, string Path)
        {
            int ret;
            IntPtr hsignal;
            byte[] ary1 = new byte[8];
            byte[] ary2 = new byte[8];
            short address;
            myconvert(W_HKey, W_LKey, ary1);
            myconvert(new_HKey, new_LKey, ary2);
            address = 0;
            hsignal = CreateSemaphore(0, 1, 1, "ex_sim");
            WaitForSingleObject(hsignal, INFINITE);
            ret = y_setcal(ary2, address, 8, ary1, Path);
            ReleaseSemaphore(hsignal, 1, 0);
            CloseHandle(hsignal);
            return ret;
        }

        public int CheckKeyByFindort_2()
        {
            //使用普通算法一查找指定的加密锁
            string DevicePath = ""; //用于储存加密锁所在的路径
            return FindPort_2(0, 1, -199635098, ref DevicePath);
        }

        //使用带长度的方法从指定的地址读取字符串
        private int ReadStringEx(int addr, ref string outstring, string DevicePath)
        {
            int nlen, ret;
            byte[] buf = new byte[1];
            //先从地址0读到以前写入的字符串的长度
            ret = YReadEx(buf, (short)addr, (short)1, "664307B7", "4E2E8337", DevicePath);
            if (ret != 0) return ret;
            nlen = buf[0];
            //再读取相应长度的字符串
            return YReadString(ref outstring, addr + 1, nlen, "664307B7", "4E2E8337", DevicePath);

        }
        //使用从储存器读取相应数据的方式检查是否存在指定的加密锁
        public int CheckKeyByReadEprom()
        {
            int n, ret;
            string DevicePath = "";//用于储存加密锁所在的路径
            string outstring = "";
            //@NoUseCode_data return 1;//如果没有使用这个功能，直接返回1
            for (n = 0; n < 255; n++)
            {
                ret = FindPort(n, ref DevicePath);
                if (ret != 0) return ret;
                ret = ReadStringEx(0, ref outstring, DevicePath);
                if ((ret == 0) && (outstring.CompareTo("shenzhen1-20210324") == 0)) return 0;
            }
            return -92;
        }
        //使用增强算法一检查加密锁，这个方法可以有效地防止仿真
        public int CheckKeyByEncstring()
        {
            //推荐加密方案：生成随机数，让锁做加密运算，同时在程序中端使用代码做同样的加密运算，然后进行比较判断。

            int n, ret;
            string DevicePath = "";//用于储存加密锁所在的路径
            string InString;

            //@NoUseKeyEx return 1;//如果没有使用这个功能，直接返回1
            System.Random rnd = new System.Random();

            InString = rnd.Next(0, 32767).ToString("X") + rnd.Next(0, 32767).ToString("X");

            for (n = 0; n < 255; n++)
            {
                ret = FindPort(n, ref DevicePath);
                if (ret != 0) return ret;
                if (Sub_CheckKeyByEncstring(InString, DevicePath) == 0) return 0;
            }
            return -92;
        }

        private int Sub_CheckKeyByEncstring(string InString, string DevicePath)
        {
            //使用增强算法一对字符串进行加密
            int ret;
            string outstring = "";
            string outstring_2;
            ret = EncString(InString, ref outstring, DevicePath);
            if (ret != 0) return ret;
            outstring_2 = StrEnc(InString, "A09BAF8BBCE49EE2CE7F0F0502D8D8F2");
            if (outstring_2.CompareTo(outstring) == 0)//比较结果是否相符
            {
                ret = 0;
            }
            else
            {
                ret = -92;
            }
            return ret;
        }

        //使用增强算法二检查是否存在对应的加密锁
        public int CheckKeyByEncstring_New()
        {
            int n, ret;
            string DevicePath = "";//用于储存加密锁所在的路径
            string outstring = "";
            System.Random rnd = new System.Random();
            int myrnd = rnd.Next(0, 500);
            String[] EncInString ={ "2102","4680","14593","11788","27465","22134","17470","9469","23678","8382","11182","15316","20608","15137","6475","23716","20759","12355","29999","14111","5936","32099","20373","663","17617","25702","27349","24572","16167","24012",
"25963","23522","10363","10936","1009","4220","31287","1771","6943","26522","16102","31431","6608","31918","27696","19792","13054","7884","23777","5671","10180","29347","27361","28556","12634","13448","10557","18532","2844","6763",
"15899","22063","7299","21982","23752","10000","17192","13845","24494","21448","15559","27103","598","20922","12602","25009","15209","25831","23673","20891","14002","12787","22687","21328","28444","28701","2023","10273","26799","15178",
"7850","6913","2712","1531","19809","642","6430","11088","2729","25301","20396","8461","32056","12237","6861","18836","1516","11996","29225","26086","27067","30994","21131","10151","7107","7461","22494","25366","10044","24930",
"30507","24856","14719","9664","22684","20781","19464","26061","20859","23416","28272","23229","32306","21178","12401","15715","31819","1623","11237","10286","17521","13390","11975","6450","9325","6979","1327","5153","22824","11384",
"25432","25676","18606","24014","28573","32422","23592","7112","27766","961","31200","23432","640","25038","8001","19663","10866","5792","6867","11264","14119","4521","26177","13341","32665","13818","8278","12487","10467","31813",
"9979","12353","4158","13534","20794","21868","21763","27061","18749","18891","686","24668","1614","20352","23255","15367","26327","29516","16187","16262","27234","24210","15281","12107","2970","18772","12340","21353","14920","3123",
"6879","6546","30037","21106","490","22295","1230","27634","32176","10988","21465","16182","25260","32672","27331","23392","2936","11623","2294","10802","17099","15379","24313","16908","19842","17686","26746","22515","5271","30080",
"26176","20871","28450","12295","30775","30474","3167","20217","22376","17760","32166","1610","12285","24585","24691","25995","6977","9000","10335","31215","14748","21886","2341","4169","27801","31342","157","5897","12515","17402",
"194","8257","6210","9411","14766","932","25506","524","924","8323","15628","1463","26852","16337","4522","4165","20482","5664","3014","19160","17807","26016","11466","22835","26181","16422","1857","11193","26878","26915",
"6355","29033","18612","22440","17483","10727","3714","22632","27540","22389","15264","20404","10679","20724","21354","7039","11486","13288","1369","5467","12371","30320","30470","13433","5761","6481","11822","20396","8944","31210",
"6824","32613","21579","17472","16495","8120","24156","1268","414","29268","17422","23366","2158","24717","4459","19226","20792","23893","13722","12019","21624","26430","31976","18928","20678","10874","10943","31189","13535","17113",
"13223","2309","10650","7632","5964","8319","15346","31855","23158","23015","9986","5481","8465","29055","15874","22287","26661","6185","30285","18772","24542","25957","4447","4042","25989","26968","26318","2979","7557","24258",
"20055","7599","11622","32006","10708","2147","5588","27174","3901","29134","11415","19651","4782","1446","13979","32288","12383","14137","8681","23582","22187","9083","24491","15169","4110","4862","24107","30888","19742","7155",
"8915","11288","20120","25644","1070","31216","12118","798","997","1334","3664","983","18625","392","22689","1802","14509","25897","13188","1291","12980","15101","1618","12234","26879","5252","9348","29913","23256","7137",
"6070","8044","28329","17877","2465","6738","11261","9155","19999","20992","5501","21932","3371","13958","8946","7220","3293","23958","23367","2715","7025","17540","3720","22723","6463","12864","12252","21807","8899","14685",
"20366","30464","23190","23052","13576","4090","21362","17440","6357","7805","6705","14602","32412","16267","24525","3550","12075","16584","17249","19787" };
            String[] EncOutString ={ "D8D2FD11334B5D5D","EE6E6651AAC751D4","886A85912F898348","A6FC162EBFCFD82B","CBA5A84B8475B22E","A3C3DE0AEEE949D8","3653F8166A4A901E","C8F2C6061B4D2E28","18680F8131F69039","CF1D3C7C0D7A1D7F","E45E792A8C7E05C9","38B1E0C3D96B5638","08BCF3DC664E6884","F8D2ECF308B62293","566DF96797DF88DF","A560FEFC89DF186F","639292031A33947D","A8F362E05C82B370","AE2DAA36AAF31612","213918AD00B61C42","6B53666A5DB3220A","1659FB23F0C8E327","39B3AD9A6F5DBAB9","776164F12312547A","ACE7DF2BB834A2BC","C37C8A27FA579B3D","38DA2E7379547149","0A2CE6C843ACAA54","1ECD74EDE1048ECD","6AA5B9C45F27FA25",
"D9FB1D6D21E3A54D","1BE118EBB60793F0","B9310FCB5DB97F60","E504F218E56FFF2D","D49AA738CF1BC358","1F896332F20230B1","14E55181B8D246A4","F30F18E74DD5B38A","9B7C60585F0B9819","D1F197649B539746","2B40A71E044D99FB","AEBCDC61F1BA119D","04525AF3212B0179","0FBBCDE1EED8D2AB","E7AD69378122142C","3A27BB13380BD17B","B63CBB5D70943F28","4FA30EF3DEB5F5D2","DB3F6F3FFCA9198A","C439E07493B5EE60","4502BF41441CC0DF","ECFE3390B29124B6","76332BCA1BE24952","5FE66F0861BEB0BE","38F8C3AB9423DCD8","CB15515E48F95664","2BFF6A8088AED841","B1E87225DCA50B28","7F8F01CF48BEF3BA","F6CF9103936FE3C7",
"23A39754F5E8006B","8D434EE6C284C68E","14A37D975D53ACFD","CA5A4A8675239711","C1AD509F94D74F71","89F727AC5A13591C","C185AB7C693EC331","64A39284A43E30A3","A6EB6CD59CAEB8FB","EE69BB664BAABA3B","BD1585D7252023E5","C91B4E361EC2AC84","A5B504CC7F58DA70","823F619C859EF6B9","D0287A68A4BADC68","C5A26C0BBD72AB85","F938ABA0090A6717","9A6FF986AC6773E5","0E8C0BD8748119EB","ED5686C9D58E1420","EC7EBF11ADE61650","E47E15D2A1011B7C","667F0C13E31FF7C2","2A2BCDB7D00B5628","887A6ADDA00074D2","744F578F5B2BE1C9","53ABECE88DF976A0","40595D4CAABDD967","B53DE729077111B3","C60F43A57E2FB4E2",
"9F9944064B1E7B9A","7D851D7D773041E1","1B26FE1D1FC0D330","5242AA8382235E70","5BA3C675DBF3AA2E","5064C4296295F7C9","B521BDD082F19C26","35CF5B7CF58189C9","3DC4924ECA5BB687","FECB6656374B8433","7D43FCE6D6B43D60","36B9105F7A8241AC","DBE01EB35FE0EA4A","88D87D901EEA99F8","1A98B4A99B7BC754","078CB455B9E2A5BD","DF8365890A31811F","30B0FB6C541E4CD7","2A47FB09AB673D39","AF8C3E6BB57E12D1","DDBA70201481C435","737BBDFA2DE832E9","5FD033A49E75EC0B","B0180569D2291F0B","2D730C9AAC081547","EC30CAD0C18F65C7","454F21B5C053FC12","29757FAC3B735F10","E0819F2F868C51A3","C000D59917B317A7",
"18EE413AE6E94102","12EBA9686C70790E","619214FFC42CA1F3","F9F1F0052DDADA82","478F12012ED83D2B","75FDDE8EA7127330","468111E0A0B4D0B7","B5D1C7F55F8C63E3","4E8D652BAC706372","ABE899CD71B5E1BC","F28DB5B228B81725","DA5936561C73CA8B","950AA2A084681955","689F907A833038F9","FF75A543EA252A78","E0878C184DB2E399","4A22173007E7B11A","69A4F497051A09E0","909297CAD27761AA","D709E44B29FFD81C","8D54735AF498C70C","42416DE6F1CF0C5D","6E10A0E58084C650","2F091956CBA3C0E1","F3083F009171BF57","2DDD0B507453072A","315A63443B20AB26","B602993FD2A697C7","6D38A71500DABC57","E7E9D89618F09597",
"61CEB4F5354ED89A","90BEABBE5ED2FB7F","F15A83CD30F7B5A9","86DA98AC9C603051","DEAA7F8B3F38CA17","E5C831EF32A330B6","160A730E9866C2AD","0CA21B89A0144225","1FCD700285E0E7A9","3CBE736B6D769370","74A82F33E24CE394","99A4BED8881401A4","5005D71D54B1ADC8","7DC70BED86A2CFCA","6CAEE35898358BF2","2C03E4BCC5BA8583","6F052E4263601070","12940296E67596D1","04E5B8A6D3BA7DC0","CC0D58DC34E7FCFE","6C2F265F95343AE1","514E129ED3912C22","BF6E4C9C984D1CFD","901E701B04277FCE","13DD9F927C645E49","3B222E60DCCF084C","FD1BE4D953D336D8","54534899AADB8E9A","A70BAE72E9F69000","810EF264C526B5A3",
"C72DB2470221625A","09CA8D86D060C63F","993953737BDE4E07","4CAECDB00DF51E37","CE1159F148E62B0D","F4B824FA86E91022","4DD23767F9F3208F","9BDFAFFC3F817A83","4DAEEB3155DD5B48","9EE22A5BEFD2E8C1","B185B74BA3063D7D","691ECB4604064C54","52EC3A3AEEC3BC98","FBC711720A11343E","1C7538AC083908E6","10F138C5E9E4D2CE","DFF131658DC37DCA","82CB0430BBAB72C2","A5DCE08E63116BFE","25AC1E71B5B8D8A3","E6E7D5B9038D3A23","BF3AB537629B03BA","8B4BCFB15A3353FC","3EDD25236E5C2194","71929C2F9D263356","0D4E182E2EC5DC68","6C59EA93D8172BC9","54EAD835DADED8A4","396E117E1011D03F","46CD9BCD2833226B",
"59F02BD6C8A4BD20","667328EE925FA164","22EA7725A25552D1","225BFE1D7F11ACE5","C07A668E21CAD0D0","E061F300B3B97BFF","E393A31E3478DAB1","1F21A9503BAFD6B5","3CF345E375A80295","455FA52CA115AC7D","8AD3692E49D67A7B","07104962186212D7","6098761D8945AD7F","7C710865C3B03822","F1D77510AABE0184","713796C2DF842D86","E6A220C21D54A70F","BCEB956EE8736946","0E000B466888324D","2DC8B79B00B23598","D93142C81976A43E","66F5CD75C4DDD74A","53BF5918407D92D6","428D7FCDD387F4FE","7169042D683CF49C","696CC1AE909E09C5","F64E124E13E1E2C6","81EC886D2A14DAAC","5695D2E188199C85","2240AA63134C4236",
"127BAEC395906190","7641C329F1409ED4","6990F7B92AF12F30","21CF23297D4CC097","A32A4E6E3D56C55D","5CD9ED4544158034","3EAB8AD8789248E4","BE8F0AB80AF93A74","BB3669668AE5CC60","6F7108428C280817","3F677663EF26660F","51558F65ADAE7B4E","5ED1EB15478D6188","FFC173112DD8BEFB","77F796A63BB30F8F","245CAC6CE9075EBD","5670BE687AC03D81","821E03A342994432","6FBC5BD5530554BF","17C573BB78681711","5C6FAE41813A3C85","C6554A28932603E6","5F562E9203526A38","DD7AD594A378BC6C","C30DA2D158BD1AF3","08E38BF874A24BB1","589EE568130190AA","27D974B3B29B3F44","2E611484829ADCE9","088CA9AA10E3F069",
"3D3913978036141C","F8EF0ECD1053A782","4564A077111BBEDD","47335EF920842F31","B22FF78B1C3B3083","5E031DACE787EEE5","8A2E6124BB43A299","C857E3BDF190BF88","6F6418FB050FBCB6","3E0633C935447A65","642ADA6F38E7B19B","561A8E0157A0A4C3","756607CE0F2AD01B","6F08554C3DEF1A3C","CBBC4EC47FD90B3B","2508F7C55DCDB368","3D8BAA21CC22D021","0B9C490E04A20AFA","987DA41A04C73D20","698A90F284D722F7","04FE67A5C988070B","A14FAD82FC64B7E7","BAA599D5B595EDBB","379311ACE14029C8","EC1ECF9F471E5F0E","BB3060F867A84C3F","950D84D690190201","F925D52F52ABC0A6","FEFBDD3BB082B371","3E7B9BD439427589",
"054DB234AC534EEA","67A0DE627924FE6A","F4516AF0C40DF23F","9F5B7FE2CBF728A0","A7F0D2F11D3164F7","351EAFDFB5406BD7","981493AA28A41992","71954F8EDC379B6C","5C7E1D93BA51D7DB","A45184696A33CAF2","9E5F409370DED572","D2ADB32A6FCC37DB","C705054EA2D09703","68DD4084BF8AE9BD","BE7ACCA3E3098B8F","528EBC5F0A94B36F","35FC9CAB2D9A301B","2CB67B3F7960B74A","D8B4865E61F88E61","ADC7759A8A2BB34C","5675F531B037BED7","6D25CCD56B0E5ADB","51CC471B0322614B","FE1AD568AA8BBAA9","3DC3F36B6D08DCA9","D27B3544E7B625A8","D3029CB8F5F64DAC","7D43FCE6D6B43D60","080BAA600576C534","BBD1AD6F0B2DDCA5",
"CC203C0A3784D454","464C6521006FE8EE","7443B415388CA99A","9BD80902BD1577EB","FA91A5C46DF88C57","1190F94A1B0FB0D6","E12DB297747B271E","4AD739F53B460344","ED102D14AFA8A222","5B2FD81FB116CFBB","AD1A29EA582F95C8","128BB02FA2B9C6F6","389AF20059688027","CCF0132056DBEBFC","FF43D8746E77773E","0B090FF7275CE677","48534602F6A3414D","6BDC2083ED845E45","E3C76EDDD8757608","C693AA7EC7A99632","E447ECC4831DA87F","AFFD1C8AE0CB7FBF","AA9CC1057F0C4E87","169C78B90C7ED35B","C4ACB5EB18F63840","B21E7016B0C3C07F","744E58DE317DC04B","EDF516A7EA9D9737","34FC27D6FFCA4955","5EB326DD2C3BB7A6",
"748FED2BFC5F972C","847C21E2295B68B6","BA55B5D2E6503593","D667035538D45012","94AFDE1808F3436A","FCF61216B329BD52","FBFB5CB722FB5CAD","9819C40A0936E8B3","E1668BEBEEA899A1","6933FA84813A7DC2","5B2203351E9EA2A4","CF95DF07732685EC","59EBA677F4A16E43","0F689B9528026DEB","AF7D286A7842EE05","A8A8C8FF75AB5A25","9EB2817BECD9CB1D","4EBF6AE6857E7144","E328B8F965ECBFDC","0D4E182E2EC5DC68","75111C73F628D310","4DAE1B8A6F9E3928","D46190EF6F7906DF","53AC7837A1D641F5","B520741FCD023CE5","D68047E2021D94A8","561B241211415722","B604188E60B9089D","117100A0737DC758","70FCFACE42FA2829",
"63D438CBFBE1669E","F69FFBA4AD9C1836","E6471017FEE05CD1","A6663C2EE94ACC6D","FC58A09348FB48E7","FC866464621611CA","685B7CD3BE0049FC","1B7F4A37BAD97234","2EABC5EF0791C010","62997980B8F1351A","4099FE831A5B55E5","6EF9578660253872","3070EC613B2570BF","0F6766C96EF405A9","6BD8E7AAE306542B","0B84CC017AFAD578","C0E4542B84A6842A","DA15483D41A9FDDF","FB921DBB5F986D22","0A0D8780F7031286","BA2A9EB2C6FD4B16","ED72CEEFF262FF5E","6930570B27BD1A65","9E4BDB13FE1242BA","CE082B10F8656235","617C6527143518CB","999A2B057D43F0AF","E7B09B4A3B6D3D04","6E7772F56C398150","D39B06C30B8C5746",
"8C0DE01125C555BE","32380C5E1D529916","52924283DC35E9E8","4CA367FFDBBA1A09","F1B0E0DD834F947D","7DF4B4ACBBD47D9E","69BF89E176E864E5","61EBF9755DC08AFA","655BF93D6837D55B","0450AA9039FDBD9D","11447452148CF246","5362EA51BC6F7C82","2724C006C4D318AA","DF0EEE81C0C67CB3","E335B714CA5ACBCF","7A8EF5C860119A02","F3AED71A215ECB4D","8E16E2EE8DFC8660","CEA35A141B372FCA","722973ECE624A118","4F7A3624569B5692","2F038444D0FA182F","20C1D9C10A31D5B6","84092FCE8456DA71","D06DBED75D596B40","DD89E4270465A3B6","F97120334A20FB89","82E68D11A283FECC","1381C4E5F99A79D2","474B7BA5B9F958C1",
"3014DA55AE6E6CFC","107FCF03EC79714E","87E9681DFF80184F","FF1E779E5C345276","DA7057C732EF0832","3408A808C878B8E3","4A4DB56EE1A8212A","CD8EF8261535EF76","9CD7543E06A96CE0","5F31768C8EECEEC6","7CC6E5A0ECEEB984","D8EDC1AC42EABEA6","223073BDD826A0FD","AB83253A22903CBC","BF3CDBA198E469EC","317487BD574DA261","A19BC45913AE5B34","D1D790FE75E87EF6","27ED172C7D5344EB","DD2964312F487F26","13F44D47B47181B7","4FE9A00B8D599F18","7188427C7B5FC2E9","07987866A9B81FE0","08115FEE1FBE3456","741D76C0DD2EADE9","E1E927E11B20BE26","A9F3612A78483D86","42A43DEE32830286","DC593124D1CCFE71",
"6B1659DB408DD11C","0B1B764B192EAF9E","342DA0A929E9EAA3","1B180E95EDFC6754","54B89F96F85D26D7","57A9561CB21DA8BE","23B25EAFECF806A9","67253B9403DCCB01","0084D83CD31967A5","9A56614FE5C3FE4F","263A82EFE99B8D67","3C9526AEB8A93E9F","6739C4095988B3BA","03BE5463CBC3912E","AD15D59BB33EA07E","CC6101EAF2ADCDBF","83658C73F01DC5D1","08E7FE692C4B6A89","42AF1D7510C2D94B","DCDC117B5E149F5A" };
            //@NoUseNewKeyEx return 1;//如果没有使用这个功能，直接返回1
            //@NoSupNewKeyEx return 2;//如果该锁不支持这个功能，直接返回2
            for (n = 0; n < 255; n++)
            {
                ret = FindPort(n, ref DevicePath);
                if (ret != 0) return ret;
                ret = EncString_New(EncInString[myrnd], ref outstring, DevicePath);
                if ((ret == 0) && (outstring.CompareTo(EncOutString[myrnd]) == 0)) return 0;
            }
            return -92;
        }
    }
}
