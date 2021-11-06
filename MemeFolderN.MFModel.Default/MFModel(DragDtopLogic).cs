using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Extentions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase.Wpf
{
    public partial class MFModelWpf : MFModelBase
    {
        private readonly List<string> fileExtentions = new List<string> { ".jpg", ".jpeg", ".png", };

        public Task AddInputDataAsync(FolderDTO folderDTO, List<string> paths) => Task.Run(() => AddInputData(folderDTO, paths));

        public async Task AddInputData(FolderDTO folderDTO, List<string> paths)
        {
            FolderDTO parentFolder = folderDTO with { Folders = new(), Memes = new() };
            string parentFolderPath = parentFolder.FolderPath;

            List<string> folderData = GetDirectoriesList(parentFolderPath);
            folderData.AddRange(GetFilesList(parentFolderPath));

            paths.ForEach(path =>
            {
                if (File.Exists(path))
                {
                    if (fileExtentions.Any(x => x == Path.GetExtension(path)))
                    {
                        string newImagePath = ExplorerHelper.CreateNewImage(parentFolderPath, path);
                        string newMiniImagePath = ExplorerHelper.CreateNewMiniImageForNewImage(parentFolderPath, newImagePath);

                        parentFolder.Memes.Add(new MemeDTO
                        {
                            Title = Path.GetFileNameWithoutExtension(newImagePath),
                            ImagePath = newImagePath,
                            MiniImagePath = newMiniImagePath
                        });
                    }
                }
                else if (Directory.Exists(path))
                {
                    ExplorerHelper.Copy(path, parentFolderPath);

                    string newFolderPath = Path.Combine(parentFolderPath, Path.GetFileName(path));
                    FolderDTO newFolder = new FolderDTO
                    {
                        Title = Path.GetFileNameWithoutExtension(newFolderPath),
                        FolderPath = newFolderPath,
                        Memes = new(),
                        Folders = new()
                    };
                    parentFolder.Folders.Add(newFolder);
                    FolderCheckPath(newFolder, folderData);
                }
            });

            List<FolderDTO> dbUpdatedFolder = await extentionalDataService.BulkInsertAndUpdateFolder(parentFolder);
            List<FolderDTO> dbCreatedFolder = dbUpdatedFolder.SelectRecursive(x => x.Folders).ToList();
            List<MemeDTO> dbCreatedMemes = dbCreatedFolder.SelectMany(x => x.Memes).ToList();

            OnAddFoldersEvent(dbCreatedFolder);
            OnAddMemesEvent(dbCreatedMemes);
        }



        protected void FolderCheckPath(FolderDTO parentFolder, List<string> skipPaths = null)
        {
            string parentFolderPath = parentFolder.FolderPath;

            List<string> directories = GetDirectoriesList(parentFolderPath);

            List<string> files = GetFilesList(parentFolderPath);

            if (skipPaths != null)
            {
                directories = directories.Except(skipPaths).ToList();
                files = files.Except(skipPaths).ToList();
            }

            files.ForEach(filePath =>
            {
                string newMiniImagePath = ExplorerHelper.CreateNewMiniImageForNewImage(parentFolderPath, filePath);

                parentFolder.Memes.Add(new MemeDTO
                {
                    Title = Path.GetFileNameWithoutExtension(filePath),
                    ImagePath = filePath,
                    MiniImagePath = newMiniImagePath
                });
            });

            directories.ForEach(directoryPath =>
            {
                FolderDTO newFolder = new FolderDTO
                {
                    Title = Path.GetFileNameWithoutExtension(directoryPath),
                    FolderPath = directoryPath,
                    Memes = new(),
                    Folders = new()
                };
                parentFolder.Folders.Add(newFolder);
                FolderCheckPath(newFolder);
            });
        }

        protected List<string> GetDirectoriesList(string path) => Directory.GetDirectories(path).ToList();

        protected List<string> GetFilesList(string path) => Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
            .Where(s => fileExtentions.Any(x => x == Path.GetExtension(s)))
            .ToList();
    }
}
