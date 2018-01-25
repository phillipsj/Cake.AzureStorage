using System;
using Cake.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Cake.AzureStorage.Tests {
    public class AzureStorageAliasesTests {
        [TestClass]
        public class UploadFileToBlobAliasTests {
            [TestMethod]
            public void ShouldThrowIfContextIsNull() {
                // When
                var result = Assert.ThrowsException<ArgumentNullException>(() =>
                    AzureStorageAlias.UploadFileToBlob(null, null, null));

                // Then
                Assert.AreEqual("context", result.ParamName);
            }

            [TestMethod]
            public void ShouldThrowIfSettingsAreNull() {
                // Given 
                var context = Substitute.For<ICakeContext>();
                // When
                var result = Assert.ThrowsException<ArgumentNullException>(() =>
                    AzureStorageAlias.UploadFileToBlob(context, null, null));

                // Then
                Assert.AreEqual("settings", result.ParamName);
            }

            [TestMethod]
            public void ShouldThrowIfFileToUploadIsNull() {
                // Given 
                var context = Substitute.For<ICakeContext>();
                var settings = new AzureStorageSettings {
                    AccountName = "test",
                    Key = "test",
                    ContainerName = "test",
                    BlobName = "test"
                };

                // When
                var result = Assert.ThrowsException<ArgumentNullException>(() =>
                    AzureStorageAlias.UploadFileToBlob(context, settings, null));

                // Then
                Assert.AreEqual("fileToUpload", result.ParamName);
            }
        }

        [TestClass]
        public class DeleteBlobAliasTests {
            [TestMethod]
            public void ShouldThrowIfContextIsNull() {
                // When
                var result = Assert.ThrowsException<ArgumentNullException>(() =>
                    AzureStorageAlias.DeleteBlob(null, null));

                // Then
                Assert.AreEqual("context", result.ParamName);
            }

            [TestMethod]
            public void ShouldThrowIfSettingsAreNull() {
                // Given 
                var context = Substitute.For<ICakeContext>();
                // When
                var result = Assert.ThrowsException<ArgumentNullException>(() =>
                    AzureStorageAlias.DeleteBlob(context, null));

                // Then
                Assert.AreEqual("settings", result.ParamName);
            }
        }

        [TestClass]
        public class DeleteBlobsByPrefixAliasTests {
            [TestMethod]
            public void ShouldThrowIfContextIsNull() {
                // When
                var result = Assert.ThrowsException<ArgumentNullException>(() =>
                    AzureStorageAlias.DeleteBlobsByPrefix(null, null));

                // Then
                Assert.AreEqual("context", result.ParamName);
            }

            [TestMethod]
            public void ShouldThrowIfSettingsAreNull() {
                // Given 
                var context = Substitute.For<ICakeContext>();
                // When
                var result = Assert.ThrowsException<ArgumentNullException>(() =>
                    AzureStorageAlias.DeleteBlobsByPrefix(context, null));

                // Then
                Assert.AreEqual("settings", result.ParamName);
            }
        }
    }
}
