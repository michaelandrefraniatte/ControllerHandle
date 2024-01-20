﻿using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HidHandle
{
    internal class WindowsHidApiService : ApiService, IHidApiService
    {
        private static Guid? _HidGuid;
        
        public WindowsHidApiService()
        {
        }

        [DllImport("hid.dll", SetLastError = true)]
        private static extern bool HidD_GetPreparsedData(SafeFileHandle hidDeviceObject, out IntPtr pointerToPreparsedData);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool HidD_GetManufacturerString(SafeFileHandle hidDeviceObject, IntPtr pointerToBuffer, uint bufferLength);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool HidD_GetProductString(SafeFileHandle hidDeviceObject, IntPtr pointerToBuffer, uint bufferLength);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool HidD_GetSerialNumberString(SafeFileHandle hidDeviceObject, IntPtr pointerToBuffer, uint bufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern int HidP_GetCaps(IntPtr pointerToPreparsedData, out HidCollectionCapabilities hidCollectionCapabilities);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern bool HidD_GetAttributes(SafeFileHandle hidDeviceObject, out HidAttributes attributes);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern void HidD_GetHidGuid(out Guid hidGuid);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern bool HidD_FreePreparsedData(ref IntPtr pointerToPreparsedData);

        private delegate bool GetString(SafeFileHandle hidDeviceObject, IntPtr pointerToBuffer, uint bufferLength);

        public ConnectedDeviceDefinition GetDeviceDefinition(string deviceId, SafeFileHandle safeFileHandle)
        {
            var hidAttributes = GetHidAttributes(safeFileHandle);
            var hidCollectionCapabilities = GetHidCapabilities(safeFileHandle);

            var manufacturer = GetManufacturer(safeFileHandle);
            var serialNumber = GetSerialNumber(safeFileHandle);
            var product = GetProduct(safeFileHandle);

            return new ConnectedDeviceDefinition(
                deviceId,
                DeviceType.Hid,
                writeBufferSize: hidCollectionCapabilities.OutputReportByteLength,
                readBufferSize: hidCollectionCapabilities.InputReportByteLength,
                manufacturer: manufacturer,
                productName: product,
                productId: (ushort)hidAttributes.ProductId,
                serialNumber: serialNumber,
                usage: hidCollectionCapabilities.Usage,
                usagePage: hidCollectionCapabilities.UsagePage,
                vendorId: (ushort)hidAttributes.VendorId,
                versionNumber: (ushort)hidAttributes.VersionNumber,
                classGuid: GetHidGuid());
        }

        public string GetManufacturer(SafeFileHandle safeFileHandle) => GetHidString(safeFileHandle, HidD_GetManufacturerString);

        public string GetProduct(SafeFileHandle safeFileHandle) => GetHidString(safeFileHandle, HidD_GetProductString);

        public string GetSerialNumber(SafeFileHandle safeFileHandle) => GetHidString(safeFileHandle, HidD_GetSerialNumberString);

        public HidAttributes GetHidAttributes(SafeFileHandle safeFileHandle)
        {
            HidD_GetAttributes(safeFileHandle, out var hidAttributes);
            return hidAttributes;
        }

        public HidCollectionCapabilities GetHidCapabilities(SafeFileHandle readSafeFileHandle)
        {
            HidD_GetPreparsedData(readSafeFileHandle, out var pointerToPreParsedData);

            HidP_GetCaps(pointerToPreParsedData, out var hidCollectionCapabilities);

            HidD_FreePreparsedData(ref pointerToPreParsedData);

            return hidCollectionCapabilities;
        }

        public Guid GetHidGuid()
        {
            if (_HidGuid.HasValue)
            {
                return _HidGuid.Value;
            }

            HidD_GetHidGuid(out var hidGuid);

            _HidGuid = hidGuid;

            return hidGuid;
        }

        public Stream OpenRead(SafeFileHandle readSafeFileHandle, ushort readBufferSize) => new FileStream(readSafeFileHandle, FileAccess.Read, readBufferSize, true);

        public Stream OpenWrite(SafeFileHandle writeSafeFileHandle, ushort writeBufferSize) => new FileStream(writeSafeFileHandle, FileAccess.ReadWrite, writeBufferSize, true);
        
        private static string GetHidString(SafeFileHandle safeFileHandle, GetString getString, [CallerMemberName] string callMemberName = null)
        {
            try
            {
                var pointerToBuffer = Marshal.AllocHGlobal(126);
                var isSuccess = getString(safeFileHandle, pointerToBuffer, 126);
                var text = Marshal.PtrToStringAuto(pointerToBuffer);
                Marshal.FreeHGlobal(pointerToBuffer);
                return text;
            }
            catch 
            {
                return null;
            }
        }
        
    }
}