﻿using System;

namespace DeviceHandle.Windows
{
    [Flags]
    public enum FileAccessRights : uint
    {
        None = 0,
        GenericRead = 2147483648,
        GenericWrite = 1073741824
    }
}