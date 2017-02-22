using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace dotNetTips.Utility.Standard.IO
{
    /// <summary>
    /// Class DriveHelper.
    /// </summary>
    public static class DriveHelper
    {
        /// <summary>
        /// Finds the serial number of a drive.
        /// </summary>
        /// <param name="drive">Drive name.</param>
        /// <returns>Serial number.</returns>
        /// <remarks>This call could take some time. Recommend multi-threading.</remarks>
        //public string GetDriveSerialNumber(string drive)
        //{
        //    System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(string.IsNullOrWhiteSpace(drive) == false);

        //    string driveSerial = string.Empty;

        //    //No matter what is sent in, get just the drive letter
        //    string driveFixed = System.IO.Path.GetPathRoot(drive).Replace("\\", string.Empty);
           
        //    //Perform Query
        //    using (var querySearch = new ManagementObjectSearcher(string.Format("SELECT VolumeSerialNumber FROM Win32_LogicalDisk Where Name = '{0}'", driveFixed)))
        //    {
        //        using (var queryCollection = querySearch.Get())
        //        {
        //            ManagementObject moItem;
        //            foreach (moItem in queryCollection)
        //            {
        //                driveSerial = (string)moItem.Item("VolumeSerialNumber");
        //                break; // TODO: might not be correct. Was : Exit For
        //            }
        //        }
        //    }

        //    return driveSerial;

        //}
    }
}
