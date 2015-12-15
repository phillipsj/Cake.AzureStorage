using System;
using System.IO;
using Cake.Core.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace Cake.AzureStorage {
    /// <summary>
    /// Azure Storage Client for Cake.
    /// </summary>
    public class AzureStorage {
        /// <summary>
        /// Uploads file to an Azure blob storage account.
        /// </summary>
        /// <param name="settings">Azure Storage Settings</param>
        /// <param name="fileToUpload">File to upload to blob storage.</param>
        public static void UploadFileToBlob(AzureStorageSettings settings, FilePath fileToUpload) {
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings), "Settings are null");
            }
            if (string.IsNullOrEmpty(settings.AccountName)) {
                throw new ArgumentNullException("account name", "Account name is null.");
            }
            if (string.IsNullOrEmpty(settings.Key)) {
                throw new ArgumentNullException("key", "Key is null.");
            }
            if (string.IsNullOrEmpty(settings.ContainerName)) {
                throw new ArgumentNullException("container name", "Container name is null.");
            }
            if (string.IsNullOrEmpty(settings.BlobName)) {
                throw new ArgumentNullException("blob name", "Blob name is null.");
            }
            if (fileToUpload == null) {
                throw new ArgumentNullException(nameof(fileToUpload), "File to upload is null.");
            }

            var storageAccount = new CloudStorageAccount(new StorageCredentials(settings.AccountName, settings.Key), true);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);
            var block = container.GetBlockBlobReference(settings.BlobName);
            block.UploadFromFile(fileToUpload.FullPath, FileMode.Open);
        }

    }
}
