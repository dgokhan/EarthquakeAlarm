using System;
using System.Threading.Tasks;
using Plugin.Permissions;

namespace DepremAlarmi.Controls.Helpers
{
    public class RequestPermissionsHelpers
    {
        #region Storage Request

        public async Task<bool> RequestStoragePermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                Console.WriteLine("Does not have storage permission granted, requesting.");
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Storage) &&
                    results[Plugin.Permissions.Abstractions.Permission.Storage] != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    Console.WriteLine("Storage permission Denied.");
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Location Request

        public async Task<bool> RequestLocationPermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);
            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                Console.WriteLine("Does not have storage permission granted, requesting.");
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Location);
                if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Storage) &&
                    results[Plugin.Permissions.Abstractions.Permission.Storage] != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    Console.WriteLine("Storage permission Denied.");
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
