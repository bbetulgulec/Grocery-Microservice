namespace manav.webUI.Services.AwsS3Services;

public interface ICloudStorageService
{
    Task<string> UploadFileAsync(IFormFile file, string folderPath);
    Task DeleteFileAsync(string fileUrl);
}