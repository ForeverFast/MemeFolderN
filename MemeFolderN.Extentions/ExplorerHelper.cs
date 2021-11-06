using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace MemeFolderN.Extentions
{
    public static class ExplorerHelper
    {
        public static string CreateNewImage(string parentFolderPath, string imagePath, string title = null)
        {
            string newImagePath = string.Empty;
            if (!string.IsNullOrEmpty(title))
            {
                newImagePath = $@"{parentFolderPath}\{title}{Path.GetExtension(imagePath)}";
            }
            else
            {
                newImagePath = Path.Combine(parentFolderPath, Path.GetFileName(imagePath));
            }

            newImagePath = GetImageAnotherName(parentFolderPath, newImagePath);
            File.Copy(imagePath, newImagePath);

            return newImagePath;
        }

        public static string CreateNewMiniImageForNewImage(string parentFolderPath, string imagePath)
        {
            string newMiniImageMemePath = @$"{parentFolderPath}\Mini{Path.GetFileName(imagePath)}";
            newMiniImageMemePath = GetImageAnotherName(parentFolderPath, newMiniImageMemePath);
            Image result = ResizeOrigImg(Image.FromFile(imagePath), 120, 72);
            result.Save(newMiniImageMemePath);
            result.Dispose();

            return newMiniImageMemePath;
        }

        private static string GetImageAnotherName(string parentFolderPath, string imagePath)
        {
            string newMemePath = @$"{parentFolderPath}\{Path.GetFileName(imagePath)}";
            if (!File.Exists(newMemePath))
            {
                return newMemePath;
            }

            int num = 2;
            while (true)
            {
                newMemePath = @$"{parentFolderPath}\{Path.GetFileNameWithoutExtension(imagePath)} ({num++}){Path.GetExtension(imagePath)}";
                if (!File.Exists(newMemePath))
                {
                    return newMemePath;
                }
            }
        }

        public static Image ResizeOrigImg(Image image, int nWidth, int nHeight)
        {
            int newWidth, newHeight;
            var coefH = (double)nHeight / (double)image.Height;
            var coefW = (double)nWidth / (double)image.Width;
            if (coefW >= coefH)
            {
                newHeight = (int)(image.Height * coefH);
                newWidth = (int)(image.Width * coefH);
            }
            else
            {
                newHeight = (int)(image.Height * coefW);
                newWidth = (int)(image.Width * coefW);
            }

            Image result = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(result))
            {
                g.CompositingQuality = CompositingQuality.Default;
                g.SmoothingMode = SmoothingMode.Default;
                g.InterpolationMode = InterpolationMode.Default;

                g.DrawImage(image, 0, 0, newWidth, newHeight);
                g.Dispose();
            }
            return result;
        }


        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            target = Directory.CreateDirectory(Path.Combine(target.FullName, source.Name));

            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                //DirectoryInfo nextTargetSubDir =
                //    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, target);
            }
        }
    }
}
