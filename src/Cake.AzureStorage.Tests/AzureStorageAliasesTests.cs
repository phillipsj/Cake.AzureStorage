using System;
using Cake.Core;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Cake.AzureStorage.Tests {
    public sealed class AzureStorageAliasesTests {
        public class UploadFileToBlobAliasTests {
            [Fact]
            public void ShouldThrowIfContextIsNull() {
                // When
                var result = Record.Exception(() => 
                    AzureStorageAlias.UploadFileToBlob(null, null, null));

                // Then
                result.Should().BeOfType<ArgumentNullException>().Subject.ParamName.Should().Equals("context");
            }

            [Fact]
            public void ShouldThrowIfSettingsAreNull() {
                // Given 
                var context = Substitute.For<ICakeContext>();
                
                // When
                var result = Record.Exception(() => 
                    AzureStorageAlias.UploadFileToBlob(context, null, null));

                // Then
                result.Should().BeOfType<ArgumentNullException>().Subject.ParamName.Should().Equals("settings");
            }

            [Fact]
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
                
                var result = Record.Exception(() => 
                    AzureStorageAlias.UploadFileToBlob(context, settings, null));

                // Then
                result.Should().BeOfType<ArgumentNullException>().Subject.ParamName.Should().Equals("fileToUpload");
            }
        }

        public class DeleteBlobAliasTests {
            [Fact]
            public void ShouldThrowIfContextIsNull() {
                // When
                var result = Record.Exception(() => 
                    AzureStorageAlias.DeleteBlob(null, null));

                // Then
                result.Should().BeOfType<ArgumentNullException>().Subject.ParamName.Should().Equals("context");
            }

            [Fact]
            public void ShouldThrowIfSettingsAreNull() {
                // Given 
                var context = Substitute.For<ICakeContext>();
                
                // When
                var result = Record.Exception(() => 
                    AzureStorageAlias.DeleteBlob(context, null));


                // Then
                result.Should().BeOfType<ArgumentNullException>().Subject.ParamName.Should().Equals("settings");
            }
        }

        public class DeleteBlobsByPrefixAliasTests {
            [Fact]
            public void ShouldThrowIfContextIsNull() {
                // When
                var result = Record.Exception(() => 
                    AzureStorageAlias.DeleteBlobsByPrefix(null, null));

                // Then
                result.Should().BeOfType<ArgumentNullException>().Subject.ParamName.Should().Equals("context");
            }

            [Fact]
            public void ShouldThrowIfSettingsAreNull() {
                // Given 
                var context = Substitute.For<ICakeContext>();
                // When
                var result = Record.Exception(() => 
                    AzureStorageAlias.DeleteBlobsByPrefix(context, null));

                // Then
                result.Should().BeOfType<ArgumentNullException>().Subject.ParamName.Should().Equals("settings");
            }
        }
    }
}
