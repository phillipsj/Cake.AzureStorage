using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.AzureStorage {
    /// <summary>
    /// Contains functionality for working with Azure Storage
    /// </summary>
    [CakeAliasCategory("AzureStorage")]
    public static class AzureStorageAlias {
        /// <summary>
        /// Uploads a file to Azure Blob Storage.
        /// </summary>
        /// <example>
        /// <code>
        /// Task("PackageNoSettings")
        ///  .Does(() => {
        ///   var settings = new AzureStorageSettings();
        ///   settings.AccountName = "AccountName";
        ///   settings.Key = "API KEY";
        ///   settings.ContainerName = "ContainerName";
        ///   settings.BlobName = "BlobName";
        ///   UploadFileToBlob(settings, GetFile("./path/to/file/to/upload"));
        /// });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="settings">Azure Storage Settings.</param>
        /// <param name="fileToUpload">File to upload to Azure storage.</param>
        [CakeMethodAlias]
        public static void UploadFileToBlob(this ICakeContext context, AzureStorageSettings settings, FilePath fileToUpload) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            AzureStorage.UploadFileToBlob(settings, fileToUpload);
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
        [CakeMethodAlias]
        public static void DeleteBlob(this ICakeContext context, AzureStorageSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            AzureStorage.DeleteBlob(settings);
        }

        /// <summary>
        /// Deletes an Azure Storage blobs by prefix.
        /// </summary>
        /// <example>
        /// <code>
        /// Task("PackageAfterDelete")
        ///  .Does(() => {
        ///   var settings = new AzureStorageSettings();
        ///   settings.AccountName = "AccountName";
        ///   settings.Key = "API KEY";
        ///   settings.ContainerName = "ContainerName";
        ///   settings.BlobName = "PrefixToSearchBy";
        /// 
        ///   var deletedLog = DeleteBlobsByPrefix(settings);
        ///   foreach (var line in deletedLog){
        ///     Information(line);
        ///   }
        ///   
        ///   settings.BlobName = "NameToUploadAs";
        ///   var filePath = new FilePath("./location/of/file/to/upload");
        ///   UploadFileToBlob(settings, filePath));
        /// });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="settings">Azure Storage Settings.</param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IEnumerable<string> DeleteBlobsByPrefix(this ICakeContext context, AzureStorageSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            return AzureStorage.DeleteBlobsByPrefix(settings);
        }
    }
}
