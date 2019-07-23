namespace Cake.AzureStorage {
    /// <summary>
    ///     Initializes a new instance of the <see cref="AzureStorageSettings"/> class.
    /// </summary>
    public class AzureStorageSettings {
        /// <summary>
        /// Gets or sets the Use Local Emulator option.
        /// </summary>
        public bool UseLocal { get; set; }
        /// <summary>
        /// Gets or sets the Azure account name.
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Gets or sets the Azure API Key.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the Azure container name.
        /// </summary>
        public string ContainerName { get; set; }
        /// <summary>
        /// Gets or sets the Azure blob name.
        /// </summary>
        public string BlobName { get; set; }
        /// <summary>
        /// Gets or sets whether to use HTTPS or not, 
        /// </summary>
        /// <value>
        ///     <c>true</c> to use HTTPS, default; otherwise, <c>false</c>.
        /// </value>
        public bool UseHttps { get; set; }
        /// <summary>
        /// Gets or sets the content type property of the blob, 
        /// </summary>
        /// <value>
        ///     set to content type if desired, examples: video/mp4, image/jpeg, application/json
        /// </value>
        public string ContentType { get; set; }
        
        /// <summary>
        /// The constructor.
        /// </summary>
        public AzureStorageSettings() {
            UseHttps = true;
        }
    }
}
