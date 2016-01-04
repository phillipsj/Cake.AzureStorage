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

        private static void CheckSettings(AzureStorageSettings settings) {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "Settings are null");
            }
            if (string.IsNullOrEmpty(settings.AccountName))
            {
                throw new ArgumentNullException("account name", "Account name is null.");
            }
            if (string.IsNullOrEmpty(settings.Key))
            {
                throw new ArgumentNullException("key", "Key is null.");
            }
            if (string.IsNullOrEmpty(settings.ContainerName))
            {
                throw new ArgumentNullException("container name", "Container name is null.");
            }
            if (string.IsNullOrEmpty(settings.BlobName))
            {
                throw new ArgumentNullException("blob name", "Blob name is null.");
            }
        }
        /// <summary>
        /// Uploads file to an Azure blob storage account.
        /// </summary>
        /// <param name="settings">Azure Storage Settings</param>
        /// <param name="fileToUpload">File to upload to blob storage.</param>
        public static void UploadFileToBlob(AzureStorageSettings settings, FilePath fileToUpload) {
            CheckSettings(settings);
            if (fileToUpload == null)
            {
                throw new ArgumentNullException(nameof(fileToUpload), "File to upload is null.");
            }
            var storageAccount = new CloudStorageAccount(new StorageCredentials(settings.AccountName, settings.Key), true);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);
            var block = container.GetBlockBlobReference(settings.BlobName);
            block.UploadFromFile(fileToUpload.FullPath, FileMode.Open);
        }
        /// <summary>
        /// Deletes a Azure blob from Azure Storage
        /// </summary>
        /// <param name="settings">Azure Storage Settings</param>
        public static void DeleteBlob(AzureStorageSettings settings) {
            CheckSettings(settings);
            var storageAccount = new CloudStorageAccount(new StorageCredentials(settings.AccountName, settings.Key), true);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);
            var blob = container.GetBlockBlobReference(settings.BlobName);
            blob.Delete();
        }

    }
}
