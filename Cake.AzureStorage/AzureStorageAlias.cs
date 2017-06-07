using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.AzureStorage {
    /// <summary>
    /// Contains functionality for workign with Azure Storage
    /// </summary>
    [CakeAliasCategory("AzureStorage")]
    public static class AzureStorageAlias {
        /// <summary>
        /// Uploads a file to Azure Blob Storage.
        /// </summary>
        /// <example>
        /// <code>
        /// Task("PackageNoSettings")
        ///  .Does(async () => {
        ///   var settings = new AzureStorageSettings();
        ///   settings.AccountName = "AccountName";
        ///   settings.Key = "API KEY";
        ///   settings.ContainerName = "ContainerName";
        ///   settings.BlobName = "BlobName";
        ///   await UploadFileToBlobAsync(settings, GetFile("./path/to/file/to/upload"));
        /// });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="settings">Azure Storage Settings.</param>
        /// <param name="fileToUpload">File to upload to Azure storage.</param>
        [CakeMethodAlias]
        public static Task UploadFileToBlob(this ICakeContext context, AzureStorageSettings settings, FilePath fileToUpload) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            return AzureStorage.UploadFileToBlobAsync(settings, fileToUpload);
        }

        /// <summary>
        /// Deletes an Azure Storage blob
        /// </summary>
        /// <example>
        /// <code>
        /// Task("PackageAfterDelete")
        ///  .Does(() => {
        ///   var settings = new AzureStorageSettings();
        ///   settings.AccountName = "AccountName";
        ///   settings.Key = "API KEY";
        ///   settings.ContainerName = "ContainerName";
        ///   settings.BlobName = "NameToDelete";
        ///   DeleteBlob(settings);
        ///   
        ///   settings.BlobName = "NameToUploadAs";
        ///    var filePath = new FilePath("./location/of/file/to/upload");
        ///   UploadFileToBlob(settings, filePath));
        /// });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="settings">Azure Storage Settings.</param>
        /// <returns>Task with bool telling whether delete operation went well.</returns>
        [CakeMethodAlias]
        public static Task<bool> DeleteBlob(this ICakeContext context, AzureStorageSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            return AzureStorage.DeleteBlobAsync(settings);
        }

        /// <summary>
        /// Deletes an Azure Storage blobs by prefix.
        /// </summary>
        /// <example>
        /// <code>
        /// Task("PackageAfterDelete")
        ///  .Does(async () => {
        ///   var settings = new AzureStorageSettings();
        ///   settings.AccountName = "AccountName";
        ///   settings.Key = "API KEY";
        ///   settings.ContainerName = "ContainerName";
        ///   settings.BlobName = "PrefixToSearchBy";
        /// 
        ///   var deletedLog = await DeleteBlobsByPrefixAsync(settings);
        ///   foreach (var line in deletedLog){
        ///     Information(line);
        ///   }
        ///   
        ///   settings.BlobName = "NameToUploadAs";
        ///   var filePath = new FilePath("./location/of/file/to/upload");
        ///   await UploadFileToBlobAsync(settings, filePath));
        /// });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="settings">Azure Storage Settings.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="string"/> containing log of blobs deleted.</returns>
        [CakeMethodAlias]
        public static Task<IEnumerable<string>> DeleteBlobsByPrefixAsync(this ICakeContext context, AzureStorageSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            return AzureStorage.DeleteBlobsByPrefixAsync(settings);
        }
    }
}
