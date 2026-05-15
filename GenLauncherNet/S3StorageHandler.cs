using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenLauncherNet
{
    public class S3StorageHandler
    {
        private IMinioClient minioClient;

        public const string GenInsavePKey = "S58TYR9ISEZV8PBP8QG1";
        public const string GenInsaveSKey = "b2RU1oqVU5toJRnb4gODrXX8sBSgoLcHRX6qPWxj";

        public async Task<List<ModificationFileInfo>> GetModInfo(ModificationVersion version)
        {
            var current = new CultureInfo("en-US");
            current.DateTimeFormat = new DateTimeFormatInfo();
            current.DateTimeFormat.Calendar = new GregorianCalendar();

            Thread.CurrentThread.CurrentCulture = current;

            if (string.IsNullOrEmpty(version.S3HostPublicKey) || String.IsNullOrEmpty(version.S3HostSecretKey))
                minioClient = CreateClient(version.S3HostLink, GenInsavePKey, GenInsaveSKey);
            else
                minioClient = CreateClient(version.S3HostLink, version.S3HostPublicKey, version.S3HostSecretKey);

            return await GetFilesFromBucket(version);
        }

        private async Task<List<ModificationFileInfo>> GetFilesFromBucket(ModificationVersion version)
        {
            await minioClient.ListBucketsAsync();

            var filestList = new List<ModificationFileInfo>();

            var listObjectsArgs = new ListObjectsArgs()
                .WithBucket(version.S3BucketName)
                .WithPrefix(version.S3FolderName)
                .WithRecursive(true);

            var objects = minioClient.ListObjectsEnumAsync(listObjectsArgs);
            var enumerator = objects.GetAsyncEnumerator(CancellationToken.None);

            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    var item = enumerator.Current;
                    filestList.Add(new ModificationFileInfo(item.Key.Replace(version.S3FolderName + '/', ""), item.ETag, item.Size));
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
            }

            return filestList;
        }

        private static IMinioClient CreateClient(string endpoint, string accessKey, string secretKey)
        {
            return new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .Build();
        }
    }
}
