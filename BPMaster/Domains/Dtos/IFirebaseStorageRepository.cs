namespace RPMSMaster.Domains.Dtos
{
    public interface IFirebaseStorageRepository
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
    }
}
