// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-22-2017
//
// Last Modified By : David McCarter
// Last Modified On : 01-21-2017
// ***********************************************************************
// <copyright file="DriveHelper.cs" company="dotNetTips.Utility.Standard">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The IO namespace.
/// </summary>
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
