namespace Hospital.Train.PL.Helper
{
    public static class DocumentSettings
    {
        public static string Upload(IFormFile file, string folderName)
        {
            
            string FolderPath=Path.Combine(Directory.GetCurrentDirectory(),$"wwwroot\\files\\{folderName}");

            
           string FileName=$"{Guid.NewGuid()}{file.FileName}";

            
            string FilePath=Path.Combine(FolderPath,FileName);

            
            using var Filestream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(Filestream);
            return FileName;
        }

        public static void Delete(string FileName, string folderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\files\\{folderName}", FileName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
