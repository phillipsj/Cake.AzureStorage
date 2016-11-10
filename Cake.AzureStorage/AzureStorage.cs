using System;
using System.Collections.Generic;
using Cake.Core.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

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
            if (settings.UseLocal)
            {
                return;
            }
            if (string.IsNullOrEmpty(settings.AccountName))
            {
                throw new ArgumentNullException(nameof(settings.AccountName), "Account name is null.");
            }
            if (string.IsNullOrEmpty(settings.Key))
            {
                throw new ArgumentNullException(nameof(settings.Key), "Key is null.");
            }
            if (string.IsNullOrEmpty(settings.ContainerName))
            {
                throw new ArgumentNullException(nameof(settings.ContainerName), "Container name is null.");
            }
            if (string.IsNullOrEmpty(settings.BlobName))
            {
                throw new ArgumentNullException(nameof(settings.BlobName), "Blob name is null.");
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
            var storageAccount = GetStorageAccount(settings);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);
            var block = container.GetBlockBlobReference(settings.BlobName);
            block.UploadFromFile(fileToUpload.FullPath);
        }
        /// <summary>
        /// Deletes an Azure blob from Azure Storage
        /// </summary>
        /// <param name="settings">Azure Storage Settings</param>
        public static void DeleteBlob(AzureStorageSettings settings) {
            CheckSettings(settings);
            var storageAccount = GetStorageAccount(settings);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);
            var blob = container.GetBlockBlobReference(settings.BlobName);
            blob.Delete();
        }

        /// <summary>
        /// Deletes an Azure blob from Azure Storage using a prefix to determine which blobs to delete, prefix = BlobName
        /// </summary>
        /// <param name="settings">Azure Storage Settings</param>
        public static IEnumerable<string> DeleteBlobsByPrefix(AzureStorageSettings settings) {
            CheckSettings(settings);
            var storageAccount = GetStorageAccount(settings);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);

            var messages = new List<string>();

            var blobs = container.ListBlobs(settings.BlobName);
            foreach (var blob in blobs) {
                var cloudBlob = new CloudBlob(blob.Uri);
                var blobReference = container.GetBlobReference(cloudBlob.Name);
                blobReference.Delete();
                messages.Add(cloudBlob.Name + " has been told to delete by reference.");
            }

            return messages;
        }

        private static CloudStorageAccount GetStorageAccount(AzureStorageSettings settings) {
            return settings.UseLocal ? 
                CloudStorageAccount.DevelopmentStorageAccount : 
                new CloudStorageAccount(new StorageCredentials(settings.AccountName, settings.Key), true);
        }
    }
}
