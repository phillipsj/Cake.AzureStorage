using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.AzureStorage {
    [CakeAliasCategory("AzureStorage")]
    public static class AzureStorageAlias {
        [CakeMethodAlias]
        public static void UploadFileToBlob(this ICakeContext context, AzureStorageSettings settings, FilePath fileToUpload) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            AzureStorage.UploadFileToBlob(settings, fileToUpload);
        }

        [CakeMethodAlias]
        public static void DeleteBlob(this ICakeContext context, AzureStorageSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            AzureStorage.DeleteBlob(settings);
        }
    }
}
