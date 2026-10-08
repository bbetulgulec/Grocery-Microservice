using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace manav.webUI.Services.AwsS3Services;

public class CloudStorageService : ICloudStorageService
{
    private readonly IAmazonS3 _amazonS3;
    private readonly IConfiguration _configuration;

    public CloudStorageService(IConfiguration configuration, IAmazonS3 amazonS3)
    {
        _configuration = configuration;
        _amazonS3 = amazonS3;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folderPath)
    {
        if (file.Length == 0)
            return null;

        var bucketName = _configuration["AWSStorageOptions:BucketName"];
        var cdnDomain = _configuration["AWSStorageOptions:CdnDomain"];

        var fileName = $"{folderPath}/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        using (var stream = file.OpenReadStream())
        {
            var fileTransferUtility = new TransferUtility(_amazonS3);
            await fileTransferUtility.UploadAsync(stream, bucketName, fileName);
        }

        return $"{cdnDomain}/{fileName}";
    }

    public async Task DeleteFileAsync(string fileUrl)
    {
        if (string.IsNullOrEmpty(fileUrl))
            return;

        var bucketName = _configuration["AWSStorageOptions:BucketName"];
        var cdnDomain = _configuration["AWSStorageOptions:CdnDomain"];
        var fileKey = fileUrl.Replace($"{cdnDomain}/", "").TrimStart('/');
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = fileKey
        };

        await _amazonS3.DeleteObjectAsync(deleteRequest);
    }
}