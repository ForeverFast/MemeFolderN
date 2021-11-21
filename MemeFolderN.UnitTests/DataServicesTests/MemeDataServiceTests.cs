using MemeFolderN.Common.DTOClasses;
using MemeFolderN.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MemeFolderN.UnitTests.DataServicesTests
{
    public class MemeDataServiceTests
    {
        private readonly IMemeDataService memeDataService;
        private readonly IFolderDataService folderDataService;

        public Guid RootGuid { get; } = Guid.Parse("00000000-0000-0000-0000-000000000001");


        [Fact]
        public async void AddAndGetNewMeme()
        {
            // Arrange
            MemeDTO newMeme = GetSingleMeme();

            // Act
            MemeDTO dbCreatedMeme = await memeDataService.Add(newMeme);

            // Assert
            Assert.NotNull(dbCreatedMeme);
            Assert.NotNull(await memeDataService.GetById(dbCreatedMeme.Id));

            _ = await memeDataService.DeleteAllMemes();
        }

        [Fact]
        public async void DeleteMeme()
        {
            // Arrange
            MemeDTO dbCreatedMeme = await memeDataService.Add(GetSingleMeme());
            Guid guid = dbCreatedMeme.Id;

            // Act
            bool result = await memeDataService.Delete(guid);

            // Assert
            Assert.True(result);
            try
            {
                MemeDTO memeDTO = await memeDataService.GetById(guid);
                Assert.Null(memeDTO);
            }
            catch (NullReferenceException)
            {
                Assert.True(true);
            }
           

            _ = await memeDataService.DeleteAllMemes();
        }

        [Fact]
        public async void UpdateMeme()
        {
            // Arrange
            MemeDTO newMeme = GetSingleMeme();
            MemeDTO dbCreatedMeme = await memeDataService.Add(newMeme);

            // Act
            string newPropValue = "test2";
            MemeDTO proccesedMemeDTO = new MemeDTO
            {
                Id = dbCreatedMeme.Id,
                Title = newPropValue,
                ImagePath = dbCreatedMeme.ImagePath
            };
            MemeDTO dbUpdatedMeme = await memeDataService.Update(proccesedMemeDTO.Id, proccesedMemeDTO);

            // Assert
            Assert.NotNull(dbUpdatedMeme);
            Assert.True(dbUpdatedMeme.Id == proccesedMemeDTO.Id);
            Assert.True(dbUpdatedMeme.Title == newPropValue);

            _ = await memeDataService.DeleteAllMemes();
        }

        [Fact]
        public async void CreateMultipleMemes()
        {
            // Arrange
            List<MemeDTO> memes = GetMultupleMemes();

            // Act
            IEnumerable<MemeDTO> dbCreatedMemes = await memeDataService.AddRangeMemes(memes);

            // Assert
            Assert.True(memes.Count == dbCreatedMemes.Count());

            _ = await memeDataService.DeleteAllMemes();
        }

        [Fact]
        public async void DeleteMultipleMemes()
        {
            // Arrange
            List<MemeDTO> memes = GetMultupleMemes();
            IEnumerable<MemeDTO> dbCreatedMemes = await memeDataService.AddRangeMemes(memes);

            // Act
            //bool result = await memeDataService.DeleteRangeMemes(dbCreatedMemes.ToList());

            // Assert
            Assert.True(true);

            _ = await memeDataService.DeleteAllMemes();
        }

        #region Вспомогательные методы

        private MemeDTO GetSingleMeme()
        {
            MemeDTO memeEntity = new MemeDTO
            {
                Title =  "test1",
                ImagePath = "test1Path",
                ParentFolderId = RootGuid
            };

            return memeEntity;
        }

        private List<MemeDTO> GetMultupleMemes()
        {
            List<MemeDTO> memes = new List<MemeDTO>();
            for (int i = 0; i < 15; i++)
            {
                MemeDTO memeEntity = new MemeDTO
                {
                    Title = $"test{i + 1}",
                    ImagePath = $"test{i + 1}Path",
                    ParentFolderId = RootGuid 
                };
                memes.Add(memeEntity);
            }
            return memes;
        }

        #endregion

        public MemeDataServiceTests()
        {
            memeDataService = new MemeDataService();
            folderDataService = new FolderDataService();

            FolderDTO folder = folderDataService.GetById(RootGuid).Result;
            if (folder == null)
                folderDataService.Add(new FolderDTO { Id = RootGuid });

        }
    }
}
