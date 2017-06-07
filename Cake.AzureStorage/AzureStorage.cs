using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public static Task UploadFileToBlobAsync(AzureStorageSettings settings, FilePath fileToUpload) {
            CheckSettings(settings);
            if (fileToUpload == null)
            {
                throw new ArgumentNullException(nameof(fileToUpload), "File to upload is null.");
            }
            var storageAccount = new CloudStorageAccount(new StorageCredentials(settings.AccountName, settings.Key), true);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);
            var block = container.GetBlockBlobReference(settings.BlobName);
            return block.UploadFromFileAsync(fileToUpload.FullPath);
        }
        /// <summary>
        /// Deletes an Azure blob from Azure Storage
        /// </summary>
        /// <param name="settings">Azure Storage Settings</param>
        public static Task<bool> DeleteBlobAsync(AzureStorageSettings settings) {
            CheckSettings(settings);
            var storageAccount = new CloudStorageAccount(new StorageCredentials(settings.AccountName, settings.Key), true);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);
            var blob = container.GetBlockBlobReference(settings.BlobName);
            return blob.DeleteIfExistsAsync();
        }

        /// <summary>
        /// Deletes an Azure blob from Azure Storage using a prefix to determine which blobs to delete, prefix = BlobName
        /// </summary>
        /// <param name="settings">Azure Storage Settings</param>
        public static async Task<IEnumerable<string>> DeleteBlobsByPrefixAsync(AzureStorageSettings settings) {
            CheckSettings(settings);
            var storageAccount = new CloudStorageAccount(new StorageCredentials(settings.AccountName, settings.Key), true);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.ContainerName);
            
            var continuationToken = new BlobContinuationToken();
            var blobs = await container.ListBlobsSegmentedAsync(settings.BlobName, continuationToken).ConfigureAwait(false);

            var deleteTasks = blobs.Results.Select(blob => new CloudBlob(blob.Uri))
                .Select(cloudBlob => DeleteBlobReference(container, cloudBlob));

            return await Task.WhenAll(deleteTasks).ConfigureAwait(false);
        }

        private static async Task<string> DeleteBlobReference(CloudBlobContainer container, CloudBlob blobToDelete) {
            var blobReference = container.GetBlobReference(blobToDelete.Name);
            await blobReference.DeleteIfExistsAsync().ConfigureAwait(false);
            return $"{blobToDelete.Name} has been told to delete by reference.";
        }
    }
}
